using UnityEngine;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using HMLLibrary;


namespace BetterTrees
{

    public static class ChoppableTreesModConstants
    {
        public static string PlankItemName = "Plank";
        public static string PalmLeafItemName = "Thatch";
        public static string PlasticItemName = "Plastic";
        public static string ScrapItemName = "Scrap";
        public static string NailItemName = "Nail";
        public static string TitaniumIngotItemName = "TitaniumIngot";
        public static string CircuitBoardItemName = "CircuitBoard";
        public static string Blueprint_TitaniumToolsItemName = "Blueprint_TitaniumTools";
        public static string Battery_AdvancedItemName = "Battery_Advanced";
        public static string MetalIngotItemName = "MetalIngot";
        public static string ExplosivePowderItemName = "ExplosivePowder";
        public static string CopperIngotItemName = "CopperIngot";
        public static string BoltItemName = "Bolt";
        public static string Blueprint_MetalDetectorItemName = "Blueprint_MetalDetector";
        public static string GlassItemName = "Glass";
        public static string RopeItemName = "Rope";
        public static string HealingSalveItemName = "HealingSalve";
        public static string Canteen_WaterItemName = "Canteen_Water";
        public static string ClayPlate_BBQItemName = "ClayPlate_BBQ";
        public static string OxygenBottleItemName = "OxygenBottle";
        public static string HealingSalve_GoodItemName = "HealingSalve_Good";
        public static string EggItemName = "Egg";
        public static string PlasticBottle_WaterItemName = "PlasticBottle_Water";
        public static string Cooked_SalmonItemName = "Cooked_Salmon";
        public static string FeatherItemName = "Feather";
        public static string Claybowl_EmptyItemName = "Claybowl_Empty";
        public static string Berries_RedItemName = "Berries_Red";
        public static string DrinkingGlassItemName = "DrinkingGlass";
        public static string BananaItemName = "Banana";
        public static string PlasticBottle_SaltWaterItemName = "PlasticBottle_SaltWater";
        public static string BioFuelItemName = "BioFuel";
        public static string SeaVineItemName = "SeaVine";
        public static string SandItemName = "Sand";
        public static string ClayItemName = "Clay";
        public static string StoneItemName = "Stone";
        public static string VineGooItemName = "VineGoo";
        public static string Seed_PineItemName = "Seed_Pine";
        public static string Seed_BirchItemName = "Seed_Birch";
        public static string Placeable_CookingStand_Food_OneItemName = "Placeable_CookingStand_Food_One";
        public static string Placeable_BirdsNestItemName = "Placeable_BirdsNest";
        public static string Placeable_Cropplot_SmallItemName = "Placeable_Cropplot_Small";
        public static string Blueprint_AnchorStationaryAdvancedItemName = "Blueprint_AnchorStationaryAdvanced";
        public static string Blueprint_AntennaItemName = "Blueprint_Antenna";
        public static string Blueprint_BackpackAdvancedItemName = "Blueprint_BackpackAdvanced";
        public static string Blueprint_BatteryAdvancedItemName = "Blueprint_BatteryAdvanced";
        public static string Blueprint_BatteryChargerItemName = "Blueprint_BatteryCharger";
        public static string Blueprint_BiofuelExtractorItemName = "Blueprint_BiofuelExtractor";
        public static string Blueprint_BiofuelExtractorAdvancedItemName = "Blueprint_BiofuelExtractorAdvanced";
        public static string Blueprint_CanteenItemName = "Blueprint_Canteen";
        public static string Blueprint_Cropplot_Large_Tier3ItemName = "Blueprint_Cropplot_Large_Tier3";
        public static string Blueprint_Cropplot_Medium_Tier3ItemName = "Blueprint_Cropplot_Medium_Tier3";
        public static string Blueprint_Cropplot_Small_Tier3ItemName = "Blueprint_Cropplot_Small_Tier3";
        public static string Blueprint_DetailPlankItemName = "Blueprint_DetailPlank";
        public static string Blueprint_ElectricGrillItemName = "Blueprint_ElectricGrill";
        public static string Blueprint_ElectricPurifierItemName = "Blueprint_ElectricPurifier";
        public static string Blueprint_EngineControlsItemName = "Blueprint_EngineControls";
        public static string Blueprint_FireworkItemName = "Blueprint_Firework";
        public static string Blueprint_FueltankItemName = "Blueprint_Fueltank";
        public static string Blueprint_HeadLightItemName = "Blueprint_HeadLight";
        public static string Blueprint_HeadLight_AdvancedItemName = "Blueprint_HeadLight_Advanced";
        public static string Blueprint_MacheteItemName = "Blueprint_Machete";
        public static string Blueprint_MotorWheelItemName = "Blueprint_MotorWheel";
        public static string Blueprint_Pipe_FuelItemName = "Blueprint_Pipe_Fuel";
        public static string Blueprint_Pipe_WaterItemName = "Blueprint_Pipe_Water";
        public static string Blueprint_RecieverItemName = "Blueprint_Reciever";
        public static string Blueprint_RecyclerItemName = "Blueprint_Recycler";
        public static string Blueprint_SmelterElectricItemName = "Blueprint_SmelterElectric";
        public static string Blueprint_SteeringWheelItemName = "Blueprint_SteeringWheel";
        public static string Blueprint_Storage_LargeItemName = "Blueprint_Storage_Large";
        public static string Blueprint_WaterTankItemName = "Blueprint_WaterTank";
        public static string Blueprint_WindTurbineItemName = "Blueprint_WindTurbine";
        public static string Blueprint_ZiplineBaseItemName = "Blueprint_ZiplineBase";
        public static string Blueprint_ZiplineToolItemName = "Blueprint_ZiplineTool";
        public static string Blueprint_ZiplineToolElectricItemName = "Blueprint_ZiplineToolElectric";
        public static string AxeItemName = "Axe";
        public static string Axe_StoneItemName = "Axe_Stone";
        public static string Axe_TitaniumItemName = "Axe_Titanium";
        public static string BinocularsItemName = "Binoculars";
        public static string BowItemName = "Bow";
        public static string CompassItemName = "Compass";
        public static string FishingRodItemName = "FishingRod";
        public static string FishingRod_MetalItemName = "FishingRod_Metal";
        public static string HammerItemName = "Hammer";
        public static string Hook_PlasticItemName = "Hook_Plastic";
        public static string Hook_ScrapItemName = "Hook_Scrap";
        public static string Hook_TitaniumItemName = "Hook_Titanium";
        public static string MacheteItemName = "Machete";
        public static string MetalDetectorItemName = "MetalDetector";
        public static string NetCanisterItemName = "NetCanister";
        public static string NetGunItemName = "NetGun";
        public static string PaddleItemName = "Paddle";
        public static string PaintBrushItemName = "PaintBrush";
        public static string SharkBaitItemName = "SharkBait";
        public static string ShearItemName = "Shear";
        public static string ShovelItemName = "Shovel";
        public static string Spear_ScrapItemName = "Spear_Scrap";
        public static string Spear_PlankItemName = "Spear_Plank";
        public static string SweepNetItemName = "SweepNet";
        public static string Sword_TitaniumItemName = "Sword_Titanium";
        public static string ThrowableAnchorItemName = "ThrowableAnchor";
        public static string Titanium_ShovelItemName = "Titanium_Shovel";
        public static string Duffel_BackpackItemName = "Duffel_Backpack";
        public static string Tactical_BackpackItemName = "Tactical_Backpack";
        public static string Sturdy_HelmetItemName = "Sturdy_Helmet";
        public static string Reinforced_HelmetItemName = "Reinforced_Helmet";
        public static string Scrap_PulleyItemName = "Scrap_Pulley";
        public static string Titanium_PulleyItemName = "Titanium_Pulley";
        public static string Rugged_BootsItemName = "Rugged_Boots";
        public static string Exploration_BootsItemName = "Exploration_Boots";
        public static string Worn_GlovesItemName = "Worn_Gloves";
        public static string Grip_GlovesItemName = "Grip_Gloves";
        public static string Fortified_GlovesItemName = "Fortified_Gloves";
        public static string Reinforced_FlippersItemName = "Reinforced_Flippers";
        public static string Carbon_Steel_FlippersItemName = "Carbon_Steel_Flippers";
        public static string Oxygen_CanisterItemName = "Oxygen_Canister";
        public static string Oxygen_TankItemName = "Oxygen_Tank";
        public static string Thatch_BeltItemName = "Thatch_Belt";
        public static string Tattered_BeltItemName = "Tattered_Belt";
        public static string Leather_BeltItemName = "Leather_Belt";
        public static string Utility_BeltItemName = "Utility_Belt";
        public static string Caravan_BeltItemName = "Caravan_Belt";
        public static string Bucket_MilkItemName = "Bucket_Milk";
        public static string Canteen_EmptyItemName = "Canteen_Empty";
        public static string Canteen_SaltWaterItemName = "Canteen_SaltWater";
        public static string CaveMushroomItemName = "CaveMushroom";
        public static string ChiliItemName = "Chili";
        public static string Claybowl_CoconutChickenItemName = "Claybowl_CoconutChicken";
        public static string Claybowl_HeadBrothItemName = "Claybowl_HeadBroth";
        public static string Claybowl_HeartyStewItemName = "Claybowl_HeartyStew";
        public static string Claybowl_LeftoverItemName = "Claybowl_Leftover";
        public static string Claybowl_SimpleFishStewItemName = "Claybowl_SimpleFishStew";
        public static string Claybowl_RootVegetableSoupItemName = "Claybowl_RootVegetableSoup";
        public static string ClayPlate_CatfishDeluxeItemName = "ClayPlate_CatfishDeluxe";
        public static string ClayPlate_MushroomOmeletteItemName = "ClayPlate_MushroomOmelette";
        public static string ClayPlate_SalmonLsItemName = "ClayPlate_SalmonLs";
        public static string ClayPlate_SharkDinnerItemName = "ClayPlate_SharkDinner";
        public static string ClayPlate_SteakWithJamItemName = "ClayPlate_SteakWithJam";
        public static string ClayPlate_SushiItemName = "ClayPlate_Sushi";
        public static string CoconutItemName = "Coconut";
        public static string Cooked_BeetItemName = "Cooked_Beet";
        public static string Cooked_CatfishItemName = "Cooked_Catfish";
        public static string Cooked_DrumstickItemName = "Cooked_Drumstick";
        public static string Cooked_GenericMeatItemName = "Cooked_GenericMeat";
        public static string Cooked_HerringItemName = "Cooked_Herring";
        public static string Cooked_MackerelItemName = "Cooked_Mackerel";
        public static string Cooked_PomfretItemName = "Cooked_Pomfret";
        public static string Cooked_PotatoItemName = "Cooked_Potato";
        public static string Cooked_SharkItemName = "Cooked_Shark";
        public static string Cooked_TilapiaItemName = "Cooked_Tilapia";
        public static string DrinkingGlass_CoconutBeatItemName = "DrinkingGlass_CoconutBeat";
        public static string DrinkingGlass_LeftoverItemName = "DrinkingGlass_Leftover";
        public static string DrinkingGlass_MangonanaItemName = "DrinkingGlass_Mangonana";
        public static string DrinkingGlass_RedbeetShotItemName = "DrinkingGlass_RedbeetShot";
        public static string DrinkingGlass_RedMelonItemName = "DrinkingGlass_RedMelon";
        public static string DrinkingGlass_SilverSmoothieItemName = "DrinkingGlass_SilverSmoothie";
        public static string DrinkingGlass_SimpleSmoothieItemName = "DrinkingGlass_SimpleSmoothie";
        public static string DrinkingGlass_SpicyPineberryItemName = "DrinkingGlass_SpicyPineberry";
        public static string DrinkingGlass_StrawberryColadaItemName = "DrinkingGlass_StrawberryColada";
        public static string HoneyCombItemName = "HoneyComb";
        public static string Jar_HoneyItemName = "Jar_Honey";
        public static string JuniperItemName = "Juniper";
        public static string MangoItemName = "Mango";
        public static string PineappleItemName = "Pineapple";
        public static string PlasticBottle_EmptyItemName = "PlasticBottle_Empty";
        public static string PlasticCup_EmptyItemName = "PlasticCup_Empty";
        public static string PlasticCup_SaltWaterItemName = "PlasticCup_SaltWater";
        public static string Raw_BeetItemName = "Raw_Beet";
        public static string Raw_CatfishItemName = "Raw_Catfish";
        public static string Raw_DrumstickItemName = "Raw_Drumstick";
        public static string Raw_GenericMeatItemName = "Raw_GenericMeat";
        public static string Raw_HerringItemName = "Raw_Herring";
        public static string Raw_MackerelItemName = "Raw_Mackerel";
        public static string Raw_PomfretItemName = "Raw_Pomfret";
        public static string Raw_PotatoItemName = "Raw_Potato";
        public static string Raw_SalmonItemName = "Raw_Salmon";
        public static string Raw_SharkItemName = "Raw_Shark";
        public static string Raw_TilapiaItemName = "Raw_Tilapia";
        public static string SilverAlgaeItemName = "SilverAlgae";
        public static string StrawberryItemName = "Strawberry";
        public static string TurmericItemName = "Turmeric";
        public static string WatermelonItemName = "Watermelon";
        public static string ReefHuts_PlasticWallItemName = "ReefHuts_PlasticWall";
        public static string ReefHuts_StickItemName = "ReefHuts_Stick";
        public static string Layer_Default = "Default";
        public static string Layer_Tree = "Tree";
        public static string Layer_Item = "Item";
        public static string Tag_Tree = "Tree";
        public static string Tag_Untagged = "Untagged";
        public static string Particle_TreeBreak = "Particle_TreeBreak";
        public static string ColliderName_LogInteraction = "Log_InteractionCollider";
        public static string ColliderName_InteractionNew = "collider_interaction_new";
        public static string ColliderName_BigPalm_CollisionObject0 = "CollisionObject0";
        public static string ColliderName_BigPalm_CollisionObject1 = "CollisionObject1";
        public static string ColliderName_DeadTree_Collider1 = "Collider1";
        public static string ColliderName_DeadTree_Collider2 = "Collider2";
        public static string ColliderName_DeadTree_Collider3 = "Collider3";
    }

