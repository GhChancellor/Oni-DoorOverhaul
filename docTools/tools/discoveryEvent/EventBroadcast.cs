using UnityEngine;

namespace Door_Overhaul
{
    internal class EventBroadcast
    {
        private readonly ManagementError err =
            new("# DoorOverhaul > ", "EventBroadcast.cs > ");

        public Dictionary<GameHashes, Action<object>> EventHandlers { get; set; } = new();
        public HashSet<GameHashes> ExcludedEvents { get; set; } = new();

        private GameObject gameObject;

        public void SearchEventsByComponent(EventFilterEnum eventFilter)
        {
            try
            {
                var component = gameObject?.GetComponent<KMonoBehaviour>();
                if (component == null)
                {
                    Debug.LogError(err.GetMessageAndCode() +
                        $"7 SearchEventsByComponent > Component not found");
                    return;
                }

                foreach (GameHashes gameHash in Enum.GetValues(typeof(GameHashes)))
                {
                    component.Subscribe((int)gameHash, eventData =>
                    {
                        switch (eventFilter)
                        {
                            case EventFilterEnum.ALL:
                                LogEvent("ALL", gameHash, eventData);
                                break;
                            default:
                                break;
                        }
                    });

                }

            }
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() +
                    $"8 SearchEventsByComponent > Exception: {ex.Message} Stack: \n{ex.StackTrace}");
            }
        }

        public EventBroadcast(GameObject obj)
        {
            try
            {
                if (obj == null)
                {
                    Debug.LogError(err.GetMessageAndCode() + 
                        $"1 EventBroadcast(): Input object is null");
                    return;
                }

                if (SelectTool.Instance.selected?.gameObject == null)
                {
                    Debug.LogError(err.GetMessageAndCode() + 
                        $"2 EventBroadcast(): Selected tool object is null");
                    return;
                }

                gameObject = SelectTool.Instance.selected.gameObject;
            }
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() + 
                    $"3 EventBroadcast(): {ex.Message} Stack: \n{ex.StackTrace}");
            }
        }

        public void SearchEvents(EventFilterEnum eventFilter)
        {
            try
            {
                foreach (GameHashes gameHashes in Enum.GetValues(typeof(GameHashes)))
                {
                    Game.Instance.Subscribe((int)gameHashes, eventData =>
                    {
                        switch (eventFilter)
                        {
                            case EventFilterEnum.ALL:
                                LogEvent("All", gameHashes, eventData);
                                break;
                            case EventFilterEnum.GAMEHASHES when EventHandlers.ContainsKey(gameHashes):
                                LogEvent("GAMEHASHES", gameHashes, eventData);
                                break;
                            case EventFilterEnum.EXCLUDED when !EventHandlers.ContainsKey(gameHashes):
                                break;
                            default:
                                break;
                        }
                    });

                }
            }
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() + 
                    $"4: SearchEvents: {ex.Message} Stack: \n{ex.StackTrace}");
            }
        }

        private void LogEvent(string filterType, GameHashes hash, object data)
        {
            Debug.Log(err.GetMessageAndCode() + 
                $"5: LogEvent: {filterType} - {hash}: {data}");
        }

        public void DoSomething(object data)
        {
            // Debug.Log(err.GetMessageAndCode() + $"6: DoSomething: {data}");
        }

        // Da implementare in un secondo momento

        // private string GetEventData(object data)
        // {
        //     if (data is GameObject go)
        //     {
        //         var building = go.GetComponent<Building>();
        //         var pe = go.GetComponent<PrimaryElement>();
        //         return $"Building={building?.Def?.PrefabID}, Element={pe?.ElementID}";
        //     }
        //     return data?.ToString() ?? "null";
        // }

        //         private void Aggiungere01()
        // {

        // Game.Instance.Subscribe((int)GameHashes.ActiveToolChanged, (object data) =>
        //     {
        //         if (data is SelectTool)
        //         {
        //             // Il SelectTool è stato attivato
        //             // Esegui la tua logica qui
        //             Debug.Log($"SelectTool è stato attivato");
        //         }
        //     });            
        // }    
    }
}