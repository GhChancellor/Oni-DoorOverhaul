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
                    // on_click: new System.Action(() => DuplicateDoor(PneumaticTrapDoorReplace.ID)),
                    on_click: new System.Action(() => DuplicateDoor(PneumaticTrapDoorReplace.GetID())),
                    shortcutKey: Action.BuildingUtility1,
                    tooltipText: STRINGS.BUILDINGS.PREFABS.MOVEDOOR.TOOLTIP
                )
            );
        }

        private void DuplicateDoor(String doorID)
        {
            pneumaticTrapDoorManager.Destroy(deconstructable);
            Destroy(deconstructable);
            PlanScreen planScreen = PlanScreen.Instance;

            var buildingDef = Assets.GetBuildingDef(doorID);
            if (buildingDef != null && planScreen != null)
            {
                //planScreen.CopyBuildingOrder(buildingDef, PneumaticTrapDoorReplace.ID);
                planScreen.CopyBuildingOrder(buildingDef, PneumaticTrapDoorReplace.GetID());
            }
        }

        private void Destroy(Deconstructable deconstructable)
        {
            pneumaticTrapDoorManager.Destroy(deconstructable);
        }
    }
}