    public sealed class BetterTreesChoppable : MonoBehaviour
    {
        public int HitProgress;

        private void OnDisable()
        {
            HitProgress = 0;
        }
    }

    public sealed class BetterTreesRuntimeAssets : MonoBehaviour
    {
        private readonly List<UnityEngine.Object> assets = new List<UnityEngine.Object>();

        public T Track<T>(T asset) where T : UnityEngine.Object
        {
            if (asset != null)
            {
                asset.hideFlags = HideFlags.DontSave;
                assets.Add(asset);
            }

            return asset;
        }

        private void OnDestroy()
        {
            foreach (UnityEngine.Object asset in assets)
            {
                if (asset != null)
                    UnityEngine.Object.Destroy(asset);
            }

            assets.Clear();
        }
    }

    public struct MinMaxLootConfig
    {
        public string ItemName;
        public int MinAmount;
        public int MaxAmount;
        public int ChanceForBonus;

        public MinMaxLootConfig(string name, int min, int max, int chanceForBonus)
        {
            ItemName = name;
            MinAmount = Mathf.Max(0, min);
            MaxAmount = Mathf.Max(MinAmount, max);
            ChanceForBonus = Mathf.Clamp(chanceForBonus, 0, 100);
        }
    }

    public struct CostData
    {
        public string ItemName;
        public int Amount;
        public CostData(string name, int amount) { ItemName = name; Amount = amount; }
    }
    public struct DropData
    {
        public string ItemName;
        public int Weight;
        public string Chance;
        public DropData(string name, int weight, string chance) { ItemName = name; Weight = weight; Chance = chance; }
    }
    public enum ColliderType { Capsule, Box }
    public struct ColliderData
    {
        public ColliderType Type;
        public float Param1;
        public float Param2;
        public float Param3;
        public ColliderData(ColliderType type, float p1, float p2, float p3) { Type = type; Param1 = p1; Param2 = p2; Param3 = p3; }
    }


