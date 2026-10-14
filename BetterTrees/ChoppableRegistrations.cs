using UnityEngine;
using System.Collections.Generic;

namespace BetterTrees
{
    public partial class Patch_Landmark_Initialize_Choppables
    {
        private static readonly string[] choppableKeywords = {
 			"balboa_bigbush",        "banana_bush",          "bed_simple",           "bigpalm",
			"bird_nest",             "birchtree_big",        "buoy",                 "campfire",
			"crate_big",             "crate_small",          "cropplot_floor",       "deadtree",
			"firtree_big",           "fl_cloth",             "fl_crate",             "fl_foundation",
			"fl_pillar",             "fl_plank",             "fl_plywood",           "fl_roof",
			"log",                   "parabol",              "parabol_3",            "pinetree_big",
			"reefhuts_pillar",       "reefhuts_wall1",       "reefhuts_wall2",       "reefhuts_wall3",
			"reefhuts_wall4",        "reefhuts_wall5",       "scarecrow",            "tp_moontown_barrel01",
			"tp_moontown_barrel02",  "tp_moontown_barrel03", "tp_moontown_barrel04", "tp_moontown_sealedcrate01",
			"tp_moontown_sealedcrate02", "tp_moontown_sealedcrate03", "tp_moontown_tarpcrate01", "tp_moontown_tarpcrate02",
			"tp_moontown_tarpcrate03", "tp_pinetreesnowy", "vp_metalcrate", "table_floor", "reefhuts_stick", "reefhuts_plasticwall"
			
        };

        private static readonly string[] exclusionKeywords = {
            "deadtree_treestumps",
            "pickup_landmark_plank", "parabol_3_collider", "parabol_2_collider", "parabol_1_collider", "tree_log_01", "tree_log_02", "pickup_landmark_tree_tangaroa_1", "pickup_landmark_tree_tangaroa", "tangaroa", "dialogue", "olofslog", "notebookpickups" 
        };

        static Patch_Landmark_Initialize_Choppables()
        {
            objectConfigurators = new Dictionary<string, System.Action<GameObject, int, int, string, List<GameObject>>>();

            objectConfigurators.Add("log", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                if (nameLower.Contains("log_collider") && obj.name == "Log_Collider")
                {
                    ConfigureLogCollider(obj, treeLyr); 
                }
                else
                {
                    SetupBasicChoppable(obj, treeLyr, defLyr, 8,
                       new List<CostData> { new CostData(ChoppableTreesModConstants.PlankItemName, 6) },
                        null, new Interval_Int { minValue = 0, maxValue = 0 }, ChoppableTreesModConstants.Particle_TreeBreak,
                        new ColliderData(ColliderType.Capsule, 4f, 0.5f, 1), ChoppableTreesModConstants.ColliderName_LogInteraction, processedObjects);
                }
            });
            
            objectConfigurators.Add("parabol_3", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                if (nameLower.Contains("parabol_3_model") && obj.name == "Parabol_3_Model")
                {
                    ConfigureLogCollider(obj, treeLyr); 
                }
                else
                {
                    SetupBasicChoppable(obj, treeLyr, defLyr, 8,
                       new List<CostData> { 
                           new CostData(ChoppableTreesModConstants.PlankItemName, 2),
                           new CostData(ChoppableTreesModConstants.ScrapItemName, 4),
						   new CostData(ChoppableTreesModConstants.CircuitBoardItemName, 1)
                       },
                       null, new Interval_Int { minValue = 0, maxValue = 0 }, ChoppableTreesModConstants.Particle_TreeBreak,
                       new ColliderData(ColliderType.Capsule, 4f, 0.5f, 1), ChoppableTreesModConstants.ColliderName_LogInteraction, processedObjects);
                }
            });

