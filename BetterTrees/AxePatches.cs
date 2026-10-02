using HarmonyLib;
using Steamworks;
using System.Collections.Generic;
using UnityEngine;

namespace BetterTrees
{
    internal static class BetterTreesAxeNetwork
    {
        private const int RequestOneHit = -1100;
        private const int RequestStone = -1101;
        private const int RequestMetal = -1102;
        private const int RequestTitanium = -1103;
        private const int VisualOnly = -1201;
        private const int HarvestCommit = -1202;

        private const float TreeSearchRadius = 1.25f;
        private const int MaxHarvestPasses = 256;

        public static bool IsBetterTreesMessage(int value)
        {
            return value == RequestOneHit ||
                   value == RequestStone ||
                   value == RequestMetal ||
                   value == RequestTitanium ||
                   value == VisualOnly ||
                   value == HarvestCommit;
        }

        public static int GetRequestMarker(PlayerInventory inventory)
        {
            if (inventory == null)
                return RequestOneHit;

            Slot slot = inventory.allSlots[inventory.hotbar.GetSelectedSlotIndex()];
            if (slot == null || slot.IsEmpty || !slot.HasValidItemInstance())
                return RequestOneHit;

            switch (slot.itemInstance.UniqueIndex)
            {
                case 188:
                    return RequestStone;
                case 32:
                    return RequestMetal;
                case 559:
                    return RequestTitanium;
                default:
                    return RequestOneHit;
            }
        }

        public static int GetHitsRequired(int requestMarker)
        {
            switch (requestMarker)
            {
                case RequestStone:
                    return Mathf.Max(1, Main.HitsToBreak_StoneAxe);
                case RequestMetal:
                    return Mathf.Max(1, Main.HitsToBreak_MetalAxe);
                case RequestTitanium:
                    return Mathf.Max(1, Main.HitsToBreak_TitaniumAxe);
                default:
                    return 1;
            }
        }

        public static bool IsRequest(int value)
        {
            return value == RequestOneHit ||
                   value == RequestStone ||
                   value == RequestMetal ||
                   value == RequestTitanium;
        }

        public static bool IsVisualOnly(int value)
        {
            return value == VisualOnly;
        }

        public static bool IsHarvestCommit(int value)
        {
            return value == HarvestCommit;
        }

        public static void SetVisualOnly(Message_AxeHit message)
        {
            message.treeObjectIndex = VisualOnly;
        }

        public static void SetHarvestCommit(Message_AxeHit message)
        {
            message.treeObjectIndex = HarvestCommit;
        }

        public static bool RegisterHostHit(HarvestableTree tree, int requestMarker)
        {
            BetterTreesChoppable state = tree != null ? tree.GetComponent<BetterTreesChoppable>() : null;
            if (state == null)
                return false;

            int hitsRequired = GetHitsRequired(requestMarker);
            state.HitProgress++;

            if (state.HitProgress < hitsRequired)
                return false;

            state.HitProgress = 0;
            return true;
        }

        public static void DepleteTree(HarvestableTree tree, PlayerInventory inventory)
        {
            if (tree == null)
                return;

            int remaining = MaxHarvestPasses;
            while (!tree.Depleted && remaining-- > 0)
            {
                if (!tree.Harvest(inventory))
                    break;
            }

            if (!tree.Depleted)
                Debug.LogWarning($"{Main.logPrefix} Tree could not be fully harvested: {tree.gameObject.name}");
        }

        public static HarvestableTree FindTree(Vector3 hitPoint)
        {
            Collider[] nearby = Physics.OverlapSphere(
                hitPoint,
                TreeSearchRadius,
                ~0,
                QueryTriggerInteraction.Collide);

            HarvestableTree bestTree = null;
            float bestDistance = float.MaxValue;
            HashSet<HarvestableTree> checkedTrees = new HashSet<HarvestableTree>();

            for (int i = 0; i < nearby.Length; i++)
            {
                Collider collider = nearby[i];
                if (collider == null)
                    continue;

                HarvestableTree tree = collider.GetComponentInParent<HarvestableTree>();
                if (tree == null ||
                    tree.Depleted ||
                    tree.GetComponent<BetterTreesChoppable>() == null ||
                    !checkedTrees.Add(tree))
                {
                    continue;
                }

                float distance = GetClosestColliderDistance(tree, hitPoint);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestTree = tree;
                }
            }

            if (bestTree != null)
                return bestTree;

            BetterTreesChoppable[] allTrees = Object.FindObjectsOfType<BetterTreesChoppable>();
            for (int i = 0; i < allTrees.Length; i++)
            {
                BetterTreesChoppable state = allTrees[i];
                if (state == null)
                    continue;

                HarvestableTree tree = state.GetComponent<HarvestableTree>();
                if (tree == null || tree.Depleted || !tree.gameObject.activeInHierarchy)
                    continue;

                float distance = GetClosestColliderDistance(tree, hitPoint);
                if (distance < bestDistance && distance <= TreeSearchRadius)
                {
                    bestDistance = distance;
                    bestTree = tree;
                }
            }

            return bestTree;
        }

