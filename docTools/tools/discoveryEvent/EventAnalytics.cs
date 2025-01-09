using UnityEngine;

namespace Door_Overhaul
{
    internal class EventAnalytics
    {
        private readonly ManagementError err =
            new("# DoorOverhaul > ", "EventAnalytics.cs > ");

        private Dictionary<GameHashes, DiscoveryEvent> events = new();

        private GameObject gameObject;
        public EventAnalytics(GameObject obj)
        {
            try
            {
                if (obj == null)
                {
                    Debug.LogError(err.GetMessageAndCode() + 
                        $"1  EventAnalytics(): Null object passed.");
                    return;
                }

                if (SelectTool.Instance.selected != null)
                {
                    obj = SelectTool.Instance.selected.gameObject;
                    if (obj == null)
                    {
                        Debug.LogError(err.GetMessageAndCode() + 
                            $"2  EventAnalytics(): Selected object found");
                    }
                }

                gameObject = obj;
            }
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() + 
                    $"3 EventAnalytics(): Exception: " + ex.Message);
            }
        }

        public void ToggleEvent(GameHashes gamehash)
        {
            if (!events.TryGetValue(gamehash, out var currentEvent)) return;

            try
            {
                if (currentEvent.IsGlobalEvent)
                {
                    if (currentEvent.IsActive)
                    {
                        Game.Instance.Unsubscribe((int)gamehash, currentEvent.GameplayEventHandler);
                    }
                    else
                    {
                        Game.Instance.Subscribe((int)gamehash, currentEvent.GameplayEventHandler);
                    }
                }
                else
                {
                    var component = gameObject?.GetComponent<KMonoBehaviour>();
                    if (component == null)
                    {
                        Debug.LogError(err.GetMessageAndCode() + $"4 ToggleEvent > Component not found");
                        return;
                    }

                    if (currentEvent.IsActive)
                    {
                        component.Unsubscribe((int)gamehash, currentEvent.GameplayEventHandler);
                    }
                    else
                    {
                        component.Subscribe((int)gamehash, currentEvent.GameplayEventHandler);
                    }
                }

                currentEvent.IsActive = !currentEvent.IsActive;
                LogEventStatus(currentEvent);
            }
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() + 
                    $"5 ToggleEvent > Error: {ex.Message} Stack: \n{ex.StackTrace}");
            }
        }

        public void AddEvent(GameHashes gameHash, string description, bool isGlobal, bool verbose = false)
        {
            if (events.ContainsKey(gameHash)) return;

            var discoveryEvent = new DiscoveryEvent();
            if (isGlobal)
            {
                discoveryEvent.AddGlobalEvent(gameHash, description, false,
                    data => ShowEventActive(data, description, verbose), verbose);
            }
            else
            {
                if (gameObject == null)
                {
                    Debug.LogError(err.GetMessageAndCode() + 
                        $"6 AddEvent > GameObject is null");
                    return;
                }
                discoveryEvent.AddComponetEvent(gameObject, gameHash, description, false,
                    data => ShowEventActive(data, description, verbose), verbose);
            }

            events.Add(gameHash, discoveryEvent);
        }

        private void LogEventStatus(DiscoveryEvent evt)
        {
            var status = evt.IsActive ? "Subscribe" : "Unsubscribe";
            Debug.Log(err.GetMessageAndCode() + 
                $"7 LogEventStatus > {status} {evt.Description}");
        }

        private void ShowEventActive(object data, string description, bool verbose)
        {
            if (!verbose)
            {
                Debug.Log(err.GetMessageAndCode() + 
                    $"8 ShowEventActive > Event {description}: {data?.GetType()}");
                return;
            }

            var gameObj = data as GameObject;
            var details = GetObjectDetails(gameObj, description);
            Debug.Log(err.GetMessageAndCode() + 
                $"9 ShowEventActive > Event {description}: {details}");
        }

        private string GetObjectDetails(GameObject gameObj, string eventType)
        {
            if (gameObj == null) return "null";

            var sb = new System.Text.StringBuilder();
            var building = gameObj.GetComponent<Building>();
            var primaryElement = gameObj.GetComponent<PrimaryElement>();

            switch (eventType)
            {
                case "BuildingStateChanged":
                    sb.Append($"HasDef:{building?.Def != null}, ")
                      .Append($"DefID:{building?.Def?.PrefabID}, ")
                      .Append($"ElementID:{primaryElement?.ElementID}, ")
                      .Append($"Mass:{primaryElement?.Mass}, ")
                      .Append($"Orientation:{building?.Orientation}, ")
                      .Append($"PlacementCells:{building?.PlacementCells?.Length ?? 0}");
                    break;
                default:
                    sb.Append($"GameObject Type: {gameObj.GetType()}");
                    break;
            }

            return sb.ToString();
        }
    
    }
}