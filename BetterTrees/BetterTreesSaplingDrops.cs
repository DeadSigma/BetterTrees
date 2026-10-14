using System;
using UnityEngine;

namespace BetterTrees
{
    /// <summary>
    /// Phase 1: one physical pickup per fully harvested large tree.
    /// Uses vanilla seed items as temporary inventory identifiers.
    /// Phase 2 will replace their world/held visuals with a miniature model
    /// of the respective tree and implement RMB planting and game-day growth.
    /// </summary>
    internal static class BetterTreesSaplingDrops
    {
        private const float DropHeight = 0.65f;

        internal static void DropAfterHarvest(HarvestableTree source, Vector3 worldPosition)
        {
            if (!Raft_Network.IsHost || source == null)
                return;

            string seedName = GetSeedName(source.gameObject.name);
            if (seedName == null)
                return; // Only the four requested large-tree variants.

            try
            {
                Item_Base item = ItemManager.GetItemByName(seedName);
                if (item == null)
                {
                    Debug.LogWarning(Main.logPrefix + " Missing seed item: " + seedName);
                    return;
                }

                // Helper.DropItem uses Raft's world pickup mechanism.
                // The source is an island tree, not an object parented to the raft.
                Helper.DropItem(
                    new ItemInstance(item, 1, item.MaxUses),
                    worldPosition + Vector3.up * DropHeight,
                    Vector3.up,
                    false);

                Debug.Log(Main.logPrefix + " One physical seed dropped from " + source.gameObject.name);
            }
            catch (Exception error)
            {
                Debug.LogError(Main.logPrefix + " Could not drop sapling: " + error);
            }
        }

        private static string GetSeedName(string sourceObjectName)
        {
            if (string.IsNullOrEmpty(sourceObjectName))
                return null;

            string name = sourceObjectName.ToLowerInvariant();
            if (name.Contains("birchtree_big"))
                return ChoppableTreesModConstants.Seed_BirchItemName;
            if (name.Contains("pinetree_big") ||
                name.Contains("firtree_big") ||
                name.Contains("tp_pinetreesnowy"))
                return ChoppableTreesModConstants.Seed_PineItemName;

            return null;
        }
    }
}