        private static float GetClosestColliderDistance(HarvestableTree tree, Vector3 point)
        {
            Collider[] colliders = tree.GetComponentsInChildren<Collider>(true);
            float bestDistance = float.MaxValue;

            for (int i = 0; i < colliders.Length; i++)
            {
                Collider collider = colliders[i];
                if (collider == null || !collider.enabled || !collider.gameObject.activeInHierarchy)
                    continue;

                Vector3 closest = collider.ClosestPoint(point);
                float distance = Vector3.Distance(closest, point);
                if (distance < bestDistance)
                    bestDistance = distance;
            }

            if (bestDistance < float.MaxValue)
                return bestDistance;

            return Vector3.Distance(tree.transform.position, point);
        }
    }

    [HarmonyPatch(typeof(Axe), "OnAxeHit")]
    public static class BetterTreesAxeHitPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(Axe __instance)
        {
            Network_Player player = RAPI.GetLocalPlayer();
            if (player == null || !player.IsLocalPlayer || !__instance.gameObject.activeInHierarchy)
                return true;

            RaycastHit rayHit;
            if (!Helper.HitAtCursor(out rayHit, 5f, __instance.hitmask, QueryTriggerInteraction.UseGlobal))
                return true;

            if (!rayHit.transform.CompareTag("Tree"))
                return true;

            HarvestableTree tree = rayHit.transform.GetComponentInParent<HarvestableTree>();
            if (tree == null || tree.Depleted || tree.GetComponent<BetterTreesChoppable>() == null)
                return true;

            Message_AxeHit message = new Message_AxeHit(Messages.AxeHit, player, player.steamID)
            {
                treeObjectIndex = BetterTreesAxeNetwork.GetRequestMarker(player.Inventory),
                HitPoint = rayHit.point,
                HitNormal = rayHit.normal
            };

            player.Inventory.RemoveDurabillityFromHotSlot(1);
            __instance.PlayEffect(player.transform.parent, message.HitPoint, message.HitNormal);

            if (Raft_Network.IsHost)
            {
                bool shouldHarvest = BetterTreesAxeNetwork.RegisterHostHit(tree, message.treeObjectIndex);
                if (shouldHarvest)
                {
                    BetterTreesAxeNetwork.DepleteTree(tree, player.Inventory);
                    BetterTreesAxeNetwork.SetHarvestCommit(message);
                }
                else
                {
                    BetterTreesAxeNetwork.SetVisualOnly(message);
                }

                player.Network.RPC(
                    message,
                    Target.Other,
                    EP2PSend.k_EP2PSendReliable,
                    NetworkChannel.Channel_Game);
            }
            else
            {
                player.SendP2P(
                    message,
                    EP2PSend.k_EP2PSendReliable,
                    NetworkChannel.Channel_Game);
            }

            if (__instance.mode == AxeMode.Chopping)
                __instance.mode = AxeMode.None;

            return false;
        }
    }

    [HarmonyPatch(typeof(Network_Player), "Deserialize")]
    public static class BetterTreesAxeDeserializePatch
    {
        [HarmonyPrefix]
        public static bool Prefix(
            Network_Player __instance,
            Message_NetworkBehaviour msg,
            ref bool __result)
        {
            Message_AxeHit axeMessage = msg as Message_AxeHit;
            if (axeMessage == null ||
                msg.Type != Messages.AxeHit ||
                !BetterTreesAxeNetwork.IsBetterTreesMessage(axeMessage.treeObjectIndex))
            {
                return true;
            }

            int marker = axeMessage.treeObjectIndex;

            if (Raft_Network.IsHost)
            {
                if (!BetterTreesAxeNetwork.IsRequest(marker))
                {
                    __result = false;
                    return false;
                }

                HarvestableTree tree = BetterTreesAxeNetwork.FindTree(axeMessage.HitPoint);
                if (tree == null)
                {
                    Debug.LogWarning(
                        $"{Main.logPrefix} Host could not resolve BetterTrees hit at {axeMessage.HitPoint}");
                    __result = false;
                    return false;
                }

                if (!__instance.IsLocalPlayer && __instance.AxeScript != null)
                {
                    __instance.AxeScript.PlayEffect(
                        __instance.transform.parent,
                        axeMessage.HitPoint,
                        axeMessage.HitNormal);
                }

                bool shouldHarvest = BetterTreesAxeNetwork.RegisterHostHit(tree, marker);
                if (shouldHarvest)
                {
                    BetterTreesAxeNetwork.DepleteTree(tree, null);
                    BetterTreesAxeNetwork.SetHarvestCommit(axeMessage);
                }
                else
                {
                    BetterTreesAxeNetwork.SetVisualOnly(axeMessage);
                }

                // true - NetworkUpdateManager пересылает изменённое сообщение остальным игрокам
                __result = true;
                return false;
            }

            // Клиент не принимает запросы рубки напрямую - только решение хоста
            if (BetterTreesAxeNetwork.IsRequest(marker))
            {
                __result = false;
                return false;
            }

            if (!__instance.IsLocalPlayer && __instance.AxeScript != null)
            {
                __instance.AxeScript.PlayEffect(
                    __instance.transform.parent,
                    axeMessage.HitPoint,
                    axeMessage.HitNormal);
            }

            if (BetterTreesAxeNetwork.IsHarvestCommit(marker))
            {
                HarvestableTree tree = BetterTreesAxeNetwork.FindTree(axeMessage.HitPoint);
                if (tree == null)
                {
                    Debug.LogWarning(
                        $"{Main.logPrefix} Client could not resolve harvested tree at {axeMessage.HitPoint}");
                    __result = false;
                    return false;
                }

                PlayerInventory inventory = __instance.IsLocalPlayer ? __instance.Inventory : null;
                BetterTreesAxeNetwork.DepleteTree(tree, inventory);
            }

            __result = true;
            return false;
        }
    }
}
