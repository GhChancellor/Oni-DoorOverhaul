using UnityEngine;

namespace Door_Overhaul
{
    internal class MoveButton : KMonoBehaviour
    {
        private readonly ManagementError err =
            new ("# DoorOverhaul > ", "MoveButton.cs > ");

        public string BuildingID { get; set; }
        DiscoveryEventManager discoveryEvent = new DiscoveryEventManager();

        public MoveButton() { }

        /// <summary>
        /// Called when the building is spawned. Retrieves the Building component and saves its prefab ID.
        /// </summary>
        protected override void OnSpawn()
        {
            try
            {
                base.OnSpawn();

                var building = GetComponent<Building>();

                if (building == null) return;

                BuildingID = building.Def.PrefabID;

            }
            catch (Exception e)
            {
                Debug.LogError(err.GetMessageAndCode() + $"1 OnSpawn(): Exception - {e.Message} Stack: {e.StackTrace}");
            }
        }

        /// <summary>
        /// Called when the building is initialized after all prefabs are loaded. Subscribes to the RefreshUserMenu event.
        /// </summary>
        protected override void OnPrefabInit()
        {
            try
            {
                base.OnPrefabInit();
                Subscribe((int)GameHashes.RefreshUserMenu, new Action<object>(OnRefreshUserMenu));
            }
            catch (Exception e)
            {
                Debug.LogError(err.GetMessageAndCode() + $"2 OnPrefabInit(): Exception - {e.Message} Stack: {e.StackTrace}");
            }
        }

        /// <summary>
        /// Called when the building is cleaned up. Unsubscribes from the RefreshUserMenu event.
        /// </summary>
        protected override void OnCleanUp()
        {
            try
            {
                base.OnCleanUp();
                Unsubscribe((int)GameHashes.RefreshUserMenu);
            }
            catch (Exception e)
            {
                Debug.LogError(err.GetMessageAndCode() + $"3 OnCleanUp(): Exception - {e.Message} Stack: {e.StackTrace}");
            }
        }

        /// <summary>
        /// Called when the RefreshUserMenu event is triggered.
        /// </summary>
        /// <param name="data"></param>
        private void OnRefreshUserMenu(object data)
        {
            try
            {
                var buildingMaterialReplacer = gameObject.AddComponent<BuildingMaterialReplacer>();
                buildingMaterialReplacer.BuildingID = BuildingID;

                Game.Instance.userMenu.AddButton(
                    go: gameObject,
                    button: new KIconButtonMenu.ButtonInfo(
                        iconName: "action_mirror",
                        text: STRINGS.BUILDINGS.PREFABS.MOVEDOOR.NAME,
                        on_click: new System.Action(() => buildingMaterialReplacer.CopyAndReplace(BuildingID, gameObject)),
                        shortcutKey: Action.BuildingUtility1,
                        tooltipText: STRINGS.BUILDINGS.PREFABS.MOVEDOOR.TOOLTIP
                    )
                );

#if DEBUG
                Game.Instance.userMenu.AddButton(
                    go: gameObject,
                    button: new KIconButtonMenu.ButtonInfo(
                        iconName: "action_mirror",
                        text: "Button Test",
                        on_click: new System.Action(() => ButtonTest()),
                        shortcutKey: Action.BuildingUtility1,
                        tooltipText: "Button Test"
                    )
                );
#endif               
            }
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() + $"4 OnRefreshUserMenu() : {ex}");
            }
        }

#if DEBUG
        /// <summary>
        /// It's test class
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
        private void ButtonTest()
        {
            GameObject obj = null;
            if (SelectTool.Instance.selected != null)
            {
                obj = SelectTool.Instance.selected.gameObject;
                if (obj == null)
                {
                    Debug.LogError(err.GetMessageAndCode() + $"5 ButtonTest(): Selected object found");
                }
            }
            else
            {
                Debug.LogError(err.GetMessageAndCode() + $"6 ButtonTest(): No selected object");
                return;
            }

            discoveryEvent.InitGlobalEvent(obj);

            /* Test global event */
            discoveryEvent.ToggleAllEvent(obj, GameHashes.ClickTile, true);
            discoveryEvent.ToggleAllEvent(obj, GameHashes.BuildingActivated, true);
            discoveryEvent.ToggleAllEvent(obj, GameHashes.ActiveChanged, true);
            discoveryEvent.ToggleAllEvent(obj, GameHashes.ActiveToolChanged, true);
            discoveryEvent.ToggleAllEvent(obj, GameHashes.BuildingCompleteDestroyed, true);
            discoveryEvent.ToggleAllEvent(obj, GameHashes.SetActivator, true);
            discoveryEvent.ToggleAllEvent(obj, GameHashes.NewBuilding, true);
            discoveryEvent.ToggleAllEvent(obj, GameHashes.NewConstruction, true);
            discoveryEvent.ToggleAllEvent(obj, GameHashes.WorkableStartWork, true);
            discoveryEvent.ToggleAllEvent(obj, GameHashes.WorkableCompleteWork, true);
            discoveryEvent.ToggleAllEvent(obj, GameHashes.WorkableStopWork, true);

            /* Test local event */
            discoveryEvent.ToggleAllEvent(obj, GameHashes.DeconstructComplete, false);
        }
#endif
    }
}