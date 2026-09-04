

using System.Windows.Forms;

namespace Assets.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ModApi;
    using ModApi.Common;
    using ModApi.Mods;
    using UnityEngine;
    using Jundroo.ModTools;

    /// <summary>
    /// A singleton object representing this mod that is instantiated and initialize when the mod is loaded.
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

        private void AddStuff(string name)
        {
            StructureListChange.AddStructure(name, "Assets/Content/Models/"+name+".ModPrefab");
        }
        public override void OnModLoaded()
    {
      base.OnModLoaded();
      StructureListChange.AddStructure("RocketFactoryInside", "Assets/Content/Models/RocketFactoryInside.ModPrefab");
      StructureListChange.AddStructure("RocketFactoryOut", "Assets/Content/Models/RocketFactOut.ModPrefab");
      StructureListChange.AddStructure("deadPlane", "Assets/Content/Models/deadPlane.ModPrefab");
      StructureListChange.AddStructure("oldHouse1", "Assets/Content/Models/oldHouse1.ModPrefab");
      StructureListChange.AddStructure("oldHouse2", "Assets/Content/Models/oldHouse2.ModPrefab");
      StructureListChange.AddStructure("oldHouse3", "Assets/Content/Models/oldHouse3.ModPrefab");
      StructureListChange.AddStructure("treeTypeA", "Assets/Content/Models/treeTypeA.ModPrefab");
      AddStuff("treeTypeB");
      StructureListChange.AddStructure("heliBody", "Assets/Content/Models/heliBody.ModPrefab");
      StructureListChange.AddStructure("heliBladeMain", "Assets/Content/Models/heliBladeMain.ModPrefab");
      StructureListChange.AddStructure("heliBladeVice", "Assets/Content/Models/heliBladeVice.ModPrefab");
      StructureListChange.AddStructure("Bus", "Assets/Content/Models/Bus.ModPrefab");
      StructureListChange.AddStructure("carCivi", "Assets/Content/Models/carCivi.ModPrefab");
      StructureListChange.AddStructure("carMili", "Assets/Content/Models/carMili.ModPrefab");
      //树
      StructureListChange.AddStructure("OakBigTree01", "Assets/Content/Models/OakBigTree01.ModPrefab");
      StructureListChange.AddStructure("OakBigTree02", "Assets/Content/Models/Tree9_2.ModPrefab");
      StructureListChange.AddStructure("OakBigTree03", "Assets/Content/Models/Tree9_3.ModPrefab");
      StructureListChange.AddStructure("OakBigTree04", "Assets/Content/Models/Tree9_4.ModPrefab");
      StructureListChange.AddStructure("OakBigTree05", "Assets/Content/Models/Tree9_5.ModPrefab");
      
      //栅栏
      AddStuff("BrickDoorway");
      AddStuff("BrickWall");
      AddStuff("BrickWall1");
      AddStuff("BrickWall2");
      AddStuff("BrickWall3");
      AddStuff("BrickWallPillar");
      AddStuff("BrickWallPillar2");
      AddStuff("ConcreteFance");
      AddStuff("ConcreteFance1");
      AddStuff("ConcretePillar");
      AddStuff("ConcretePlate");
      AddStuff("Gates");
      AddStuff("LowWoodFence");
      AddStuff("LowWoodFencePillar");
      AddStuff("LowWoodFenceSharp");
      AddStuff("MeshFence");
      AddStuff("MeshFence1");
      AddStuff("MeshFence2");
      AddStuff("MeshFenceHole");
      AddStuff("MeshFencePillar");
      AddStuff("MetalSheetRack");
      AddStuff("MetalSheetRackBlue");
      AddStuff("MetalSheetRackGreen");
      AddStuff("MetalSheetRackOld");
      AddStuff("MetalSheetRackRed");
      AddStuff("OldWoodDoorWay");
      AddStuff("OldWoodFence");
      AddStuff("OldWoodPillar");
      AddStuff("PO2");
      AddStuff("PO2Stand");
      AddStuff("PO2StandV1");
      AddStuff("PO2StandV2");
      AddStuff("RoadBarrier");
      AddStuff("RoadBarrierMetalSheet");
      AddStuff("WoodSharpFence");
      AddStuff("WoodFence");
      //电线杆子
      AddStuff("electric_tower_00");
      AddStuff("electric_tower_01");
      AddStuff("electric_tower_02");
      AddStuff("electric_tower_03");
      AddStuff("electric_tower_04");
      AddStuff("electric_tower_05");
      AddStuff("electric_tower_06");
      AddStuff("electric_tower_07");
      AddStuff("electric_tower_08");
      AddStuff("electric_tower_09");
      AddStuff("electric_tower_10");
      AddStuff("electric_tower_11");
      AddStuff("electric_tower_12");
      AddStuff("electric_tower_13");
      AddStuff("electric_tower_14");
      AddStuff("electric_tower_15");
      AddStuff("electric_tower_16");
      AddStuff("electric_tower_17");
      AddStuff("electric_tower_18");
      AddStuff("electric_tower_19");
      AddStuff("Tower1");
    
      
      //建筑
      AddStuff("rus_build_2et_01");
      AddStuff("rus_build_2et_01a");
      AddStuff("rus_build_2et_01b");
      AddStuff("rus_build_2et_01c");
      AddStuff("rus_build_4et_01");
      AddStuff("rus_build_4et_01a");
      AddStuff("rus_build_5et_01");
      AddStuff("rus_build_5et_01a");
      AddStuff("rus_build_5et_02");
      AddStuff("rus_build_5et_02a");
      AddStuff("rus_build_5et_03");
      AddStuff("rus_build_5et_03a");
      AddStuff("rus_build_5et_03b");
      AddStuff("rus_build_5et_03c");
      AddStuff("rus_build_5et_03d");
      AddStuff("rus_build_5et_03e");
      AddStuff("rus_build_5et_03f");
      AddStuff("rus_build_5et_04");
      AddStuff("rus_build_5et_05");
      AddStuff("rus_build_5et_06");
      AddStuff("rus_build_9et_01");
      AddStuff("rus_build_9et_01a");
      AddStuff("rus_build_9et_01b");
      AddStuff("rus_build_9et_02");
      AddStuff("rus_build_9et_02a");
      AddStuff("rus_build_9et_02b");
      AddStuff("rus_build_9et_03");
      AddStuff("rus_build_9et_03a");
      AddStuff("rus_build_9et_03b");
      AddStuff("rusBulding1");
      AddStuff("rusBulding2");
      AddStuff("rusBulding3");
      AddStuff("rusBulding4");
      
      //路面
      AddStuff("Sand");
      AddStuff("Rock");
      AddStuff("Grass");
      //4k路面
      AddStuff("Asphalt1");
      AddStuff("Asphalt2");
      AddStuff("Asphalt3");
      AddStuff("Old Paving Stones");
      AddStuff("Paving Stones 1");
      AddStuff("Paving Stones 1 Wet");
      AddStuff("Paving Stones 2");
      AddStuff("Paving Stones 2 Wet");

     //路灯
      AddStuff("单臂路灯");
      AddStuff("双臂路灯");
      AddStuff("StreetLighting");
      AddStuff("StreetLighting2");
      //数字
      AddStuff("1");
      AddStuff("2");
      AddStuff("3");
      AddStuff("4");
      AddStuff("5");
      AddStuff("6");
      AddStuff("7");
      AddStuff("8");
      AddStuff("9");
      AddStuff("0");
      //发射场
      AddStuff("卫星天线");
      AddStuff("LC1");
      AddStuff("LC2");
    
     
      StructureListChange.StartAdd();
    }

        
    }
    
    
}