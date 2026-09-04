namespace Assets.Scripts
{
    using System.Collections.Generic;
    using ModApi.Common;
    using ModApi.Mods;
    using UnityEngine;
    using HarmonyLib;

    /// <summary>
    /// A singleton object representing this mod that is instantiated and initialized when the mod is loaded.
    /// </summary>
    public class Mod : ModApi.Mods.GameMod
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="Mod"/> class from being created.
        /// </summary>
        private Mod() : base()
        {
        }

        /// <summary>
        /// Gets the singleton instance of the mod object.
        /// </summary>
        /// <value>The singleton instance of the mod object.</value>
        public static Mod Instance { get; } = GetModInstance<Mod>();

        /// <summary>
        /// Called when the mod is loaded. Registers all Harmony patches.
        /// </summary>
        public override void OnModLoaded()
        {
            base.OnModLoaded();

            // Registers every [HarmonyPatch] in this assembly
            // (LoadItems / UpdatePreview / InstantiateSubStructures patches).
            Harmony harmony = new Harmony(Instance.ModInfo.Name);
            harmony.PatchAll();
        }

        /// <summary>
        /// The structures to add to the "Add Structure" list in Planet Studio.
        /// Key = name shown in the list, Value = path of the prefab inside this mod.
        /// </summary>
        public static Dictionary<string, string> StructurePrefabs = new Dictionary<string, string>()
        {
            // 火箭工厂
            { "RocketFactoryInside", "Assets/Content/Models/RocketFactoryInside.prefab" },
            { "RocketFactoryOut", "Assets/Content/Models/RocketFactOut.prefab" },
            { "deadPlane", "Assets/Content/Models/deadPlane.prefab" },

            // 老房子
            { "oldHouse1", "Assets/Content/Models/oldHouse1.prefab" },
            { "oldHouse2", "Assets/Content/Models/oldHouse2.prefab" },
            { "oldHouse3", "Assets/Content/Models/oldHouse3.prefab" },

            // 树
            { "treeTypeA", "Assets/Content/Models/treeTypeA.prefab" },
            { "treeTypeB", "Assets/Content/Models/treeTypeB.prefab" },
            { "OakBigTree01", "Assets/Content/Models/OakBigTree01.prefab" },
            { "OakBigTree02", "Assets/Content/Models/Tree9_2.prefab" },
            { "OakBigTree03", "Assets/Content/Models/Tree9_3.prefab" },
            { "OakBigTree04", "Assets/Content/Models/Tree9_4.prefab" },
            { "OakBigTree05", "Assets/Content/Models/Tree9_5.prefab" },

            // 直升机 / 车辆
            { "heliBody", "Assets/Content/Models/heliBody.prefab" },
            { "heliBladeMain", "Assets/Content/Models/heliBladeMain.prefab" },
            { "heliBladeVice", "Assets/Content/Models/heliBladeVice.prefab" },
            { "Bus", "Assets/Content/Models/Bus.prefab" },
            { "carCivi", "Assets/Content/Models/carCivi.prefab" },
            { "carMili", "Assets/Content/Models/carMili.prefab" },

            // 栅栏
            { "BrickDoorway", "Assets/Content/Models/BrickDoorway.prefab" },
            { "BrickWall", "Assets/Content/Models/BrickWall.prefab" },
            { "BrickWall1", "Assets/Content/Models/BrickWall1.prefab" },
            { "BrickWall2", "Assets/Content/Models/BrickWall2.prefab" },
            { "BrickWall3", "Assets/Content/Models/BrickWall3.prefab" },
            { "BrickWallPillar", "Assets/Content/Models/BrickWallPillar.prefab" },
            { "BrickWallPillar2", "Assets/Content/Models/BrickWallPillar2.prefab" },
            { "ConcreteFance", "Assets/Content/Models/ConcreteFance.prefab" },
            { "ConcreteFance1", "Assets/Content/Models/ConcreteFance1.prefab" },
            { "ConcretePillar", "Assets/Content/Models/ConcretePillar.prefab" },
            { "ConcretePlate", "Assets/Content/Models/ConcretePlate.prefab" },
            { "Gates", "Assets/Content/Models/Gates.prefab" },
            { "LowWoodFence", "Assets/Content/Models/LowWoodFence.prefab" },
            { "LowWoodFencePillar", "Assets/Content/Models/LowWoodFencePillar.prefab" },
            { "LowWoodFenceSharp", "Assets/Content/Models/LowWoodFenceSharp.prefab" },
            { "MeshFence", "Assets/Content/Models/MeshFence.prefab" },
            { "MeshFence1", "Assets/Content/Models/MeshFence1.prefab" },
            { "MeshFence2", "Assets/Content/Models/MeshFence2.prefab" },
            { "MeshFenceHole", "Assets/Content/Models/MeshFenceHole.prefab" },
            { "MeshFencePillar", "Assets/Content/Models/MeshFencePillar.prefab" },
            { "MetalSheetRack", "Assets/Content/Models/MetalSheetRack.prefab" },
            { "MetalSheetRackBlue", "Assets/Content/Models/MetalSheetRackBlue.prefab" },
            { "MetalSheetRackGreen", "Assets/Content/Models/MetalSheetRackGreen.prefab" },
            { "MetalSheetRackOld", "Assets/Content/Models/MetalSheetRackOld.prefab" },
            { "MetalSheetRackRed", "Assets/Content/Models/MetalSheetRackRed.prefab" },
            { "OldWoodDoorWay", "Assets/Content/Models/OldWoodDoorWay.prefab" },
            { "OldWoodFence", "Assets/Content/Models/OldWoodFence.prefab" },
            { "OldWoodPillar", "Assets/Content/Models/OldWoodPillar.prefab" },
            { "PO2", "Assets/Content/Models/PO2.prefab" },
            { "PO2Stand", "Assets/Content/Models/PO2Stand.prefab" },
            { "PO2StandV1", "Assets/Content/Models/PO2StandV1.prefab" },
            { "PO2StandV2", "Assets/Content/Models/PO2StandV2.prefab" },
            { "RoadBarrier", "Assets/Content/Models/RoadBarrier.prefab" },
            { "RoadBarrierMetalSheet", "Assets/Content/Models/RoadBarrierMetalSheet.prefab" },
            { "WoodSharpFence", "Assets/Content/Models/WoodSharpFence.prefab" },
            { "WoodFence", "Assets/Content/Models/WoodFence.prefab" },

            // 电线杆子
            { "electric_tower_00", "Assets/Content/Models/electric_tower_00.prefab" },
            { "electric_tower_01", "Assets/Content/Models/electric_tower_01.prefab" },
            { "electric_tower_02", "Assets/Content/Models/electric_tower_02.prefab" },
            { "electric_tower_03", "Assets/Content/Models/electric_tower_03.prefab" },
            { "electric_tower_04", "Assets/Content/Models/electric_tower_04.prefab" },
            { "electric_tower_05", "Assets/Content/Models/electric_tower_05.prefab" },
            { "electric_tower_06", "Assets/Content/Models/electric_tower_06.prefab" },
            { "electric_tower_07", "Assets/Content/Models/electric_tower_07.prefab" },
            { "electric_tower_08", "Assets/Content/Models/electric_tower_08.prefab" },
            { "electric_tower_09", "Assets/Content/Models/electric_tower_09.prefab" },
            { "electric_tower_10", "Assets/Content/Models/electric_tower_10.prefab" },
            { "electric_tower_11", "Assets/Content/Models/electric_tower_11.prefab" },
            { "electric_tower_12", "Assets/Content/Models/electric_tower_12.prefab" },
            { "electric_tower_13", "Assets/Content/Models/electric_tower_13.prefab" },
            { "electric_tower_14", "Assets/Content/Models/electric_tower_14.prefab" },
            { "electric_tower_15", "Assets/Content/Models/electric_tower_15.prefab" },
            { "electric_tower_16", "Assets/Content/Models/electric_tower_16.prefab" },
            { "electric_tower_17", "Assets/Content/Models/electric_tower_17.prefab" },
            { "electric_tower_18", "Assets/Content/Models/electric_tower_18.prefab" },
            { "electric_tower_19", "Assets/Content/Models/electric_tower_19.prefab" },
            { "Tower1", "Assets/Content/Models/Tower1.prefab" },

            // 建筑
            { "rus_build_2et_01", "Assets/Content/Models/rus_build_2et_01.prefab" },
            { "rus_build_2et_01a", "Assets/Content/Models/rus_build_2et_01a.prefab" },
            { "rus_build_2et_01b", "Assets/Content/Models/rus_build_2et_01b.prefab" },
            { "rus_build_2et_01c", "Assets/Content/Models/rus_build_2et_01c.prefab" },
            { "rus_build_4et_01", "Assets/Content/Models/rus_build_4et_01.prefab" },
            { "rus_build_4et_01a", "Assets/Content/Models/rus_build_4et_01a.prefab" },
            { "rus_build_5et_01", "Assets/Content/Models/rus_build_5et_01.prefab" },
            { "rus_build_5et_01a", "Assets/Content/Models/rus_build_5et_01a.prefab" },
            { "rus_build_5et_02", "Assets/Content/Models/rus_build_5et_02.prefab" },
            { "rus_build_5et_02a", "Assets/Content/Models/rus_build_5et_02a.prefab" },
            { "rus_build_5et_03", "Assets/Content/Models/rus_build_5et_03.prefab" },
            { "rus_build_5et_03a", "Assets/Content/Models/rus_build_5et_03a.prefab" },
            { "rus_build_5et_03b", "Assets/Content/Models/rus_build_5et_03b.prefab" },
            { "rus_build_5et_03c", "Assets/Content/Models/rus_build_5et_03c.prefab" },
            { "rus_build_5et_03d", "Assets/Content/Models/rus_build_5et_03d.prefab" },
            { "rus_build_5et_03e", "Assets/Content/Models/rus_build_5et_03e.prefab" },
            { "rus_build_5et_03f", "Assets/Content/Models/rus_build_5et_03f.prefab" },
            { "rus_build_5et_04", "Assets/Content/Models/rus_build_5et_04.prefab" },
            { "rus_build_5et_05", "Assets/Content/Models/rus_build_5et_05.prefab" },
            { "rus_build_5et_06", "Assets/Content/Models/rus_build_5et_06.prefab" },
            { "rus_build_9et_01", "Assets/Content/Models/rus_build_9et_01.prefab" },
            { "rus_build_9et_01a", "Assets/Content/Models/rus_build_9et_01a.prefab" },
            { "rus_build_9et_01b", "Assets/Content/Models/rus_build_9et_01b.prefab" },
            { "rus_build_9et_02", "Assets/Content/Models/rus_build_9et_02.prefab" },
            { "rus_build_9et_02a", "Assets/Content/Models/rus_build_9et_02a.prefab" },
            { "rus_build_9et_02b", "Assets/Content/Models/rus_build_9et_02b.prefab" },
            { "rus_build_9et_03", "Assets/Content/Models/rus_build_9et_03.prefab" },
            { "rus_build_9et_03a", "Assets/Content/Models/rus_build_9et_03a.prefab" },
            { "rus_build_9et_03b", "Assets/Content/Models/rus_build_9et_03b.prefab" },
            { "rusBulding1", "Assets/Content/Models/rusBulding1.prefab" },
            { "rusBulding2", "Assets/Content/Models/rusBulding2.prefab" },
            { "rusBulding3", "Assets/Content/Models/rusBulding3.prefab" },
            { "rusBulding4", "Assets/Content/Models/rusBulding4.prefab" },

            // 路面
            { "Sand", "Assets/Content/Models/Sand.prefab" },
            { "Rock", "Assets/Content/Models/Rock.prefab" },
            { "Grass", "Assets/Content/Models/Grass.prefab" },
            // 4k 路面
            { "Asphalt1", "Assets/Content/Models/Asphalt1.prefab" },
            { "Asphalt2", "Assets/Content/Models/Asphalt2.prefab" },
            { "Asphalt3", "Assets/Content/Models/Asphalt3.prefab" },
            { "Old Paving Stones", "Assets/Content/Models/Old Paving Stones.prefab" },
            { "Paving Stones 1", "Assets/Content/Models/Paving Stones 1.prefab" },
            { "Paving Stones 1 Wet", "Assets/Content/Models/Paving Stones 1 Wet.prefab" },
            { "Paving Stones 2", "Assets/Content/Models/Paving Stones 2.prefab" },
            { "Paving Stones 2 Wet", "Assets/Content/Models/Paving Stones 2 Wet.prefab" },

            // 路灯
            { "单臂路灯", "Assets/Content/Models/单臂路灯.prefab" },
            { "双臂路灯", "Assets/Content/Models/双臂路灯.prefab" },
            { "StreetLighting", "Assets/Content/Models/StreetLighting.prefab" },
            { "StreetLighting2", "Assets/Content/Models/StreetLighting2.prefab" },

            // 数字
            { "1", "Assets/Content/Models/1.prefab" },
            { "2", "Assets/Content/Models/2.prefab" },
            { "3", "Assets/Content/Models/3.prefab" },
            { "4", "Assets/Content/Models/4.prefab" },
            { "5", "Assets/Content/Models/5.prefab" },
            { "6", "Assets/Content/Models/6.prefab" },
            { "7", "Assets/Content/Models/7.prefab" },
            { "8", "Assets/Content/Models/8.prefab" },
            { "9", "Assets/Content/Models/9.prefab" },
            { "0", "Assets/Content/Models/0.prefab" },

            // 发射场
            { "卫星天线", "Assets/Content/Models/卫星天线.prefab" },
            { "LC1", "Assets/Content/Models/LC1.prefab" },
            { "LC2", "Assets/Content/Models/LC2.prefab" },
        };
    }
}
