using UnityEngine;

namespace Door_Overhaul
{
    internal class DiscoveryEventManager
    {
        private readonly string nameMod = "### DoorOverhaul ###";

        private readonly string nameClass = "DiscoveryEventManager.cs >";

        private Dictionary<GameHashes, DiscoveryEvent> events = new();

        /// <summary>
        /// Initialize the global event system. This method subscribes to global events and adds specific events for the current game object.
        /// </summary>
        /// <param name="gameObject"></param>
        public void InitGlobalEvent(GameObject gameObject)
        {
            // /* Subscribe to global events */
            AddGlobalEvent(GameHashes.ClickTile, "ClickTile", false,
                (data) => ShowEventActive(data, "ClickTile"));

            // AddGlobalEvent(GameHashes.BuildingActivated, "BuildingActivated", false,
            //    (data) => ShowEventActive(data, "BuildingActivated"));

            // AddGlobalEvent(GameHashes.ActiveChanged, "ActiveChanged", false,
            //    (data) => ShowEventActive(data, "ActiveChanged"));

            // AddGlobalEvent(GameHashes.ActiveToolChanged, "ActiveToolChanged", false,
            //    (data) => ShowEventActive(data, "ActiveToolChanged"));

            // AddGlobalEvent(GameHashes.BuildingCompleteDestroyed, "BuildingCompleteDestroyed", false,
            //    (data) => ShowEventActive(data, "BuildingCompleteDestroyed"));

            // AddGlobalEvent(GameHashes.SetActivator, "SetActivator", false,
            //    (data) => ShowEventActive(data, "SetActivator"));

            // AddGlobalEvent(GameHashes.NewBuilding, "NewBuilding", false,
            //    (data) => ShowEventActive(data, "NewBuilding"));

            // AddGlobalEvent(GameHashes.NewConstruction, "NewConstruction", false,
            //    (data) => ShowEventActive(data, "NewConstruction"));

            // AddGlobalEvent(GameHashes.WorkableStartWork, "WorkableStartWork", false,
            //    (data) => ShowEventActive(data, "WorkableStartWork"));

            // AddGlobalEvent(GameHashes.WorkableStopWork, "WorkableStopWork", false,
            //    (data) => ShowEventActive(data, "WorkableStopWork"));

            // AddGlobalEvent(GameHashes.WorkableCompleteWork, "WorkableCompleteWork", false,
            //    (data) => ShowEventActive(data, "WorkableCompleteWork"));

            // /* Subscribe to component events */
            AddComponetEvent(gameObject, GameHashes.DeconstructComplete, "DeconstructComplete", false,
               (data) => ShowEventActive(data, "DeconstructComplete"));

            Debug.Log("\n**************************************");
        }

        /// <summary>
        /// Adds global event to the dictionary.
        /// </summary>
        /// <param name="gameHash"></param>
        /// <param name="description"></param>
        /// <param name="isActive"></param>
        /// <param name="gameplayEventHandler"></param>
        private void AddGlobalEvent(GameHashes gameHashes,
            string description, bool isActive, Action<object> gameplayEventHandler)
        {
            if (!events.ContainsKey(gameHashes))
            {
                var discovertCurrentEvent = new DiscoveryEvent();
                discovertCurrentEvent.AddGlobalEvent(gameHashes, description, isActive,
                    gameplayEventHandler);
                events.Add(gameHashes, discovertCurrentEvent);
                // Debug.Log($"{nameMod} {nameClass} AddGlobalEvent > X13  add: {gameHashes}");
                return;
            }

            // Debug.LogWarning("{nameMod} {nameClass} AddGlobalEvent > X14  already exists: {gameHashes} -> {isActive}");
        }

        /// <summary>
        /// Adds componet event to the dictionary.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="gameHash"></param>
        /// <param name="description"></param>
        /// <param name="isActive"></param>
        /// <param name="gameplayEventHandler"></param>
        private void AddComponetEvent(GameObject gameObject, GameHashes gameHashes,
            string description, bool isActive, Action<object> gameplayEventHandler)
        {

            if (gameObject == null)
            {
                Debug.LogError($"{nameMod} {nameClass} AddComponetEvent > X1 Object is null");
                return;
            }

            if (!events.ContainsKey(gameHashes))
            {
                var discovertCurrentEvent = new DiscoveryEvent();

                discovertCurrentEvent.AddComponetEvent(gameObject, gameHashes, description, isActive,
                    gameplayEventHandler);

                events.Add(gameHashes, discovertCurrentEvent);
                // Debug.Log($"{nameMod} {nameClass} AddComponetEvent > X2 GameHashes: '{gameHashes}' created");
                return;
            }

            // Debug.LogWarning($"{nameMod} {nameClass} AddComponetEvent > X3 GameHashes already exists: '{gameHashes}'");
        }