    [HarmonyPatch(typeof(Landmark), "Initialize")]
    [HarmonyAfter("com.aidanamite.MoreBananas")]
    public partial class Patch_Landmark_Initialize_Choppables
    {
        private static Dictionary<string, System.Action<GameObject, int, int, string, List<GameObject>>> objectConfigurators;


        private static int defaultLayerId;
        private static int treeLayerId;
        private static int itemLayerId;



        private const string MoreBananasHarmonyId = "com.aidanamite.MoreBananas";
        private const string MoreBananasTreeName = "MoreBananas_Banana_Bush";

        private static HashSet<Transform> GetMoreBananasReservedTransforms(Landmark landmark)
        {
            var method = AccessTools.Method(typeof(Landmark), "Initialize");
            var patches = method != null ? Harmony.GetPatchInfo(method) : null;
            if (patches == null || !patches.Prefixes.Any(p => p.owner == MoreBananasHarmonyId))
                return null;

            var reserved = new HashSet<Transform>();
            foreach (MeshRenderer renderer in landmark.GetComponentsInChildren<MeshRenderer>(true))
            {
                if (renderer == null ||
                    (!renderer.name.Contains("Banana_Bush") &&
                     !renderer.name.Contains("Banan_Bush")))
                    continue;

                // More Bananas заменяет эту модель, а Destroy выполняется в конце кадра
                Transform model = renderer.transform;
                reserved.Add(model);

                Transform parent = model.parent;
                if (parent != null && parent != landmark.transform)
                    reserved.Add(parent);

                Transform grandparent = parent != null ? parent.parent : null;
                if (grandparent != null && grandparent != landmark.transform &&
                    (grandparent.name.IndexOf("banana", System.StringComparison.OrdinalIgnoreCase) >= 0 ||
                     grandparent.name.IndexOf("banan", System.StringComparison.OrdinalIgnoreCase) >= 0))
                    reserved.Add(grandparent);
            }

            return reserved;
        }

        private static bool IsMoreBananasProtected(Transform current, HashSet<Transform> reserved)
        {
            for (Transform parent = current; parent != null; parent = parent.parent)
            {
                // Деревья, созданные More Bananas, всегда обслуживаются самим модом
                if (parent.name.StartsWith(MoreBananasTreeName, System.StringComparison.Ordinal))
                    return true;

                if (reserved != null && reserved.Contains(parent))
                    return true;
            }

            return false;
        }

