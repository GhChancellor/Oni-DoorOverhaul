namespace Door_Overhaul
{
    internal class MoveButton : KMonoBehaviour
    {
#pragma warning disable CS0649
        [MyCmpGet]
        private Deconstructable deconstructable;
#pragma warning restore CS0649

        public static string BuildingID { get; set; }

        private static readonly Dictionary<string, Action<Deconstructable>> destroyActions =
            new Dictionary<string, Action<Deconstructable>>
        {
            {PneumaticTrapDoor.GetBuildingID(), (deconstructable) => new PneumaticTrapDoorManager().Destroy(deconstructable)},
            {PneumaticTrapDoorReplace.GetBuildingID(), (deconstructable) => new PneumaticTrapDoorManager().Destroy(deconstructable)},

            // {NoPawNoProblemDoor.GetBuildingID(), (deconstructable) => new NoPawNoProblemDoorManager().Destroy(deconstructable)},
            // {NoPawNoProblemDoorReplace.GetBuildingID(), (deconstructable) => new NoPawNoProblemDoorManager().Destroy(deconstructable)}

        };

        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();
            this.Subscribe((int)GameHashes.RefreshUserMenu,
                new System.Action<object>(OnRefreshUserMenu));
        }

        protected override void OnCleanUp()
        {
            Unsubscribe((int)GameHashes.RefreshUserMenu);
            base.OnCleanUp();
        }

        private void OnRefreshUserMenu(object data)
        {
            Game.Instance.userMenu.AddButton(
                go: this.gameObject,
                button: new KIconButtonMenu.ButtonInfo(
                    iconName: "action_mirror",
                    text: STRINGS.BUILDINGS.PREFABS.MOVEDOOR.NAME,
                    on_click: new System.Action(() => DuplicateDoor()),
                    shortcutKey: Action.BuildingUtility1,
                    tooltipText: STRINGS.BUILDINGS.PREFABS.MOVEDOOR.TOOLTIP
                )
            );
        }

        private void DuplicateDoor2(String _doorID)
        {
            string doorID = PneumaticTrapDoorReplace.GetBuildingID();

            Debug.Log("DuplicateDoor2");
            PlanScreen planScreen = PlanScreen.Instance;

            var buildingDef = Assets.GetBuildingDef(doorID);

            planScreen.OnSelectBuilding(this.gameObject, buildingDef, doorID);
            planScreen.CopyBuildingOrder(buildingDef, doorID);

        }

        private void DuplicateDoor()
        {
            Destroy(deconstructable);
            PlanScreen planScreen = PlanScreen.Instance;

            var buildingDef = Assets.GetBuildingDef(BuildingID);

            if (buildingDef != null && planScreen != null)
            {
                planScreen.CopyBuildingOrder(buildingDef, BuildingID);
            }

            BuildingID = null;
        }

        private void Destroy(Deconstructable deconstructable)
        {
            if (destroyActions.TryGetValue(BuildingID, out var destroyAction))
            {
                destroyAction(deconstructable);
            }
        }
    }
}
