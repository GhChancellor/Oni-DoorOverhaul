using UnityEngine;

namespace Door_Overhaul
{
    internal class DiscoveryEvent
    {
        private readonly string nameMod = "### DoorOverhaul ###";

        private readonly string nameClass = "DiscoveryEvent.cs >";

        public Action<object> GameplayEventHandler { get; set; }

        private GameHashes GameHashes { get; set; }

        public GameObject GameObject { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public void AddGlobalEvent(GameHashes gameHashes,
            string description, bool isActive, Action<object> gameplayEventHandler)
        {
            GameHashes = gameHashes;
            Description = description;
            IsActive = isActive;
            GameplayEventHandler = gameplayEventHandler;
            Debug.Log($"{nameMod} {nameClass} AddGlobalEvent > X1 Added {description}");
        }

        public void AddComponetEvent(GameObject gameObject, GameHashes gameHashes,
            string description, bool isActive, Action<object> gameplayEventHandler)
        { 
            GameObject = gameObject;
            GameHashes = gameHashes;
            Description = description;
            IsActive = isActive;
            GameplayEventHandler = gameplayEventHandler;
            Debug.Log($"{nameMod} {nameClass} AddComponetEvent > X2 Added {description}");
        }
    }
}
