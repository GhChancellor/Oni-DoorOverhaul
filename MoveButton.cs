namespace Door_Overhaul
{
    internal class MoveButton : KMonoBehaviour
    {
#pragma warning disable CS0649
        [MyCmpGet]
        private Deconstructable deconstructable;
        [MyCmpGet]
        private Constructable constructable;

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

        private void DeconstructableX01()
        {
            var buildingDef = Assets.GetBuildingDef(BuildingID);

            /* --------------------Cambia tempo di distruzione----------------------- */
            Console.WriteLine("Lele - deconstructable() Before :" + BuildingID);
            Console.WriteLine("Lele - deconstructable() Before :" + buildingDef.ConstructionTime);

            buildingDef.ConstructionTime = 15f;

            Console.WriteLine("Lele - deconstructable() after :" + buildingDef.ConstructionTime);

            deconstructable.SetWorkTime(buildingDef.ConstructionTime);

            Console.WriteLine("Lele - deconstructable() after - GetWorkTime():" + deconstructable.GetWorkTime());
            /* ---------------------------------------------------- */
        }

        private void ConstructableX01()
        {
            /* --------------------Cambia tempo di distruzione----------------------- */
            var buildingDef = Assets.GetBuildingDef(BuildingID);

            Console.WriteLine("Lele - constructable() Before :" + BuildingID);
            Console.WriteLine("Lele - constructable() Before :" + buildingDef.ConstructionTime);

            buildingDef.ConstructionTime = 15f;

            Console.WriteLine("Lele - constructable() after :" + buildingDef.ConstructionTime);

            constructable.SetWorkTime(buildingDef.ConstructionTime);

            Console.WriteLine("Lele - constructable() after - GetWorkTime():" + constructable.GetWorkTime());
            /* ---------------------------------------------------- */
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
        }

        private void Destroy(Deconstructable deconstructable)
        {
            if (destroyActions.TryGetValue(BuildingID, out var destroyAction))
            {
                destroyAction(deconstructable);
            }
        }

        private void DuplicateDoor2()
        {
            Debug.Log("Lele - Start - Duplicate ");
            try
            {
                Debug.Log($"Attempting to add building {BuildingID} to plan screen");

                var buildingDef = Assets.GetBuildingDef(BuildingID);
                if (buildingDef == null)
                {
                    Debug.LogError($"BuildingDef not found for {BuildingID}");
                    return;
                }
                Debug.Log($"BuildingDef found for {BuildingID}");

                var planScreen = PlanScreen.Instance;
                if (planScreen == null)
                {
                    Debug.LogError("PlanScreen instance is null");
                    return;
                }
                Debug.Log("PlanScreen instance found");

                if (buildingDef != null && planScreen != null)
                {
                    Debug.Log($"Attempting to copy building order for {BuildingID}");
                    planScreen.CopyBuildingOrder(buildingDef, BuildingID);
                    Debug.Log($"Successfully copied building order for {BuildingID}");
                }
                else
                {
                    Debug.LogWarning($"Either buildingDef or planScreen is null for {BuildingID}");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error adding building {BuildingID} to plan screen: {e.Message}");
                Debug.LogException(e);
            }

            Debug.Log("Lele - Stop - Duplicate ");
            BuildingID = null;
        }
    }
}
