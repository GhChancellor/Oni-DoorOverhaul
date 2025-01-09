using UnityEngine;

namespace Door_Overhaul
{
    internal class MoveButton : KMonoBehaviour
    {
        private readonly ManagementError err =
            new("# DoorOverhaul > ", "MoveButton.cs > ");

        public string BuildingID { get; set; }

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
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() + 
                    $"1 OnSpawn(): Exception - {ex.Message}  Stack: \n{ex.StackTrace}");
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
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() + 
                    $"2 OnPrefabInit(): Exception - {ex.Message} Stack: \n{ex.StackTrace}");
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
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() + 
                    $"3 OnCleanUp(): Exception - {ex.Message}  Stack: \n{ex.StackTrace}");
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
                Debug.LogError(err.GetMessageAndCode() + 
                    $"4 OnRefreshUserMenu() : {ex.Message} Stack: \n{ex.StackTrace}");
            }
        }

#if DEBUG
        /// <summary>
        /// It's test class
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
        private void ButtonTest()
        {
            // Debug.Log(err.GetMessageAndCode() + $"7 ButtonTest() entered");

            EventAnalytics eventAnalytics = new(this.gameObject);

            EventBroadcast eventBroadcast = new(this.gameObject);
            eventBroadcast.SearchEventsByComponent(EventFilterEnum.ALL);
            
            // eventBroadcast.EventHandlers.Add
            //     (GameHashes.HighlightStatusItem, obj =>
            //     { eventBroadcast.DoSomething(obj); });

            // eventBroadcast.SearchEvents(EventFilterEnum.GAMEHASHES);
            // InitEvent(eventAnalytics);


            /*  init Global event */
            // eventAnalytics.AddEvent(GameHashes.ActiveToolChanged, "ActiveToolChanged", true);
            // eventAnalytics.AddEvent(GameHashes.BuildToolDeactivated, "BuildToolDeactivated", true);
            // eventAnalytics.AddEvent(GameHashes.NewBuilding, "NewBuilding", true);
            // eventAnalytics.AddEvent(GameHashes.BuildingStateChanged, "BuildingStateChanged", true, true);

            // /* init local Event */



            // /* Test - Global Event */
            // eventAnalytics.ToggleEvent(GameHashes.ActiveToolChanged);
            // eventAnalytics.ToggleEvent(GameHashes.BuildToolDeactivated);
            // eventAnalytics.ToggleEvent(GameHashes.NewBuilding);
            // eventAnalytics.ToggleEvent(GameHashes.BuildingStateChanged);

            /* Test -  Component Events */

            // /* Unkwon event */
            // eventAnalytics.ToggleEvent(GameHashes.Craft);
            // eventAnalytics.ToggleEvent(GameHashes.HoverObject);
            // eventAnalytics.ToggleEvent(GameHashes.StartWork);
            // eventAnalytics.ToggleEvent(GameHashes.UseBuilding);
        }

        private void InitEvent(EventAnalytics eventAnalytics)
        {
            // /* Add Global event */
            eventAnalytics.AddEvent(GameHashes.ClickTile, "ClickTile", true);
            eventAnalytics.AddEvent(GameHashes.BuildingActivated, "BuildingActivated", true);
            eventAnalytics.AddEvent(GameHashes.ActiveChanged, "ActiveChanged", true);
            eventAnalytics.AddEvent(GameHashes.ActiveToolChanged, "ActiveToolChanged", true);
            eventAnalytics.AddEvent(GameHashes.BuildingCompleteDestroyed, "BuildingCompleteDestroyed", true);
            eventAnalytics.AddEvent(GameHashes.SetActivator, "SetActivator", true);
            eventAnalytics.AddEvent(GameHashes.NewBuilding, "NewBuilding", true);
            eventAnalytics.AddEvent(GameHashes.NewConstruction, "NewConstruction", true);
            eventAnalytics.AddEvent(GameHashes.WorkableStartWork, "WorkableStartWork", true);
            eventAnalytics.AddEvent(GameHashes.WorkableCompleteWork, "WorkableCompleteWork", true);
            eventAnalytics.AddEvent(GameHashes.WorkableStopWork, "WorkableStopWork", true);

            /* Add Local event */
            eventAnalytics.AddEvent(GameHashes.DeconstructComplete, "DeconstructComplete", false);
            eventAnalytics.AddEvent(GameHashes.SelectObject, "SelectObject", false);


            /* Active Global event */
            eventAnalytics.ToggleEvent(GameHashes.ClickTile);
            eventAnalytics.ToggleEvent(GameHashes.BuildingActivated);
            eventAnalytics.ToggleEvent(GameHashes.ActiveChanged);
            eventAnalytics.ToggleEvent(GameHashes.ActiveToolChanged);
            eventAnalytics.ToggleEvent(GameHashes.BuildingCompleteDestroyed);
            eventAnalytics.ToggleEvent(GameHashes.SetActivator);
            eventAnalytics.ToggleEvent(GameHashes.NewBuilding);
            eventAnalytics.ToggleEvent(GameHashes.NewConstruction);
            eventAnalytics.ToggleEvent(GameHashes.WorkableStartWork);
            eventAnalytics.ToggleEvent(GameHashes.WorkableCompleteWork);
            eventAnalytics.ToggleEvent(GameHashes.WorkableStopWork);

            /* Active Local event */
            eventAnalytics.ToggleEvent(GameHashes.DeconstructComplete);
            eventAnalytics.ToggleEvent(GameHashes.SelectObject);
        }
#endif
    }
}