        static void Prefix(Landmark __instance, bool ___initialized)
        {
            if (___initialized) return;

            HashSet<Transform> moreBananasReserved = GetMoreBananasReservedTransforms(__instance);

            defaultLayerId = LayerMask.NameToLayer(ChoppableTreesModConstants.Layer_Default);
            treeLayerId = LayerMask.NameToLayer(ChoppableTreesModConstants.Layer_Tree);
            itemLayerId = LayerMask.NameToLayer(ChoppableTreesModConstants.Layer_Item);

            if (treeLayerId == -1)
            {
                treeLayerId = defaultLayerId;
                // Debug.LogWarning($"{BetterTrees.Main.logPrefix} Layer '{ChoppableTreesModConstants.Layer_Tree}' not found, using default layer ({LayerMask.LayerToName(defaultLayerId)}) for colliders requiring '{ChoppableTreesModConstants.Layer_Tree}' layer.");
            }
            if (itemLayerId == -1)
            {
                itemLayerId = defaultLayerId;
                // Debug.LogWarning($"{BetterTrees.Main.logPrefix} Layer '{ChoppableTreesModConstants.Layer_Item}' not found, using default layer ({LayerMask.LayerToName(defaultLayerId)}) for FL_Cloth and other pickups.");
            }


            List<GameObject> processedObjects = new List<GameObject>();

            foreach (Transform t in __instance.GetComponentsInChildren<Transform>(true))
            {
                if (IsMoreBananasProtected(t, moreBananasReserved))
                    continue;

                string objectNameLower = t.name.ToLower();
                GameObject rootObject = t.gameObject;

                bool isGenerallyConsidered = choppableKeywords.Any(keyword => objectNameLower.Contains(keyword));
                if (!isGenerallyConsidered) continue;
                if (exclusionKeywords.Any(exKey => objectNameLower.Contains(exKey))) continue;

                LODGroup lodGroup = t.GetComponentInParent<LODGroup>();
                if (lodGroup != null && lodGroup.gameObject != t.gameObject)
                {
                    Transform potentialRoot = t;
                    while (potentialRoot.parent != null && (potentialRoot.name.ToLower().Contains("_lod") || potentialRoot.GetComponent<MeshRenderer>() == null))
                    {
                        if (choppableKeywords.Any(keyword => potentialRoot.parent.name.ToLower().Contains(keyword)))
                        {
                            potentialRoot = potentialRoot.parent;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (choppableKeywords.Any(keyword => potentialRoot.name.ToLower().Contains(keyword)))
                    {
                        rootObject = potentialRoot.gameObject;
                    }
                    else
                    {
                        if (t.name.ToLower().Contains("_lod") || t.name.ToLower().Contains("_billboard")) continue;
                    }
                }
                else if (t.name.ToLower().Contains("_lod") || t.name.ToLower().Contains("_billboard"))
                {
                    bool partOfChoppable = false;
                    Transform currentParent = t.parent;
                    while (currentParent != null)
                    {
                        if (choppableKeywords.Any(keyword => currentParent.name.ToLower().Contains(keyword)))
                        {
                            rootObject = currentParent.gameObject;
                            partOfChoppable = true;
                            break;
                        }
                        currentParent = currentParent.parent;
                    }
                    if (!partOfChoppable) continue;
                }

                if (IsMoreBananasProtected(rootObject.transform, moreBananasReserved))
                    continue;

                if (rootObject.GetComponent<HarvestableTree>() != null ||
                    processedObjects.Contains(rootObject))
                {
                    continue;
                }


                string rootObjectNameLower = rootObject.name.ToLower();
                bool configured = false;

                var bestMatchConfigKey = objectConfigurators.Keys
                    .Where(key => rootObjectNameLower.Contains(key))
                    .OrderByDescending(key => key.Length)
                    .FirstOrDefault();

                if (bestMatchConfigKey != null)
                {
                    objectConfigurators[bestMatchConfigKey](rootObject, treeLayerId, defaultLayerId, rootObjectNameLower, processedObjects);
                    configured = true;
                }

                if (configured)
                {
                    if (!processedObjects.Contains(rootObject))
                    {
                        processedObjects.Add(rootObject);
                    }
                }
            }
        }

        static T CreateRuntimeAsset<T>(GameObject owner) where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();
            return owner.GetOrAddComponent<BetterTreesRuntimeAssets>().Track(asset);
        }

        static void ConfigureFLCloth(GameObject rootObject, string nameLower)
        {
            // // Debug.Log($"{BetterTrees.Main.logPrefix} Configuring FL_Cloth: {rootObject.name}"); 

            rootObject.tag = ChoppableTreesModConstants.Tag_Untagged;
            rootObject.layer = Patch_Landmark_Initialize_Choppables.itemLayerId;

            PickupItem pi = rootObject.GetOrAddComponent<PickupItem>();
            PickupItem_Networked pin = rootObject.GetOrAddComponent<PickupItem_Networked>();
            LandmarkItem_PickupItem lipi = rootObject.GetOrAddComponent<LandmarkItem_PickupItem>();

            BoxCollider bc = rootObject.GetComponent<BoxCollider>();
            if (bc == null)
            {
                bc = rootObject.AddComponent<BoxCollider>();
            }
            bc.isTrigger = false;

            rootObject.GetOrAddComponent<RaycastInteractable>();

            YieldHandler yh = rootObject.GetOrAddComponent<YieldHandler>();
            SO_ItemYield yieldAsset = CreateRuntimeAsset<SO_ItemYield>(rootObject);
            if (yieldAsset.yieldAssets == null) yieldAsset.yieldAssets = new List<Cost>();
            else yieldAsset.yieldAssets.Clear();
            yh.yieldAsset = yieldAsset;
            pi.yieldHandler = yh;

            RandomDropper rd = rootObject.GetOrAddComponent<RandomDropper>();
            SO_RandomDropper dropperAsset = CreateRuntimeAsset<SO_RandomDropper>(rootObject);
            if (dropperAsset.randomizer == null) dropperAsset.randomizer = new Randomizer();

            Item_Base ropeItem = ItemManager.GetItemByName(ChoppableTreesModConstants.RopeItemName);
            if (ropeItem != null)
            {
                List<RandomItem> randomItemList = new List<RandomItem> {
                    new RandomItem { obj = ropeItem, weight = 90, spawnChance = "0.9" }
                };
                dropperAsset.randomizer.items = randomItemList.ToArray();
            }
            else
            {
                //  Debug.LogWarning($"{BetterTrees.Main.logPrefix} ConfigureFLCloth: Item '{ChoppableTreesModConstants.RopeItemName}' not found for loot.");
                dropperAsset.randomizer.items = System.Array.Empty<RandomItem>();
            }
            Traverse.Create(rd).Field("amountOfItems").SetValue(new Interval_Int { minValue = 1, maxValue = 1 });
            Traverse.Create(rd).Field("randomDropperAsset").SetValue(dropperAsset);
            pi.dropper = rd;

            pi.pickupItemType = PickupItemType.Default;
            pi.canBePickedUp = true;
            pi.requiresHands = true;
            if (pi.itemInstance == null) pi.itemInstance = new ItemInstance();
            pi.itemInstance.Uses = 1;

            pi.networkID = pin;

            SetupNetwork(pin, lipi);

            //// Debug.Log($"{BetterTrees.Main.logPrefix} Successfully configured FL_Cloth: {rootObject.name} with Tag: {rootObject.tag}, Layer: {LayerMask.LayerToName(rootObject.layer)}");
        }

        static void ConfigurePickupLootObject(GameObject rootObject, string objConfigName, string lootItemNameConst, Interval_Int amountToDrop, string itemChanceStr)
        {
            // // Debug.Log($"{BetterTrees.Main.logPrefix} Configuring PickupLootObject '{objConfigName}': {rootObject.name}");

            rootObject.tag = ChoppableTreesModConstants.Tag_Untagged;
            rootObject.layer = Patch_Landmark_Initialize_Choppables.itemLayerId;

            PickupItem pi = rootObject.GetOrAddComponent<PickupItem>();
            PickupItem_Networked pin = rootObject.GetOrAddComponent<PickupItem_Networked>();
            LandmarkItem_PickupItem lipi = rootObject.GetOrAddComponent<LandmarkItem_PickupItem>();

            BoxCollider bc = rootObject.GetComponent<BoxCollider>();
            if (bc == null)
            {
                Collider existingCollider = rootObject.GetComponent<Collider>();
                if (existingCollider == null)
                {
                    bc = rootObject.AddComponent<BoxCollider>();
                }
                else
                {
                    existingCollider.isTrigger = false;
                }
            }
            if (bc != null)
            {
                bc.isTrigger = false;
            }

            rootObject.GetOrAddComponent<RaycastInteractable>();

            YieldHandler yh = rootObject.GetOrAddComponent<YieldHandler>();
            SO_ItemYield yieldAsset = CreateRuntimeAsset<SO_ItemYield>(rootObject);
            if (yieldAsset.yieldAssets == null) yieldAsset.yieldAssets = new List<Cost>();
            else yieldAsset.yieldAssets.Clear();
            yh.yieldAsset = yieldAsset;
            pi.yieldHandler = yh;

            RandomDropper rd = rootObject.GetOrAddComponent<RandomDropper>();
            SO_RandomDropper dropperAsset = CreateRuntimeAsset<SO_RandomDropper>(rootObject);
            if (dropperAsset.randomizer == null) dropperAsset.randomizer = new Randomizer();

            Item_Base lootItem = ItemManager.GetItemByName(lootItemNameConst);
            if (lootItem != null)
            {
                List<RandomItem> randomItemList = new List<RandomItem> {
                    new RandomItem { obj = lootItem, weight = 100, spawnChance = itemChanceStr }
                };
                dropperAsset.randomizer.items = randomItemList.ToArray();
            }
            else
            {
                // Debug.LogWarning($"{BetterTrees.Main.logPrefix} ConfigurePickupLootObject '{objConfigName}': Item '{lootItemNameConst}' not found for loot on {rootObject.name}.");
                dropperAsset.randomizer.items = System.Array.Empty<RandomItem>();
            }
            Traverse.Create(rd).Field("amountOfItems").SetValue(amountToDrop);
            Traverse.Create(rd).Field("randomDropperAsset").SetValue(dropperAsset);
            pi.dropper = rd;

            pi.pickupItemType = PickupItemType.Default;
            pi.canBePickedUp = true;
            pi.requiresHands = true;
            if (pi.itemInstance == null) pi.itemInstance = new ItemInstance();
            pi.itemInstance.Uses = 1;

            pi.networkID = pin;
            SetupNetwork(pin, lipi);

            // // Debug.Log($"{BetterTrees.Main.logPrefix} Successfully configured '{objConfigName}': {rootObject.name} with Tag: {rootObject.tag}, Layer: {LayerMask.LayerToName(rootObject.layer)}, LootItem: {lootItemNameConst}, DropAmount: {amountToDrop.minValue}-{amountToDrop.maxValue}, Chance: {itemChanceStr}");
        }


        static void ConfigureLogCollider(GameObject targetObject, int treeLyr)
        {
            // // Debug.Log($"{BetterTrees.Main.logPrefix} Configuring {targetObject.name} with tag {ChoppableTreesModConstants.Tag_Tree} and layer {LayerMask.LayerToName(treeLyr)}, removing specific components.");
            targetObject.tag = ChoppableTreesModConstants.Tag_Tree;
            targetObject.layer = treeLyr;

            Component[] componentsToRemove = new Component[] {
                targetObject.GetComponent<PickupItem>(),
                targetObject.GetComponent<HarvestableTree>(),
                targetObject.GetComponent<PickupItem_Networked>(),
                targetObject.GetComponent<LandmarkItem_PickupItem>(),
                targetObject.GetComponent<YieldHandler>(),
                targetObject.GetComponent<RandomDropper>()
            };
            foreach (var component in componentsToRemove.Where(c => c != null))
            {
                if (Application.isEditor && !Application.isPlaying)
                    Object.DestroyImmediate(component);
                else
                    Object.Destroy(component);
            }

            CapsuleCollider collider = targetObject.GetComponent<CapsuleCollider>();
            if (collider == null)
            {
                collider = targetObject.AddComponent<CapsuleCollider>();
                collider.height = 4f;
                collider.radius = 0.5f;
                collider.direction = 1;
                collider.center = Vector3.zero;
            }
            collider.enabled = true;
            collider.isTrigger = false;

            targetObject.GetOrAddComponent<RaycastInteractable>();
        }

        static void SetupBasicChoppable(GameObject targetObject, int treeLyr, int defaultLyr, int uses,
                                       List<CostData> yieldItems, List<DropData> dropItems, Interval_Int dropAmountPerHit,
                                       string particleEffect, ColliderData colliderConfig, string interactionColliderName, List<GameObject> processedObjects)
        {
            // // Debug.Log($"{BetterTrees.Main.logPrefix} SetupBasic (for Log): Configuring {targetObject.name}");

            PickupItem pi = targetObject.GetOrAddComponent<PickupItem>();
            HarvestableTree ht = targetObject.GetOrAddComponent<HarvestableTree>();
            PickupItem_Networked pin = targetObject.GetOrAddComponent<PickupItem_Networked>();
            LandmarkItem_PickupItem lipi = targetObject.GetOrAddComponent<LandmarkItem_PickupItem>();

            pi.pickupItemType = PickupItemType.Default;
            pi.canBePickedUp = true;
            pi.requiresHands = false;
            if (pi.itemInstance == null) pi.itemInstance = new ItemInstance();
            pi.itemInstance.Uses = 1;
            pi.networkID = pin;
            targetObject.GetOrAddComponent<BetterTreesChoppable>();

            YieldHandler yh = targetObject.GetOrAddComponent<YieldHandler>();
            pi.yieldHandler = yh;
            SO_ItemYield yieldAsset = CreateRuntimeAsset<SO_ItemYield>(targetObject);
            yieldAsset.yieldAssets = new List<Cost>();
            if (yieldItems != null)
            {
                foreach (var itemData in yieldItems)
                {
                    Item_Base item = ItemManager.GetItemByName(itemData.ItemName);
                    if (item != null)
                        yieldAsset.yieldAssets.Add(new Cost(item, itemData.Amount));
                    else
                        Debug.LogWarning($"{BetterTrees.Main.logPrefix} SetupBasic: Yield item '{itemData.ItemName}' not found for {targetObject.name}.");
                }
            }
            yh.yieldAsset = yieldAsset;

            RandomDropper rd = targetObject.GetOrAddComponent<RandomDropper>();
            pi.dropper = rd;
            SO_RandomDropper dropperAsset = CreateRuntimeAsset<SO_RandomDropper>(targetObject);
            if (dropperAsset.randomizer == null) dropperAsset.randomizer = new Randomizer();
            dropperAsset.randomizer.items = System.Array.Empty<RandomItem>();
            if (dropItems != null)
            {
                List<RandomItem> randomItemList = new List<RandomItem>();
                foreach (var dropData in dropItems)
                {
                    Item_Base item = ItemManager.GetItemByName(dropData.ItemName);
                    if (item != null)
                        randomItemList.Add(new RandomItem { obj = item, weight = dropData.Weight, spawnChance = dropData.Chance });
                    else
                        Debug.LogWarning($"{BetterTrees.Main.logPrefix} SetupBasic: Drop item '{dropData.ItemName}' not found for {targetObject.name}.");
                }
                dropperAsset.randomizer.items = randomItemList.ToArray();
            }
            Traverse.Create(rd).Field("amountOfItems").SetValue(dropAmountPerHit);
            Traverse.Create(rd).Field("randomDropperAsset").SetValue(dropperAsset);

            Traverse.Create(ht).Field("pickupItem").SetValue(pi);
            Traverse.Create(ht).Field("hideOnDepleted").SetValue(true);
            Traverse.Create(ht).Field("particleSystemName").SetValue(particleEffect);

            Transform[] particlePositionsBasic = targetObject.GetComponentsInChildren<Transform>(true)
                .Where(c => c.name.ToLower().Contains("particle_spawn") || c.name.ToLower().Contains("particleorigin")).ToArray();
            if (particlePositionsBasic.Length == 0)
                particlePositionsBasic = targetObject.GetComponentsInChildren<Transform>(true).Where(c => c.name.ToLower().Contains("particle")).ToArray();
            Traverse.Create(ht).Field("particleSpawnPositions").SetValue(particlePositionsBasic.Length > 0 ? particlePositionsBasic : new[] { targetObject.transform });

            SetupInteractionCollider(targetObject, treeLyr, colliderConfig, interactionColliderName);

            targetObject.layer = defaultLyr;
            targetObject.tag = ChoppableTreesModConstants.Tag_Tree;

            Transform interactionColliderGO_Transform = targetObject.transform.Find(interactionColliderName);
            if (interactionColliderGO_Transform != null && interactionColliderGO_Transform.gameObject != targetObject)
            {
                RaycastInteractable rootBasicRI = targetObject.GetComponent<RaycastInteractable>();
                if (rootBasicRI != null)
                {
                    if (Application.isEditor && !Application.isPlaying) Object.DestroyImmediate(rootBasicRI); else Object.Destroy(rootBasicRI);
                }
                if (interactionColliderGO_Transform.parent == targetObject)
                {
                    foreach (var rootCol in targetObject.GetComponents<Collider>().Where(c => c.gameObject == targetObject))
                    {
                        if (Application.isEditor && !Application.isPlaying) Object.DestroyImmediate(rootCol); else Object.Destroy(rootCol);
                    }
                }
            }
            SetupNetwork(pin, lipi);
        }

        static void ConfigureChoppableBaseLogic(GameObject rootObject, int uses, string particleEffect,
                                               out PickupItem pi, out HarvestableTree ht,
                                               out PickupItem_Networked pin, out LandmarkItem_PickupItem lipi,
                                               out YieldHandler yh, out RandomDropper rd)
        {
            pi = rootObject.GetOrAddComponent<PickupItem>();
            ht = rootObject.GetOrAddComponent<HarvestableTree>();
            pin = rootObject.GetOrAddComponent<PickupItem_Networked>();
            lipi = rootObject.GetOrAddComponent<LandmarkItem_PickupItem>();

            pi.pickupItemType = PickupItemType.Default;
            pi.canBePickedUp = true;
            pi.requiresHands = false;
            if (pi.itemInstance == null) pi.itemInstance = new ItemInstance();
            pi.itemInstance.Uses = 1;
            pi.networkID = pin;
            rootObject.GetOrAddComponent<BetterTreesChoppable>();

            yh = rootObject.GetOrAddComponent<YieldHandler>();
            pi.yieldHandler = yh;

            rd = rootObject.GetOrAddComponent<RandomDropper>();
            pi.dropper = rd;
            SO_RandomDropper dropperAsset = CreateRuntimeAsset<SO_RandomDropper>(rootObject);
            if (dropperAsset.randomizer == null) dropperAsset.randomizer = new Randomizer();
            dropperAsset.randomizer.items = System.Array.Empty<RandomItem>();
            Traverse.Create(rd).Field("amountOfItems").SetValue(new Interval_Int { minValue = 0, maxValue = 0 });
            Traverse.Create(rd).Field("randomDropperAsset").SetValue(dropperAsset);

            Traverse.Create(ht).Field("pickupItem").SetValue(pi);
            Traverse.Create(ht).Field("hideOnDepleted").SetValue(true);
            Traverse.Create(ht).Field("particleSystemName").SetValue(particleEffect);

            Transform[] particlePositions = rootObject.GetComponentsInChildren<Transform>(true)
                .Where(c => c.name.ToLower().Contains("particle_spawn") || c.name.ToLower().Contains("particleorigin"))
                .ToArray();
            if (particlePositions.Length == 0)
            {
                particlePositions = rootObject.GetComponentsInChildren<Transform>(true)
                    .Where(c => c.name.ToLower().Contains("particle"))
                    .ToArray();
            }
            Traverse.Create(ht).Field("particleSpawnPositions").SetValue(particlePositions.Length > 0 ? particlePositions : new[] { rootObject.transform });

            SetupNetwork(pin, lipi);
        }

        static void ConfigureBigPalmSpecial(GameObject rootObject, int treeLyr, int defaultLyr, List<MinMaxLootConfig> lootList, int uses, string particleEffect, List<GameObject> processedObjects)
        {
            // Debug.Log($"{BetterTrees.Main.logPrefix} Configuring BigPalmSpecial: {rootObject.name}");

            ConfigureChoppableBaseLogic(rootObject, uses, particleEffect,
                                        out _, out _, out _, out _,
                                        out YieldHandler yh, out _);

            SO_ItemYield yieldAsset = CreateRuntimeAsset<SO_ItemYield>(rootObject);
            yieldAsset.yieldAssets = new List<Cost>();
            if (lootList != null)
            {
                foreach (var lootConfig in lootList)
                {
                    Item_Base item = ItemManager.GetItemByName(lootConfig.ItemName);
                    if (item == null)
                    {
                        Debug.LogWarning($"{BetterTrees.Main.logPrefix} ConfigureBigPalmSpecial: Yield item '{lootConfig.ItemName}' not found for {rootObject.name}.");
                        continue;
                    }
                    int amountToDrop = lootConfig.MinAmount;
                    if (lootConfig.MaxAmount > lootConfig.MinAmount && UnityEngine.Random.Range(1, 101) <= lootConfig.ChanceForBonus)
                    {
                        amountToDrop += UnityEngine.Random.Range(1, (lootConfig.MaxAmount - lootConfig.MinAmount) + 1);
                    }
                    if (amountToDrop > 0)
                    {
                        yieldAsset.yieldAssets.Add(new Cost(item, amountToDrop));
                    }
                }
            }
            yh.yieldAsset = yieldAsset;

            string[] collisionObjectNames = { ChoppableTreesModConstants.ColliderName_BigPalm_CollisionObject0, ChoppableTreesModConstants.ColliderName_BigPalm_CollisionObject1 };
            bool collisionObjectConfigured = false;
            foreach (string colName in collisionObjectNames)
            {
                Transform colTransform = rootObject.transform.Find(colName);
                if (colTransform != null)
                {
                    GameObject colGO = colTransform.gameObject;
                    // Debug.Log($"{BetterTrees.Main.logPrefix} Configuring BigPalm collision object: {colGO.name}");
                    colGO.tag = ChoppableTreesModConstants.Tag_Tree;
                    colGO.layer = treeLyr;
                    colGO.GetOrAddComponent<RaycastInteractable>();

                    foreach (Collider actualCollider in colGO.GetComponents<Collider>())
                    {
                        actualCollider.enabled = true;
                        actualCollider.isTrigger = false;
                    }
                    collisionObjectConfigured = true;

                    if (!processedObjects.Contains(colGO))
                    {
                        processedObjects.Add(colGO);
                        // Debug.Log($"{BetterTrees.Main.logPrefix} ConfigureBigPalmSpecial for '{rootObject.name}': Marked child '{colGO.name}' as processed.");
                    }
                }
                else
                {
                    Debug.LogWarning($"{BetterTrees.Main.logPrefix} BigPalmSpecial: Collision object '{colName}' not found under {rootObject.name}.");
                }
            }

            if (!collisionObjectConfigured)
            {
                Debug.LogWarning($"{BetterTrees.Main.logPrefix} BigPalmSpecial: NO collision objects ({string.Join("/", collisionObjectNames)}) found for {rootObject.name}. Interaction might not work as intended.");
            }

            RaycastInteractable rootRI = rootObject.GetComponent<RaycastInteractable>();
            if (rootRI != null)
            {
                if (Application.isEditor && !Application.isPlaying) Object.DestroyImmediate(rootRI); else Object.Destroy(rootRI);
            }
            foreach (var col in rootObject.GetComponents<Collider>().ToList())
            {
                if (Application.isEditor && !Application.isPlaying) Object.DestroyImmediate(col); else Object.Destroy(col);
            }

            rootObject.tag = ChoppableTreesModConstants.Tag_Tree;
            rootObject.layer = defaultLyr;

            // Debug.Log($"{BetterTrees.Main.logPrefix} Successfully configured BigPalmSpecial: {rootObject.name}. Interaction via {string.Join("/", collisionObjectNames)}.");
        }

        static void ConfigureDeadTreeWithSpecificColliders(GameObject rootObject, int treeLyr, int defaultLyr, List<MinMaxLootConfig> lootList, int uses, string particleEffect, List<GameObject> processedObjects)
        {
            // Debug.Log($"{BetterTrees.Main.logPrefix} Configuring DeadTreeWithSpecificColliders: {rootObject.name}");

            ConfigureChoppableBaseLogic(rootObject, uses, particleEffect,
                                        out _, out _, out _, out _,
                                        out YieldHandler yh, out _);

            SO_ItemYield yieldAsset = CreateRuntimeAsset<SO_ItemYield>(rootObject);
            yieldAsset.yieldAssets = new List<Cost>();
            if (lootList != null)
            {
                foreach (var lootConfig in lootList)
                {
                    Item_Base item = ItemManager.GetItemByName(lootConfig.ItemName);
                    if (item == null)
                    {
                        Debug.LogWarning($"{BetterTrees.Main.logPrefix} ConfigureDeadTree: Yield item '{lootConfig.ItemName}' not found for {rootObject.name}.");
                        continue;
                    }
                    int amountToDrop = lootConfig.MinAmount;
                    if (lootConfig.MaxAmount > lootConfig.MinAmount && UnityEngine.Random.Range(1, 101) <= lootConfig.ChanceForBonus)
                    {
                        amountToDrop += UnityEngine.Random.Range(1, (lootConfig.MaxAmount - lootConfig.MinAmount) + 1);
                    }
                    if (amountToDrop > 0)
                    {
                        yieldAsset.yieldAssets.Add(new Cost(item, amountToDrop));
                    }
                }
            }
            yh.yieldAsset = yieldAsset;

            string[] specificColliderNames = {
                ChoppableTreesModConstants.ColliderName_DeadTree_Collider1,
                ChoppableTreesModConstants.ColliderName_DeadTree_Collider2,
                ChoppableTreesModConstants.ColliderName_DeadTree_Collider3
            };
            bool specificCollidersConfigured = false;

            foreach (string colName in specificColliderNames)
            {
                Transform colTransform = rootObject.transform.Find(colName);
                if (colTransform != null)
                {
                    GameObject colGO = colTransform.gameObject;
                    //  // Debug.Log($"{BetterTrees.Main.logPrefix} Configuring DeadTree specific collider: {colGO.name}"); 
                    colGO.tag = ChoppableTreesModConstants.Tag_Tree;
                    colGO.layer = treeLyr;
                    colGO.GetOrAddComponent<RaycastInteractable>();

                    foreach (Collider actualCollider in colGO.GetComponents<Collider>())
                    {
                        actualCollider.enabled = true;
                        actualCollider.isTrigger = false;
                    }
                    specificCollidersConfigured = true;

                    if (!processedObjects.Contains(colGO))
                    {
                        processedObjects.Add(colGO);
                        // // Debug.Log($"{BetterTrees.Main.logPrefix} ConfigureDeadTree for '{rootObject.name}': Marked child '{colGO.name}' as processed."); 
                    }
                }
                else
                {
                    //Debug.LogWarning($"{BetterTrees.Main.logPrefix} DeadTree: Specific collider '{colName}' not found under {rootObject.name}."); 
                }
            }

            if (!specificCollidersConfigured)
            {
                //  Debug.LogWarning($"{BetterTrees.Main.logPrefix} DeadTree: NO specific colliders ({string.Join("/", specificColliderNames)}) found for {rootObject.name}. Interaction might not work as intended.");
            }

            RaycastInteractable rootRI = rootObject.GetComponent<RaycastInteractable>();
            if (rootRI != null)
            {
                if (Application.isEditor && !Application.isPlaying) Object.DestroyImmediate(rootRI); else Object.Destroy(rootRI);
            }
            foreach (var col in rootObject.GetComponents<Collider>().ToList())
            {
                if (Application.isEditor && !Application.isPlaying) Object.DestroyImmediate(col); else Object.Destroy(col);
            }

            rootObject.tag = ChoppableTreesModConstants.Tag_Tree;
            rootObject.layer = defaultLyr;

            // // Debug.Log($"{BetterTrees.Main.logPrefix} Successfully configured DeadTree: {rootObject.name}. Interaction via specific children: {string.Join("/", specificColliderNames)}."); 
        }


        static void ConfigureTemplateObject(GameObject rootObject, int treeLyr, int defaultLyr, List<MinMaxLootConfig> lootList, int uses, string particleEffect, bool adjustBananaBushCollider, ColliderType? defaultColliderForCreation, List<GameObject> processedObjects, bool preserveOriginalPhysicsColliders = false)
        {
            // // Debug.Log($"{BetterTrees.Main.logPrefix} Configuring Template Object (MinMaxLoot): {rootObject.name}");

            // Исходные таблицы лута игровых деревьев не должны заменяться настройками мода
            string treeName = rootObject.name.ToLowerInvariant();
            bool nativeTreeLoot = treeName.Contains("birchtree_big") ||
                treeName.Contains("firtree_big") || treeName.Contains("pinetree_big") ||
                treeName.Contains("tp_pinetreesnowy");
            YieldHandler originalYield = nativeTreeLoot ? rootObject.GetComponent<YieldHandler>() : null;
            SO_ItemYield nativeYieldAsset = originalYield != null ? originalYield.yieldAsset : null;
            bool hasNativeYield = nativeYieldAsset != null && nativeYieldAsset.yieldAssets != null &&
                nativeYieldAsset.yieldAssets.Count > 0;
            RandomDropper originalDropper = nativeTreeLoot ? rootObject.GetComponent<RandomDropper>() : null;
            SO_RandomDropper nativeDropAsset = originalDropper != null ?
                Traverse.Create(originalDropper).Field("randomDropperAsset").GetValue<SO_RandomDropper>() : null;
            Interval_Int nativeDropCount = originalDropper != null ?
                Traverse.Create(originalDropper).Field("amountOfItems").GetValue<Interval_Int>() : default(Interval_Int);

            ConfigureChoppableBaseLogic(rootObject, uses, particleEffect,
                                        out _, out _, out _, out _,
                                        out YieldHandler yh, out RandomDropper configuredDropper);

            SO_ItemYield yieldAsset = CreateRuntimeAsset<SO_ItemYield>(rootObject);
            yieldAsset.yieldAssets = new List<Cost>();
            if (lootList != null)
            {
                foreach (var lootConfig in lootList)
                {
                    Item_Base item = ItemManager.GetItemByName(lootConfig.ItemName);
                    if (item == null)
                    {
                        //  Debug.LogWarning($"{BetterTrees.Main.logPrefix} ConfigureTemplateObject: Yield item '{lootConfig.ItemName}' not found for {rootObject.name}.");
                        continue;
                    }
                    int amountToDrop = lootConfig.MinAmount;
                    if (lootConfig.MaxAmount > lootConfig.MinAmount &&
                        UnityEngine.Random.Range(1, 101) <= lootConfig.ChanceForBonus)
                    {
                        amountToDrop += UnityEngine.Random.Range(1, (lootConfig.MaxAmount - lootConfig.MinAmount) + 1);
                    }
                    if (amountToDrop > 0)
                    {
                        yieldAsset.yieldAssets.Add(new Cost(item, amountToDrop));
                    }
                }
            }
            yh.yieldAsset = hasNativeYield ? nativeYieldAsset : yieldAsset;
            if (hasNativeYield) yh.ResetYield();
            if (nativeDropAsset != null)
            {
                Traverse.Create(configuredDropper).Field("randomDropperAsset").SetValue(nativeDropAsset);
                Traverse.Create(configuredDropper).Field("amountOfItems").SetValue(nativeDropCount);
            }

            GameObject colliderHolderGO = null;
            List<Collider> collidersToCopyFrom = new List<Collider>();
            bool foundColliderOnRoot = false;
            GameObject childColliderSourceGameObject = null;

            Collider[] rootObjectColliders = rootObject.GetComponents<Collider>();
            if (rootObjectColliders.Length > 0)
            {
                collidersToCopyFrom.AddRange(rootObjectColliders.Where(c => !c.isTrigger));
                if (collidersToCopyFrom.Any()) foundColliderOnRoot = true;
            }

            if (!foundColliderOnRoot)
            {
                Collider firstChildCollider = rootObject.GetComponentsInChildren<Collider>(true)
                                                     .FirstOrDefault(c => c.gameObject != rootObject && !c.isTrigger);
                if (firstChildCollider == null)
                {
                    firstChildCollider = rootObject.GetComponentsInChildren<Collider>(true)
                                                 .FirstOrDefault(c => c.gameObject != rootObject);
                }
                if (firstChildCollider != null)
                {
                    collidersToCopyFrom.Add(firstChildCollider);
                    childColliderSourceGameObject = firstChildCollider.gameObject;
                }
            }

            colliderHolderGO = new GameObject(ChoppableTreesModConstants.ColliderName_InteractionNew);
            colliderHolderGO.transform.SetParent(rootObject.transform, false);
            colliderHolderGO.transform.localPosition = Vector3.zero;
            colliderHolderGO.transform.localRotation = Quaternion.identity;
            colliderHolderGO.transform.localScale = Vector3.one;

            if (childColliderSourceGameObject != null)
            {
                Transform sourceTransform = childColliderSourceGameObject.transform;
                colliderHolderGO.transform.position = sourceTransform.position;
                colliderHolderGO.transform.rotation = sourceTransform.rotation;

                Vector3 rootScale = rootObject.transform.lossyScale;
                Vector3 sourceScale = sourceTransform.lossyScale;
                colliderHolderGO.transform.localScale = new Vector3(
                    SafeDivide(sourceScale.x, rootScale.x),
                    SafeDivide(sourceScale.y, rootScale.y),
                    SafeDivide(sourceScale.z, rootScale.z));
            }

            bool successfullyCopiedOrCreatedCollider = false;
            if (collidersToCopyFrom.Any())
            {
                foreach (Collider colToCopy in collidersToCopyFrom)
                {
                    Collider copied = CopyColliderToGameObject(colToCopy, colliderHolderGO);
                    if (copied != null)
                    {
                        successfullyCopiedOrCreatedCollider = true;

                        if (childColliderSourceGameObject != null && colToCopy.gameObject == childColliderSourceGameObject)
                        {
                            if (!processedObjects.Contains(childColliderSourceGameObject))
                            {
                                processedObjects.Add(childColliderSourceGameObject);
                                //  // Debug.Log($"{BetterTrees.Main.logPrefix} ConfigureTemplateObject for '{rootObject.name}': Marked child '{childColliderSourceGameObject.name}' as processed because its collider was used.");
                            }
                        }
                        if (!preserveOriginalPhysicsColliders)
                        {
                            if (Application.isEditor && !Application.isPlaying)
                                Object.DestroyImmediate(colToCopy);
                            else
                                Object.Destroy(colToCopy);
                        }
                    }
                }
            }

            if (!successfullyCopiedOrCreatedCollider)
            {
                ColliderType typeToCreate = defaultColliderForCreation ?? ColliderType.Box;

                if (typeToCreate == ColliderType.Capsule)
                {
                    CapsuleCollider defaultCap = colliderHolderGO.AddComponent<CapsuleCollider>();
                    defaultCap.radius = 0.5f;
                    defaultCap.height = 2f;
                    defaultCap.direction = 1;
                    //  Debug.LogWarning($"{BetterTrees.Main.logPrefix} ConfigureTemplateObject: No suitable collider found for {rootObject.name}. Created default CapsuleCollider on '{colliderHolderGO.name}'.");
                }
                else
                {
                    BoxCollider defaultBox = colliderHolderGO.AddComponent<BoxCollider>();
                    defaultBox.size = Vector3.one;
                    // Debug.LogWarning($"{BetterTrees.Main.logPrefix} ConfigureTemplateObject: No suitable collider found for {rootObject.name}. Created default BoxCollider on '{colliderHolderGO.name}'.");
                }
                successfullyCopiedOrCreatedCollider = true;
            }

            if (adjustBananaBushCollider && successfullyCopiedOrCreatedCollider && colliderHolderGO != null)
            {
                // // Debug.Log($"{BetterTrees.Main.logPrefix} Attempting to adjust Banana_Bush collider on {colliderHolderGO.name}");
                foreach (Collider colInHolder in colliderHolderGO.GetComponents<Collider>())
                {
                    if (colInHolder is BoxCollider boxCol)
                    {
                        float originalHeight = boxCol.size.y;
                        float newHeight = Mathf.Max(originalHeight, 1.0f) * 1.5f;
                        boxCol.size = new Vector3(boxCol.size.x, newHeight, boxCol.size.z);
                        //  // Debug.Log($"{BetterTrees.Main.logPrefix} Adjusted Banana_Bush BoxCollider height from {originalHeight} to: {newHeight}");
                    }
                    else if (colInHolder is CapsuleCollider capCol)
                    {
                        float originalHeight = capCol.height;
                        capCol.height *= 1.5f;
                        // // Debug.Log($"{BetterTrees.Main.logPrefix} Adjusted Banana_Bush CapsuleCollider height from {originalHeight} to: {capCol.height}");из Main
                    }
                }
            }

            if (successfullyCopiedOrCreatedCollider && colliderHolderGO != null)
            {
                colliderHolderGO.tag = ChoppableTreesModConstants.Tag_Tree;
                colliderHolderGO.layer = treeLyr;
                colliderHolderGO.GetOrAddComponent<RaycastInteractable>();
                foreach (Collider colInHolder in colliderHolderGO.GetComponents<Collider>())
                {
                    colInHolder.enabled = true;
                    colInHolder.isTrigger = false;
                }
            }
            else if (colliderHolderGO != null)
            {
                if (Application.isEditor && !Application.isPlaying) Object.DestroyImmediate(colliderHolderGO); else Object.Destroy(colliderHolderGO);
            }

            rootObject.tag = ChoppableTreesModConstants.Tag_Tree;
            rootObject.layer = defaultLyr;

            RaycastInteractable rootRI = rootObject.GetComponent<RaycastInteractable>();
            if (rootRI != null)
            {
                if (Application.isEditor && !Application.isPlaying) Object.DestroyImmediate(rootRI); else Object.Destroy(rootRI);
            }

            if (!preserveOriginalPhysicsColliders)
            {
                foreach (Collider col in rootObject.GetComponents<Collider>().ToList())
                {
                    if (Application.isEditor && !Application.isPlaying)
                        Object.DestroyImmediate(col);
                    else
                        Object.Destroy(col);
                }
            }
            // // Debug.Log($"{BetterTrees.Main.logPrefix} Successfully configured Template Object (MinMaxLoot): {rootObject.name}. Interaction via '{colliderHolderGO?.name ?? "UNKNOWN_COLLIDER_HOLDER"}'.");
        }

        static void SetupInteractionCollider(GameObject parentObject, int treeLyr, ColliderData config, string colliderObjectName)
        {
            Transform colliderT = parentObject.transform.Find(colliderObjectName);
            GameObject colliderGO;
            if (colliderT == null)
            {
                colliderGO = new GameObject(colliderObjectName);
                colliderGO.transform.SetParent(parentObject.transform, false);
                colliderGO.transform.localPosition = Vector3.zero;
                colliderGO.transform.localRotation = Quaternion.identity;
                colliderGO.transform.localScale = Vector3.one;
            }
            else
            {
                colliderGO = colliderT.gameObject;
            }

            foreach (Collider col in colliderGO.GetComponents<Collider>())
            {
                if (Application.isEditor && !Application.isPlaying) Object.DestroyImmediate(col); else Object.Destroy(col);
            }

            Collider newCol = null;
            if (config.Type == ColliderType.Capsule)
            {
                CapsuleCollider cap = colliderGO.AddComponent<CapsuleCollider>();
                cap.height = config.Param1;
                cap.radius = config.Param2;
                cap.direction = (int)config.Param3;
                cap.center = Vector3.zero;
                newCol = cap;
            }
            else if (config.Type == ColliderType.Box)
            {
                BoxCollider box = colliderGO.AddComponent<BoxCollider>();
                box.size = new Vector3(config.Param1, config.Param2, config.Param3);
                box.center = Vector3.zero;
                newCol = box;
            }

            if (newCol != null)
            {
                newCol.enabled = true;
                newCol.isTrigger = false;
                colliderGO.layer = treeLyr;
                colliderGO.tag = ChoppableTreesModConstants.Tag_Tree;
                colliderGO.GetOrAddComponent<RaycastInteractable>();
            }
        }

        static float SafeDivide(float value, float divisor)
        {
            return Mathf.Abs(divisor) > 0.0001f ? value / divisor : value;
        }

        static Collider CopyColliderToGameObject(Collider sourceCollider, GameObject targetGO)
        {
            if (sourceCollider == null || targetGO == null) return null;
            Collider newCollider = null;
            System.Type colliderType = sourceCollider.GetType();

            if (sourceCollider is BoxCollider boxSource)
            {
                BoxCollider boxTarget = targetGO.AddComponent<BoxCollider>();
                boxTarget.center = boxSource.center; boxTarget.size = boxSource.size; newCollider = boxTarget;
            }
            else if (sourceCollider is SphereCollider sphereSource)
            {
                SphereCollider sphereTarget = targetGO.AddComponent<SphereCollider>();
                sphereTarget.center = sphereSource.center; sphereTarget.radius = sphereSource.radius; newCollider = sphereTarget;
            }
            else if (sourceCollider is CapsuleCollider capsuleSource)
            {
                CapsuleCollider capsuleTarget = targetGO.AddComponent<CapsuleCollider>();
                capsuleTarget.center = capsuleSource.center; capsuleTarget.radius = capsuleSource.radius;
                capsuleTarget.height = capsuleSource.height; capsuleTarget.direction = capsuleSource.direction; newCollider = capsuleTarget;
            }
            else if (sourceCollider is MeshCollider meshSource)
            {
                MeshCollider meshTarget = targetGO.AddComponent<MeshCollider>();
                meshTarget.sharedMesh = meshSource.sharedMesh;
                meshTarget.convex = meshSource.convex;
                newCollider = meshTarget;
            }

            if (newCollider != null)
            {
                newCollider.isTrigger = sourceCollider.isTrigger;
                newCollider.sharedMaterial = sourceCollider.sharedMaterial;
                newCollider.enabled = sourceCollider.enabled;
            }
            else
            {
                //Debug.LogWarning($"{BetterTrees.Main.logPrefix} CopyColliderToGameObject: Unsupported collider type: {colliderType} on {sourceCollider.gameObject.name} for target {targetGO.name}.");
            }
            return newCollider;
        }

        static void SetupNetwork(PickupItem_Networked pin, LandmarkItem_PickupItem lipi)
        {
            if (pin == null || lipi == null)
            {
                // Debug.LogWarning($"{BetterTrees.Main.logPrefix} SetupNetwork for {lipi?.gameObject.name ?? pin?.gameObject.name ?? "UNKNOWN"}: One or more network components are null. Skipping.");
                return;
            }
            pin.spawnType = (ObjectSpawnType)2;
            Traverse.Create(pin).Field("stopTrackingOnPickup").SetValue(false);
            Traverse.Create(pin).Field("stopTrackUseRPC").SetValue(true);

            Traverse.Create(lipi).Field("resetOnLandmarkReset").SetValue(true);
            Traverse.Create(lipi).Field("connectedBehaviourID").SetValue(pin);
        }
    }

    public static class GameObjectExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }
            return component;
        }
    }


}
