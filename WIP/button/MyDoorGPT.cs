//using UnityEngine;

//namespace Door_Overhaul
//{
//    public class MyDoorGPT : KMonoBehaviour
//    {
//        [MyCmpGet]
//        private Deconstructable deconstructable;

//        [MyCmpGet]
//        private PrimaryElement primaryElement;

//        [MyCmpGet]
//        private BuildingHP buildingHP;

//        protected override void OnPrefabInit()
//        {
//            base.OnPrefabInit();
//            Debug.Log("OnPrefabInit called.");
//            Debug.Log("Deconstructable: " + (deconstructable != null ? "Assigned" : "Not assigned"));
//            Debug.Log("PrimaryElement: " + (primaryElement != null ? "Assigned" : "Not assigned"));
//            Debug.Log("BuildingHP: " + (buildingHP != null ? "Assigned" : "Not assigned"));
//            this.Subscribe((int)GameHashes.RefreshUserMenu, new System.Action<object>(OnRefreshUserMenu));
//        }

//        private void OnRefreshUserMenu(object data)
//        {
//            Debug.Log("OnRefreshUserMenu called.");
//            if (deconstructable == null)
//            {
//                Debug.LogError("Deconstructable component is not assigned.");
//            }
//            if (primaryElement == null)
//            {
//                Debug.LogError("PrimaryElement component is not assigned.");
//            }
//            if (buildingHP == null)
//            {
//                Debug.LogError("BuildingHP component is not assigned.");
//            }

//            Game.Instance.userMenu.AddButton(
//                gameObject,
//                new KIconButtonMenu.ButtonInfo(
//                    "action_deconstruct",
//                    "Remove Door",
//                    new System.Action(RemoveDoor),
//                    Action.NumActions,
//                    null,
//                    null,
//                    null,
//                    "Remove this door"
//                ));
//        }

//        private void RemoveDoor()
//        {
//            Debug.Log("RemoveDoor called.");
//            if (deconstructable == null || primaryElement == null || buildingHP == null)
//            {
//                Debug.LogError("Cannot remove door because components are not assigned.");
//                return;
//            }

//            // Get current mass of the object
//            float currentMass = primaryElement.Mass;
//            Debug.Log("Current mass: " + currentMass);

//            // Initiate deconstruction
//            try
//            {
//                Debug.Log("Initiating deconstruction...");
//                deconstructable.Trigger((int)GameHashes.MarkForDeconstruct, null);
//                Debug.Log("Deconstruction initiated successfully.");
//                deconstructable.Trigger((int)GameHashes.RefreshUserMenu, null);
//            }
//            catch (System.Exception ex)
//            {
//                Debug.LogError("Error during deconstruction trigger: " + ex);
//            }

//            // Add the mass back to the storage or environment
//            //primaryElement.AddMass(currentMass);
//        }
//    }
//}
