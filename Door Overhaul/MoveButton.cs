using STRINGS;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Door_Overhaul
{
    internal class MoveButton : KMonoBehaviour
    {
#pragma warning disable CS0649
        [MyCmpGet]
        private Deconstructable deconstructable;
#pragma warning restore CS0649

        private PneumaticTrapDoorManager pneumaticTrapDoorManager;

        protected override void OnPrefabInit()
        {
            pneumaticTrapDoorManager = new PneumaticTrapDoorManager();
            base.OnPrefabInit();
            this.Subscribe((int)GameHashes.RefreshUserMenu, 
                new System.Action<object>(OnRefreshUserMenu));
        }

        private void OnRefreshUserMenu(object data)
        {
            Game.Instance.userMenu.AddButton(
                go: this.gameObject,
                button: new KIconButtonMenu.ButtonInfo(
                    iconName: "action_mirror",
                    text: STRINGS.BUILDINGS.PREFABS.MOVEDOOR.NAME,
                    on_click: new System.Action( () => DuplicateDoor(PneumaticTrapDoor.ID)),
                    shortcutKey: Action.BuildingUtility1,
                    tooltipText: STRINGS.BUILDINGS.PREFABS.MOVEDOOR.TOOLTIP
                )
            );
        }

        private void DuplicateDoor(String doorID)
        {
            PneumaticTrapDoor pneumaticTrapDoor = new PneumaticTrapDoor();
            PneumaticTrapDoorManager pneumaticTrapDoorManager =
                new PneumaticTrapDoorManager();

            pneumaticTrapDoorManager.Destroy(deconstructable);
            pneumaticTrapDoor.SetReplacement(true);

            // change recipe, change costructionTime
            BuildingDef buildingDef = pneumaticTrapDoor.CreateBuildingDef();
            Debug.Log("DuplicateDoor - BuildingDef create: " + buildingDef);
            Debug.Log("DuplicateDoor - BuildingDef create: PrefabID: " + buildingDef.PrefabID);
            Debug.Log("DuplicateDoor - BuildingDef construzionTime: " + buildingDef.ConstructionTime);

            //BuildingDef _buildingDef = Assets.GetBuildingDef(PneumaticTrapDoor.ID);
            //Debug.Log("DuplicateDoor - Assets.GetBuildingDef PrefabID" + _buildingDef.PrefabID);
            //Debug.Log("DuplicateDoor - Assets.GetBuildingDef " + _buildingDef.ConstructionTime);

            PlanScreen planScreen = PlanScreen.Instance;
            planScreen.CopyBuildingOrder(buildingDef, PneumaticTrapDoor.ID);


        }


        //private void CopyPneumaticTrapDoor()
        //{
        //    var buildingDef = Assets.GetBuildingDef(PneumaticTrapDoor.ID);
        //    PlanScreen planScreen = PlanScreen.Instance;
        //    planScreen.CopyBuildingOrder(buildingDef, PneumaticTrapDoor.ID);

        //}


        //public void FindAndCopyBuilding()
        //{
        //    // Assumiamo che tu voglia trovare un edificio specifico nel mondo di gioco
        //    Building building = FindBuildingByID(PneumaticTrapDoor.ID);
        //    if (building != null)
        //    {
        //        PlanScreen planScreen = PlanScreen.Instance;
        //        planScreen.CopyBuildingOrder(building);
        //    }
        //    else
        //    {
        //        Debug.LogError("Building not found");
        //    }
        //}

        //private Building FindBuildingByID(string buildingID)
        //{
        //    // Trova tutti gli oggetti di tipo Building nel mondo di gioco
        //    Building[] allBuildings = GameObject.FindObjectsOfType<Building>();
        //    foreach (Building b in allBuildings)
        //    {
        //        if (b.Def.PrefabID == buildingID)
        //        {
        //            return b;
        //        }
        //    }
        //    return null;
        //}
    }
}