            objectConfigurators.Add("bigpalm", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 6, 15, 35),
                    new MinMaxLootConfig(ChoppableTreesModConstants.PalmLeafItemName, 6, 15, 35)
                };
                ConfigureBigPalmSpecial(obj, treeLyr, defLyr, loot, 10, ChoppableTreesModConstants.Particle_TreeBreak, processedObjects);
            });
        
    objectConfigurators.Add("tp_moontown_tarpcrate03", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
        List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 5, 7, 100),
            new MinMaxLootConfig(ChoppableTreesModConstants.NailItemName, 5, 6, 90),
            new MinMaxLootConfig(ChoppableTreesModConstants.RopeItemName, 1, 4, 90),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 2, 3, 90),
            new MinMaxLootConfig(ChoppableTreesModConstants.TitaniumIngotItemName, 0, 2, 10),
            new MinMaxLootConfig(ChoppableTreesModConstants.CircuitBoardItemName, 0, 1, 15),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_TitaniumToolsItemName, 0, 1, 10),
            new MinMaxLootConfig(ChoppableTreesModConstants.Battery_AdvancedItemName, 0, 1, 10),
            new MinMaxLootConfig(ChoppableTreesModConstants.MetalIngotItemName, 0, 3, 20),
            new MinMaxLootConfig(ChoppableTreesModConstants.ExplosivePowderItemName, 0, 1, 10),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_AnchorStationaryAdvancedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_AntennaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_BackpackAdvancedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_BatteryAdvancedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_BatteryChargerItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_BiofuelExtractorItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_BiofuelExtractorAdvancedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_CanteenItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Cropplot_Large_Tier3ItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Cropplot_Medium_Tier3ItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Cropplot_Small_Tier3ItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_DetailPlankItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_ElectricGrillItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_ElectricPurifierItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_EngineControlsItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_FireworkItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_FueltankItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_HeadLightItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_HeadLight_AdvancedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_MacheteItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_MetalDetectorItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_MotorWheelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Pipe_FuelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Pipe_WaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_RecieverItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_RecyclerItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_SmelterElectricItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_SteeringWheelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Storage_LargeItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_WaterTankItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_WindTurbineItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_ZiplineBaseItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_ZiplineToolItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_ZiplineToolElectricItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.AxeItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Axe_StoneItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Axe_TitaniumItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.BinocularsItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.BowItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.CompassItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.FishingRodItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.FishingRod_MetalItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.HammerItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Hook_PlasticItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Hook_ScrapItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Hook_TitaniumItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.MacheteItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.MetalDetectorItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.NetCanisterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.NetGunItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PaddleItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PaintBrushItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.SharkBaitItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ShearItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ShovelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Spear_ScrapItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Spear_PlankItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.SweepNetItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Sword_TitaniumItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ThrowableAnchorItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Duffel_BackpackItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Tactical_BackpackItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Sturdy_HelmetItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Reinforced_HelmetItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Scrap_PulleyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Titanium_PulleyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Rugged_BootsItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Exploration_BootsItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Worn_GlovesItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Grip_GlovesItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Fortified_GlovesItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Reinforced_FlippersItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Carbon_Steel_FlippersItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Oxygen_CanisterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Oxygen_TankItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Thatch_BeltItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Tattered_BeltItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Leather_BeltItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Utility_BeltItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Caravan_BeltItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Titanium_ShovelItemName, 0, 1, 1)
        };
        ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 18, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
    });
    objectConfigurators.Add("tp_moontown_tarpcrate02", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
        List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 3, 4, 100),
            new MinMaxLootConfig(ChoppableTreesModConstants.RopeItemName, 1, 4, 90),
            new MinMaxLootConfig(ChoppableTreesModConstants.NailItemName, 5, 6, 90),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 5, 5, 100),
            new MinMaxLootConfig(ChoppableTreesModConstants.CopperIngotItemName, 0, 2, 25),
            new MinMaxLootConfig(ChoppableTreesModConstants.BoltItemName, 0, 3, 20),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_MetalDetectorItemName, 0, 1, 10),
            new MinMaxLootConfig(ChoppableTreesModConstants.GlassItemName, 0, 2, 20),
            new MinMaxLootConfig(ChoppableTreesModConstants.ScrapItemName, 0, 4, 15),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_AnchorStationaryAdvancedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_AntennaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_BackpackAdvancedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_BatteryAdvancedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_BatteryChargerItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_BiofuelExtractorItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_BiofuelExtractorAdvancedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_CanteenItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Cropplot_Large_Tier3ItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Cropplot_Medium_Tier3ItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Cropplot_Small_Tier3ItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_DetailPlankItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_ElectricGrillItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_ElectricPurifierItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_EngineControlsItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_FireworkItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_FueltankItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_HeadLightItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_HeadLight_AdvancedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_MacheteItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_MetalDetectorItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_MotorWheelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Pipe_FuelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Pipe_WaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_RecieverItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_RecyclerItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_SmelterElectricItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_SteeringWheelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Storage_LargeItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_WaterTankItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_WindTurbineItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_ZiplineBaseItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_ZiplineToolItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_ZiplineToolElectricItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.AxeItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Axe_StoneItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Axe_TitaniumItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.BinocularsItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.BowItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.CompassItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.FishingRodItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.FishingRod_MetalItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.HammerItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Hook_PlasticItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Hook_ScrapItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Hook_TitaniumItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.MacheteItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.MetalDetectorItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.NetCanisterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.NetGunItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PaddleItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PaintBrushItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.SharkBaitItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ShearItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ShovelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Spear_ScrapItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Spear_PlankItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.SweepNetItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Sword_TitaniumItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ThrowableAnchorItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Duffel_BackpackItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Tactical_BackpackItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Sturdy_HelmetItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Reinforced_HelmetItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Scrap_PulleyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Titanium_PulleyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Rugged_BootsItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Exploration_BootsItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Worn_GlovesItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Grip_GlovesItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Fortified_GlovesItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Reinforced_FlippersItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Carbon_Steel_FlippersItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Oxygen_CanisterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Oxygen_TankItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Thatch_BeltItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Tattered_BeltItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Leather_BeltItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Utility_BeltItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Caravan_BeltItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Titanium_ShovelItemName, 0, 1, 1)
        };
        ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 18, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
    });
    objectConfigurators.Add("tp_moontown_tarpcrate01", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
        List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 5, 5, 100),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 5, 5, 100),
            new MinMaxLootConfig(ChoppableTreesModConstants.RopeItemName, 1, 4, 90),
            new MinMaxLootConfig(ChoppableTreesModConstants.HealingSalveItemName, 0, 1, 20),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 0, 2, 25),
            new MinMaxLootConfig(ChoppableTreesModConstants.NailItemName, 5, 6, 90),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_AnchorStationaryAdvancedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_AntennaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_BackpackAdvancedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_BatteryAdvancedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_BatteryChargerItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_BiofuelExtractorItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_BiofuelExtractorAdvancedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_CanteenItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Cropplot_Large_Tier3ItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Cropplot_Medium_Tier3ItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Cropplot_Small_Tier3ItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_DetailPlankItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_ElectricGrillItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_ElectricPurifierItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_EngineControlsItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_FireworkItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_FueltankItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_HeadLightItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_HeadLight_AdvancedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_MacheteItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_MetalDetectorItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_MotorWheelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Pipe_FuelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Pipe_WaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_RecieverItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_RecyclerItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_SmelterElectricItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_SteeringWheelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_Storage_LargeItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_WaterTankItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_WindTurbineItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_ZiplineBaseItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_ZiplineToolItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Blueprint_ZiplineToolElectricItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.AxeItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Axe_StoneItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Axe_TitaniumItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.BinocularsItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.BowItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.CompassItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.FishingRodItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.FishingRod_MetalItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.HammerItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Hook_PlasticItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Hook_ScrapItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Hook_TitaniumItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.MacheteItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.MetalDetectorItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.NetCanisterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.NetGunItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PaddleItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PaintBrushItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.SharkBaitItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ShearItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ShovelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Spear_ScrapItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Spear_PlankItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.SweepNetItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Sword_TitaniumItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ThrowableAnchorItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Duffel_BackpackItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Tactical_BackpackItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Sturdy_HelmetItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Reinforced_HelmetItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Scrap_PulleyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Titanium_PulleyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Rugged_BootsItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Exploration_BootsItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Worn_GlovesItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Grip_GlovesItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Fortified_GlovesItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Reinforced_FlippersItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Carbon_Steel_FlippersItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Oxygen_CanisterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Oxygen_TankItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Thatch_BeltItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Tattered_BeltItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Leather_BeltItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Utility_BeltItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Caravan_BeltItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Titanium_ShovelItemName, 0, 1, 1)
        };
        ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 18, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
    });

    objectConfigurators.Add("tp_moontown_sealedcrate03", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
        List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 5, 5, 100),
            new MinMaxLootConfig(ChoppableTreesModConstants.Canteen_WaterItemName, 0, 1, 25),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_BBQItemName, 0, 1, 20),
            new MinMaxLootConfig(ChoppableTreesModConstants.OxygenBottleItemName, 0, 1, 15),
            new MinMaxLootConfig(ChoppableTreesModConstants.HealingSalve_GoodItemName, 0, 1, 15),
            new MinMaxLootConfig(ChoppableTreesModConstants.EggItemName, 0, 2, 25),
            new MinMaxLootConfig(ChoppableTreesModConstants.BananaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Berries_RedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Bucket_MilkItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Canteen_EmptyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Canteen_SaltWaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.CaveMushroomItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ChiliItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_CoconutChickenItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_EmptyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_HeadBrothItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_HeartyStewItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_LeftoverItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_SimpleFishStewItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_RootVegetableSoupItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_CatfishDeluxeItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_MushroomOmeletteItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_SalmonLsItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_SharkDinnerItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_SteakWithJamItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_SushiItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.CoconutItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_BeetItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_CatfishItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_DrumstickItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_GenericMeatItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_HerringItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_MackerelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_PomfretItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_PotatoItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_SalmonItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_SharkItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_TilapiaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlassItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_CoconutBeatItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_LeftoverItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_MangonanaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_RedbeetShotItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_RedMelonItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_SilverSmoothieItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_SimpleSmoothieItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_SpicyPineberryItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_StrawberryColadaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.EggItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.HoneyCombItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Jar_HoneyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.JuniperItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.MangoItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PineappleItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticBottle_EmptyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticBottle_SaltWaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticBottle_WaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticCup_EmptyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticCup_SaltWaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_BeetItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_CatfishItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_DrumstickItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_GenericMeatItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_HerringItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_MackerelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_PomfretItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_PotatoItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_SalmonItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_SharkItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_TilapiaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.SilverAlgaeItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.StrawberryItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.TurmericItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.WatermelonItemName, 0, 1, 5)
        };
        ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 18, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
    });
    objectConfigurators.Add("tp_moontown_sealedcrate02", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
        List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 5, 5, 100),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticBottle_WaterItemName, 0, 1, 10),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_SalmonItemName, 0, 1, 25),
            new MinMaxLootConfig(ChoppableTreesModConstants.FeatherItemName, 0, 2, 20),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_EmptyItemName, 0, 1, 25),
            new MinMaxLootConfig(ChoppableTreesModConstants.BananaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Berries_RedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Bucket_MilkItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Canteen_EmptyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Canteen_SaltWaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.CaveMushroomItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ChiliItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_CoconutChickenItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_HeadBrothItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_HeartyStewItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_LeftoverItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_SimpleFishStewItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_RootVegetableSoupItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_CatfishDeluxeItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_MushroomOmeletteItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_SalmonLsItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_SharkDinnerItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_SteakWithJamItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_SushiItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.CoconutItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_BeetItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_CatfishItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_DrumstickItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_GenericMeatItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_HerringItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_MackerelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_PomfretItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_PotatoItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_SalmonItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_SharkItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_TilapiaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlassItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_CoconutBeatItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_LeftoverItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_MangonanaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_RedbeetShotItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_RedMelonItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_SilverSmoothieItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_SimpleSmoothieItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_SpicyPineberryItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_StrawberryColadaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.EggItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.HoneyCombItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Jar_HoneyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.JuniperItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.MangoItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PineappleItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticBottle_EmptyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticBottle_SaltWaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticBottle_WaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticCup_EmptyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticCup_SaltWaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_BeetItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_CatfishItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_DrumstickItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_GenericMeatItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_HerringItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_MackerelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_PomfretItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_PotatoItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_SalmonItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_SharkItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_TilapiaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.SilverAlgaeItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.StrawberryItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.TurmericItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.WatermelonItemName, 0, 1, 5)
        };
        ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 18, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
    });
    objectConfigurators.Add("tp_moontown_sealedcrate01", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
        List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 5, 5, 100),
            new MinMaxLootConfig(ChoppableTreesModConstants.Berries_RedItemName, 0, 2, 30),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlassItemName, 0, 1, 25),
            new MinMaxLootConfig(ChoppableTreesModConstants.BananaItemName, 0, 1, 20),
            new MinMaxLootConfig(ChoppableTreesModConstants.Canteen_WaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_BBQItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.OxygenBottleItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.HealingSalve_GoodItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.EggItemName, 0, 2, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.BananaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Berries_RedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Bucket_MilkItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Canteen_EmptyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Canteen_SaltWaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.CaveMushroomItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ChiliItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_CoconutChickenItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_EmptyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_HeadBrothItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_HeartyStewItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_LeftoverItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_SimpleFishStewItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_RootVegetableSoupItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_CatfishDeluxeItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_MushroomOmeletteItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_SalmonLsItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_SharkDinnerItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_SteakWithJamItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_SushiItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.CoconutItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_BeetItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_CatfishItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_DrumstickItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_GenericMeatItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_HerringItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_MackerelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_PomfretItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_PotatoItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_SalmonItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_SharkItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_TilapiaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlassItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_CoconutBeatItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_LeftoverItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_MangonanaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_RedbeetShotItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_RedMelonItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_SilverSmoothieItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_SimpleSmoothieItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_SpicyPineberryItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_StrawberryColadaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.EggItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.HoneyCombItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Jar_HoneyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.JuniperItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.MangoItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PineappleItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticBottle_EmptyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticBottle_SaltWaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticBottle_WaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticCup_EmptyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticCup_SaltWaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_BeetItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_CatfishItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_DrumstickItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_GenericMeatItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_HerringItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_MackerelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_PomfretItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_PotatoItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_SalmonItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_SharkItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_TilapiaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.SilverAlgaeItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.StrawberryItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.TurmericItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.WatermelonItemName, 0, 1, 5)
        };
        ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 18, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
    });

    objectConfigurators.Add("vp_metalcrate", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 5, 5, 100),
            new MinMaxLootConfig(ChoppableTreesModConstants.Canteen_WaterItemName, 0, 1, 25),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_BBQItemName, 0, 1, 20),
            new MinMaxLootConfig(ChoppableTreesModConstants.OxygenBottleItemName, 0, 1, 15),
            new MinMaxLootConfig(ChoppableTreesModConstants.HealingSalve_GoodItemName, 0, 1, 15),
            new MinMaxLootConfig(ChoppableTreesModConstants.EggItemName, 0, 2, 25),
            new MinMaxLootConfig(ChoppableTreesModConstants.BananaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Berries_RedItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Bucket_MilkItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Canteen_EmptyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Canteen_SaltWaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.CaveMushroomItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ChiliItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_CoconutChickenItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_EmptyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_HeadBrothItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_HeartyStewItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_LeftoverItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_SimpleFishStewItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Claybowl_RootVegetableSoupItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_CatfishDeluxeItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_MushroomOmeletteItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_SalmonLsItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_SharkDinnerItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_SteakWithJamItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayPlate_SushiItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.CoconutItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_BeetItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_CatfishItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_DrumstickItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_GenericMeatItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_HerringItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_MackerelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_PomfretItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_PotatoItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_SalmonItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_SharkItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Cooked_TilapiaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlassItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_CoconutBeatItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_LeftoverItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_MangonanaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_RedbeetShotItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_RedMelonItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_SilverSmoothieItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_SimpleSmoothieItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_SpicyPineberryItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.DrinkingGlass_StrawberryColadaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.EggItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.HoneyCombItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Jar_HoneyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.JuniperItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.MangoItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PineappleItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticBottle_EmptyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticBottle_SaltWaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticBottle_WaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticCup_EmptyItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticCup_SaltWaterItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_BeetItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_CatfishItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_DrumstickItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_GenericMeatItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_HerringItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_MackerelItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_PomfretItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_PotatoItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_SalmonItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_SharkItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.Raw_TilapiaItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.SilverAlgaeItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.StrawberryItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.TurmericItemName, 0, 1, 1),
            new MinMaxLootConfig(ChoppableTreesModConstants.WatermelonItemName, 0, 1, 5)
        };
        ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 10, ChoppableTreesModConstants.Particle_TreeBreak, true, ColliderType.Capsule, processedObjects);
    });

    objectConfigurators.Add("tp_moontown_barrel02", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
        List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 5, 5, 100),
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticBottle_SaltWaterItemName, 0, 2, 10),
            new MinMaxLootConfig(ChoppableTreesModConstants.BioFuelItemName, 0, 1, 30),
            new MinMaxLootConfig(ChoppableTreesModConstants.SeaVineItemName, 0, 2, 30)
        };
        ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 12, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
    });
    objectConfigurators.Add("scarecrow", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
        List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
            new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 1, 3, 50),
            new MinMaxLootConfig(ChoppableTreesModConstants.RopeItemName, 1, 3, 50)
        };
        ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 12, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
    });
    objectConfigurators.Add("tp_moontown_barrel01", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
        List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 5, 5, 100),
            new MinMaxLootConfig(ChoppableTreesModConstants.SandItemName, 0, 2, 30),
            new MinMaxLootConfig(ChoppableTreesModConstants.ClayItemName, 0, 2, 35)
        };
        ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 12, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
    });
    objectConfigurators.Add("tp_moontown_barrel03", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
        List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
            new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 5, 5, 100),
            new MinMaxLootConfig(ChoppableTreesModConstants.StoneItemName, 0, 2, 30),
            new MinMaxLootConfig(ChoppableTreesModConstants.VineGooItemName, 0, 1, 35)
        };
        ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 12, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
    });
            
            objectConfigurators.Add("fl_plank", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 1, 1, 0)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 10, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects, true);
            });

            objectConfigurators.Add("banana_bush", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 1, 1, 33),
                    new MinMaxLootConfig(ChoppableTreesModConstants.PalmLeafItemName, 2, 5, 30)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 10, ChoppableTreesModConstants.Particle_TreeBreak, true, ColliderType.Capsule, processedObjects);
            });

            objectConfigurators.Add("reefhuts_wall1", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 1, 3, 80),
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 1, 2, 80)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 12, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });
            objectConfigurators.Add("reefhuts_wall2", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 1, 3, 80),
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 2, 4, 80)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 12, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });
            objectConfigurators.Add("reefhuts_wall3", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 2, 4, 80)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 12, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });
            objectConfigurators.Add("reefhuts_wall4", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 1, 4, 100),
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 2, 5, 80)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 12, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });
            objectConfigurators.Add("reefhuts_wall5", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 1, 2, 90),
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 2, 3, 80)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 12, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });
            
            objectConfigurators.Add("fl_plywood", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 1, 2, 70)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 8, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects, true);
            });
            
            objectConfigurators.Add("deadtree", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 3, 5, 70)
                };
                ConfigureDeadTreeWithSpecificColliders(obj, treeLyr, defLyr, loot, 10, ChoppableTreesModConstants.Particle_TreeBreak, processedObjects);
            });

            objectConfigurators.Add("tp_pinetreesnowy", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 4, 6, 70),
                    new MinMaxLootConfig(ChoppableTreesModConstants.FeatherItemName, 0, 2, 10), //copy that to another trees
                    new MinMaxLootConfig(ChoppableTreesModConstants.EggItemName, 0, 2, 10)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 15, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });
            objectConfigurators.Add("parabol", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.CircuitBoardItemName, 0, 1, 50),
                    new MinMaxLootConfig(ChoppableTreesModConstants.BoltItemName, 0, 1, 50),
                    new MinMaxLootConfig(ChoppableTreesModConstants.ScrapItemName, 3, 5, 70)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 10, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });
            objectConfigurators.Add("pinetree_big", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 7, 21, 70),
                    new MinMaxLootConfig(ChoppableTreesModConstants.FeatherItemName, 0, 2, 10),
                    new MinMaxLootConfig(ChoppableTreesModConstants.EggItemName, 0, 2, 10)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 20, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });
            objectConfigurators.Add("birchtree_big", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 5, 11, 70),
                    new MinMaxLootConfig(ChoppableTreesModConstants.FeatherItemName, 0, 2, 10),
                    new MinMaxLootConfig(ChoppableTreesModConstants.EggItemName, 0, 2, 10)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 20, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });
            objectConfigurators.Add("firtree_big", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 6, 15, 70),
                    new MinMaxLootConfig(ChoppableTreesModConstants.FeatherItemName, 0, 2, 10),
                    new MinMaxLootConfig(ChoppableTreesModConstants.EggItemName, 0, 2, 10)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 20, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });

            objectConfigurators.Add("balboa_bigbush", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 1, 2, 20)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 8, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });

            objectConfigurators.Add("fl_crate", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 1, 4, 40)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 10, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects, true);
            });
			 objectConfigurators.Add("table_floor", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 1, 4, 50),
					new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 1, 3, 50)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 10, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });
            objectConfigurators.Add("fl_pillar", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 1, 1, 10)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 5, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects, true);
            });
            
            objectConfigurators.Add("fl_roof", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 1, 3, 80)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 10, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects, true);
            });
            objectConfigurators.Add("reefhuts_pillar", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 1, 1, 10)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 8, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });
            
            objectConfigurators.Add("bed_simple", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PalmLeafItemName, 1, 3, 80),
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 1, 3, 70)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 8, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });

            objectConfigurators.Add("fl_foundation", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 1, 3, 60),
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 1, 3, 60)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 15, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects, true);
            });

            objectConfigurators.Add("tp_moontown_barrel04", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 2, 3, 90),
                    new MinMaxLootConfig(ChoppableTreesModConstants.SandItemName, 0, 15, 30),
                    new MinMaxLootConfig(ChoppableTreesModConstants.BioFuelItemName, 0, 1, 30),
                    new MinMaxLootConfig(ChoppableTreesModConstants.ClayItemName, 0, 15, 35),
                    new MinMaxLootConfig(ChoppableTreesModConstants.SeaVineItemName, 0, 8, 30),
                    new MinMaxLootConfig(ChoppableTreesModConstants.VineGooItemName, 0, 8, 35)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 12, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });
            
            objectConfigurators.Add("crate_small", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 2, 3, 80), 
                    new MinMaxLootConfig(ChoppableTreesModConstants.SandItemName, 0, 15, 30),
                    new MinMaxLootConfig(ChoppableTreesModConstants.ClayItemName, 0, 15, 35),
                    new MinMaxLootConfig(ChoppableTreesModConstants.SeaVineItemName, 0, 8, 30),
                    new MinMaxLootConfig(ChoppableTreesModConstants.VineGooItemName, 0, 8, 35)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 8, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });

            objectConfigurators.Add("crate_big", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 2, 3, 80),
                    new MinMaxLootConfig(ChoppableTreesModConstants.SandItemName, 0, 15, 30),
                    new MinMaxLootConfig(ChoppableTreesModConstants.ClayItemName, 0, 15, 35),
                    new MinMaxLootConfig(ChoppableTreesModConstants.SeaVineItemName, 0, 8, 30),
                    new MinMaxLootConfig(ChoppableTreesModConstants.VineGooItemName, 0, 8, 35)
                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 12, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });
			objectConfigurators.Add("reefhuts_stick", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlankItemName, 1, 1, 10), 

                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 12, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });
			objectConfigurators.Add("reefhuts_plasticwall", (obj, treeLyr, defLyr, nameLower, processedObjects) => {
                List<MinMaxLootConfig> loot = new List<MinMaxLootConfig> {
                    new MinMaxLootConfig(ChoppableTreesModConstants.PlasticItemName, 1, 3, 10), 

                };
                ConfigureTemplateObject(obj, treeLyr, defLyr, loot, 12, ChoppableTreesModConstants.Particle_TreeBreak, false, null, processedObjects);
            });

            // Original FL_Cloth
            objectConfigurators.Add("fl_cloth", (obj, treeLyrUnused, defLyrUnused, nameLower, processedObjects) => {
                ConfigureFLCloth(obj, nameLower);
            });

            // New cloth-like pickup configurations
            objectConfigurators.Add("campfire", (obj, treeLyrUnused, defLyrUnused, nameLower, processedObjects) => {
                ConfigurePickupLootObject(obj, "Campfire", ChoppableTreesModConstants.Placeable_CookingStand_Food_OneItemName, new Interval_Int { minValue = 1, maxValue = 2 }, "0.8");
            });
            objectConfigurators.Add("buoy", (obj, treeLyrUnused, defLyrUnused, nameLower, processedObjects) => {
                ConfigurePickupLootObject(obj, "Buoy", ChoppableTreesModConstants.PlasticItemName, new Interval_Int { minValue = 1, maxValue = 1 }, "0.1");
            });
            objectConfigurators.Add("bird_nest", (obj, treeLyrUnused, defLyrUnused, nameLower, processedObjects) => {
                ConfigurePickupLootObject(obj, "Bird_Nest", ChoppableTreesModConstants.Placeable_BirdsNestItemName, new Interval_Int { minValue = 1, maxValue = 1 }, "0.0");
            });
            objectConfigurators.Add("cropplot_floor", (obj, treeLyrUnused, defLyrUnused, nameLower, processedObjects) => {
                ConfigurePickupLootObject(obj, "Cropplot_Floor", ChoppableTreesModConstants.Placeable_Cropplot_SmallItemName, new Interval_Int { minValue = 1, maxValue = 1 }, "0.0");
            });
        }
    }
}