        /// <summary>
        /// Enable global the event.
        /// </summary>
        /// <param name="gameHash"></param>
        private void EnableGlobalEvent(GameHashes gameHash)
        {
            if (events.TryGetValue(gameHash, out var currentEvent_) && !currentEvent_.IsActive)
            {
                Game.Instance.Subscribe((int)gameHash, currentEvent_.GameplayEventHandler);
                currentEvent_.IsActive = true;
                // Debug.Log($"{nameMod} {nameClass} EnableGlobalEvent > X4 Subscribe {currentEvent_.Description}");
                return;
            }

            // Debug.LogWarning($"{nameMod} {nameClass} EnableGlobalEvent > X15 GameHashes '{gameHash}' is not active");
        }

        /// <summary>
        /// Enables component the event.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="gameHash"></param>
        private void EnableComponetEvent(GameObject gameObject, GameHashes gameHash)
        {
            if (gameObject == null)
            {
                Debug.LogError($"{nameMod} {nameClass} EnableComponetEvent > Object is null");
                return;
            }

            var component = gameObject.GetComponent<KMonoBehaviour>();
            if (component == null)
            {
                Debug.LogError($"{nameMod} {nameClass} EnableComponetEvent > No suitable component found");
                return;
            }

            if (events.TryGetValue(gameHash, out var currentEvent_))
            {
                component.Subscribe((int)gameHash, currentEvent_.GameplayEventHandler);
                currentEvent_.IsActive = true;
                Debug.Log($"{nameMod} {nameClass} EnableComponetEvent > X5 Subscribe {currentEvent_.Description}");
                return;
            }

            // Debug.LogWarning($"{nameMod} {nameClass} EnableComponetEvent > X16 No matching event found for game hash");
        }

        /// <summary>
        /// Disable global the event.
        /// </summary>
        /// <param name="gameHash"></param>
        private void DisableGlobalEvent(GameHashes gameHash)
        {
            if (events.TryGetValue(gameHash, out var currentEvent_) && currentEvent_.IsActive)
            {
                Game.Instance.Unsubscribe((int)gameHash, currentEvent_.GameplayEventHandler);
                currentEvent_.IsActive = false;
                Debug.Log($"{nameMod} {nameClass} DisableGlobalEvent > X6 Unsubscribe {currentEvent_.Description}");
                return ;
            }

            // Debug.LogWarning($"{nameMod} {nameClass} DisableGlobalEvent > X17 Event not found or already disabled");
        }

        /// <summary>
        /// Disable component the event.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="gameHash"></param>
        private void DisableComponetEvent(GameObject gameObject, GameHashes gameHash)
        {
            if (gameObject == null)
            {
                Debug.LogWarning($"{nameMod} {nameClass} DisableComponetEvent > Object is null");
                return;
            }

            var component = gameObject.GetComponent<KMonoBehaviour>();
            if (component == null)
            {
                Debug.LogWarning($"{nameMod} {nameClass} DisableComponetEvent > No suitable component found");
                return;
            }

            if (events.TryGetValue(gameHash, out var currentEvent_) && currentEvent_.IsActive)
            {
                component.Unsubscribe((int)gameHash, currentEvent_.GameplayEventHandler);
                currentEvent_.IsActive = false;
                Debug.Log($"{nameMod} {nameClass} DisableComponetEvent > X7 {currentEvent_.Description}");
                return;
            }

            // Debug.LogWarning($"{nameMod} {nameClass} DisableComponetEvent > X18 No active event found for hash: {gameHash}");

        }

        public void ToggleAllEvent(GameObject gameObject, GameHashes gameHash, bool globalEvent)
        {
            if (!events.TryGetValue(gameHash, out var currentEvent))
                return;

            try
            {
                if (globalEvent)
                {
                    if (currentEvent.IsActive)
                    {
                        DisableGlobalEvent(gameHash);
                        // Debug.Log($"{nameMod} {nameClass} ToggleAllEvent > X8 DisableGlobalEvent : {gameHash}");
                        return;
                    }
                    else
                    {
                        EnableGlobalEvent(gameHash);
                        // Debug.Log($"{nameMod} {nameClass} ToggleAllEvent > X9 EnableGlobalEvent : {gameHash}");
                        return;
                    }
                }
                else
                {
                    if (currentEvent.IsActive)
                    {
                        DisableComponetEvent(gameObject, gameHash);
                        // Debug.Log($"{nameMod} {nameClass} ToggleAllEvent > X10 DisableLocalEvent : {gameObject.name} - {gameHash}");
                        return;
                    }
                    else
                    {
                        EnableComponetEvent(gameObject, gameHash);
                        // Debug.Log($"{nameMod} {nameClass} ToggleAllEvent > X11 EnableLocalEvent : {gameObject.name} - {gameHash}");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"{nameMod} {nameClass} ToggleAllEvent > X19 Error: {gameObject?.name} {gameHash} {ex.Message}");
            }
        }


        /// <summary>
        /// Shows an event active message.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="description"></param>
        private void ShowEventActive(object data, string description)
        {
            Debug.Log($"{nameMod} {nameClass} ShowEventActive > X12 Event {description}: {data?.GetType()}");
        }
    }
}