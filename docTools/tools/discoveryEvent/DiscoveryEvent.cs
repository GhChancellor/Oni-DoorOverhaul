using UnityEngine;

namespace Door_Overhaul
{
    internal class DiscoveryEvent
    {
        private readonly ManagementError err =
            new("# DoorOverhaul > ", "DiscoveryEvent.cs > ");

        public bool IsActive { get; set; }
        public string Description { get; private set; }
        public GameHashes GameHash { get; private set; }
        public Action<object> GameplayEventHandler { get; private set; }
        public bool Verbose { get; set; } 
        public bool IsGlobalEvent { get; private set; }
        public GameObject GameObject { get; private set; }

        public void AddGlobalEvent(GameHashes gameHash, string description, bool isActive, Action<object> handler, bool verbose )
        {
            GameHash = gameHash;
            Description = description;
            IsActive = isActive;
            GameplayEventHandler = handler;
            IsGlobalEvent = true;
            Verbose = verbose;

            Debug.Log(err.GetMessageAndCode() + $"1 AddGlobalEvent > Added {Description}");
        }

        public void AddComponetEvent(GameObject gameObject, 
            GameHashes gameHash, string description, bool isActive, Action<object> handler, bool verbose )
        {
            GameHash = gameHash;
            Description = description;
            IsActive = isActive;
            GameplayEventHandler = handler;
            IsGlobalEvent = false;
            GameObject = gameObject;
            Verbose = verbose;

            Debug.Log(err.GetMessageAndCode() + $"2 AddComponetEvent > Added {Description}");
        }
    }
}
