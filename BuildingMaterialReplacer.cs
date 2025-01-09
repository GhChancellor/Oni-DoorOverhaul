using UnityEngine;

namespace Door_Overhaul
{
    internal class BuildingMaterialReplacer : KMonoBehaviour
    {
        private readonly ManagementError err =
            new("# DoorOverhaul > ", "BuildingMaterialReplacer.cs > ");

        // Core components
        private GameObject sourceObject;
        private BuildingDef buildingDef;
        private PrimaryElement sourcePE;
        private Element unobtaniumElement;
        private const float MIN_MATERIAL_MASS = 0.5f;

        private Action<object> newBuildingHandler;

        [MyCmpGet] private readonly Deconstructable deconstructable;

        public string BuildingID { get; set; }

        private static readonly Dictionary<string, Action<Deconstructable>> destroyActions =
            new Dictionary<string, Action<Deconstructable>>
        {
            { PneumaticTrapDoor.GetBuildingID(), (d) => new PneumaticTrapDoorManager().Destroy(d) },
            // { PneumaticTrapDoorReplace.GetBuildingID(), (d) => new PneumaticTrapDoorManager().Destroy(d) }

            // { NoPawNoProblemDoor.GetBuildingID(), (d) => new NoPawNoProblemDoorManager().Destroy(d) },
            // { NoPawNoProblemDoorReplace.GetBuildingID(), (d) => new NoPawNoProblemDoorManager().Destroy(d) }
        };

        public BuildingMaterialReplacer() { }

        /// <summary>
        /// Initializes all components required for material replacement.
        /// </summary>
        /// <param name="buildingID"></param>
        /// <param name="gameObject"></param>
        private void InitializeComponents(string buildingID, GameObject gameObject)
        {
            try
            {
                sourceObject = gameObject;
                buildingDef = Assets.GetBuildingDef(GetComponent<KPrefabID>().PrefabTag.Name);
                sourcePE = sourceObject.GetComponent<PrimaryElement>();
                unobtaniumElement = ElementLoader.FindElementByHash(SimHashes.Unobtanium);
                BuildingID = buildingID;
            }
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() +
                    $"1 InitializeComponents() : Failed to initialize components {ex.Message} Stack: \n{ex.StackTrace}");
            }
        }
        public void CopyAndReplace_ORI(string buildingID, GameObject gameObject)
        {
            try
            {
                // Debug.Log(err.GetMessageAndCode() + $"13 CopyAndReplace() : Before initialization of components");

                // Initialize components required for material replacement
                InitializeComponents(buildingID, gameObject);

                // Subscribe to the BuildToolDeactivated event to ensure 
                // that the new building is not created until after it has been fully replaced with Unobtanium
                Game.Instance.Subscribe((int)GameHashes.BuildToolDeactivated, new Action<object>(OnBuildToolDeactivatedEvent));

                var component = gameObject.GetComponent<KMonoBehaviour>();
                component.Subscribe((int)GameHashes.DeconstructComplete, new Action<object>(SpawnUnobtaniumElement));

                // Set up a handler for the NewBuilding event, which will be triggered when a 
                // new building is created using the Build Tool
                newBuildingHandler = data => OnNewBuilding(data, sourcePE.ElementID);

                // Subscribe to the NewBuilding event using the set-up handler
                Game.Instance.Subscribe((int)GameHashes.NewBuilding, newBuildingHandler);

                // Remove all materials from the current building to prepare it for replacement with Unobtanium
                RemoveMaterial();

                // Copy the existing building order and replace it with Unobtanium in the Plan Screen
                PlanScreen.Instance.CopyBuildingOrder(buildingDef, unobtaniumElement.tag.Name);

                // Activate the Build Tool and add Unobtanium to its materials list
                BuildTool.Instance.Activate(
                    buildingDef,
                    new List<Tag> { unobtaniumElement.tag }
                );
            }
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() +
                    $"2 CopyAndReplace(): Failed during copy and replace operation {ex.Message} Stack: \n{ex.StackTrace}");
            }
        }


        /// <summary>
        /// Copies the existing building and replaces its materials with Unobtanium.
        /// </summary>
        /// <param name="buildingID"></param>
        /// <param name="gameObject"></param>
        public void CopyAndReplace(string buildingID, GameObject gameObject)
        {
            CopyAndReplace_ORI(buildingID, gameObject);
        }

        /// <summary>
        /// Removes any materials used by the source prefab.
        /// </summary>
        private void RemoveMaterial()
        {
            try
            {
                // Check if the primary element is not null and has a mass greater than or equal to MIN_MATERIAL_MASS
                if (sourcePE == null || sourcePE.Mass <= MIN_MATERIAL_MASS) return;

                // Get the world inventory for cluster 0
                WorldInventory inventory = ClusterManager.Instance.GetWorld(0).worldInventory;

                // Get the tag of the primary element's material
                Tag materialTag = ElementLoader.FindElementByHash(sourcePE.ElementID).tag;

                // Calculate the mass of material to remove
                float massToRemove = sourcePE.Mass;

                // Iterate over pickupables in the inventory with the matching material tag
                foreach (var pickupable in inventory.GetPickupables(materialTag, false))
                {
                    // Check if the pickupable has a mass greater than or equal to the mass to remove
                    if (pickupable?.PrimaryElement.Mass >= massToRemove)
                    {
                        // Trigger removal events for the pickupable's object
                        TriggerInventoryRemovalEvents(pickupable.gameObject, inventory);
                        // Delete the pickupable's object
                        pickupable.gameObject.DeleteObject();

                        // Break out of the loop since we've removed enough material
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() +
                    $"3 RemoveMaterial(): Failed to remove material from inventory {ex.Message} Stack: \n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// Called when the BuildTool is deactivated.
        /// </summary>
        /// <param name="data">BuildToolDeactivatedEvent data.</param>
        private void OnBuildToolDeactivatedEvent(object data)
        {
            try
            {
                if (destroyActions.TryGetValue(BuildingID, out var destroyAction))
                {
                    destroyAction(deconstructable);
                }

                Game.Instance.Unsubscribe((int)GameHashes.BuildToolDeactivated, new Action<object>(OnBuildToolDeactivatedEvent));

            }
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() +
                    $"4 OnBuildToolDeactivatedEvent(object data): Error in build tool deactivated event {ex.Message} Stack: \n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// Spawns the Unobtanium Element at the location of the building.
        /// </summary>
        private void SpawnUnobtaniumElement(object data)
        {
            try
            {
                // Convert the position of the source object to a cell index in the game grid
                int cell = Grid.PosToCell(sourceObject.transform.position);

                // Convert the cell index back to a world position, placing it at the ore layer 
                // (which is usually slightly above ground level)
                Vector3 position = Grid.CellToPosCBC(cell, Grid.SceneLayer.Ore);

                // Spawn an instance of the Unobtanium Element resource at the specified position
                unobtaniumElement.substance.SpawnResource(
                    position: position,
                    mass: sourcePE.Mass,
                    temperature: sourcePE.Temperature,
                    disease_idx: byte.MaxValue,
                    disease_count: 0,
                    prevent_merge: false,
                    forceTemperature: false,
                    manual_activation: false
                );
            }
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() +
                    $"5 SpawnUnobtaniumElement(): Failed to spawn Unobtanium element {ex.Message} Stack: \n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// Calls the RefreshUserMenu method of the user menu.
        /// </summary>
        /// <param name="itemToRemove"></param>
        /// <param name="inventory"></param>
        private void TriggerInventoryRemovalEvents(GameObject itemToRemove, WorldInventory inventory)
        {
            try
            {
                Game.Instance.Trigger((int)GameHashes.RemovedFetchable, itemToRemove);
                inventory.gameObject.Trigger((int)GameHashes.OnStorageChange, itemToRemove);
                inventory.gameObject.Trigger((int)GameHashes.OnStorageInteracted, inventory);
            }
            catch (Exception ex)
            {
                Debug.LogWarning(err.GetMessageAndCode() +
                    $"6 TriggerInventoryRemovalEvents(): Exception caught during event triggering {ex.Message} Stack: \n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// Called when a new building is created. 
        /// Sets the element ID of the spawned building to the given element ID and copies its temperature 
        /// and position from the original building. Activates the spawned building.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="elementID"></param>
        private void OnNewBuilding(object data, SimHashes elementID)
        {
            try
            {
                // Retrieve the newly created building from the event data
                GameObject newBuilding = data as GameObject;

                // Get the position of the new building
                Vector3 position = newBuilding.transform.position;
                Debug.Log(err.GetMessageAndCode() +
                    $"14 OnNewBuilding(): Building State: {GetStateString(newBuilding.GetComponent<Building>())}");

                // Spawn a new building using the provided building definition and position
                GameObject spawned = Util.KInstantiate(buildingDef.BuildingComplete, position);

                // Access the PrimaryElement component of both the newly created building and the original one
                PrimaryElement spawnedPE = spawned.GetComponent<PrimaryElement>();
                PrimaryElement newBuildingPE = newBuilding.GetComponent<PrimaryElement>();

                // Set the element ID for the newly spawned building to Unobtanium
                spawnedPE.SetElement(elementID);

                // Copy the temperature of the original building's primary element to the newly spawned one
                spawnedPE.Temperature = newBuildingPE.Temperature;

                // Activate the newly created building
                spawned.SetActive(true);

                // Destroy the old building object since it is no longer needed
                Util.KDestroyGameObject(newBuilding);

                // Unsubscribe from NewBuilding event as it's no longer required after this method has run once
                if (newBuildingHandler != null)
                {
                    Game.Instance.Unsubscribe((int)GameHashes.NewBuilding, newBuildingHandler);
                    newBuildingHandler = null;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() +
                    $"7 OnNewBuilding(): Failed to spawn Unobtanium element and destroy original: {ex.Message} Stack: \n{ex.StackTrace}");
            }
        }

        private string GetStateString(Building building)
        {
            if (building == null) return "null";
            return $"HasDef:{building.Def != null}, Orientation:{building.Orientation}, PlacementCells:{building.PlacementCells?.Length ?? 0}";
        }

        // **********************************************************

        private void Test(object data)
        {
            Debug.Log(err.GetMessageAndCode() + "8 Test(): Function Test executed");
            try
            {
                Debug.Log(err.GetMessageAndCode() + $"9 Test()");

            }
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() +
                    $"8 Test(): Failed to execute Test method {ex.Message} Stack: \n{ex.StackTrace}");
            }
            Debug.Log(err.GetMessageAndCode() + "11 Test(): Test method finished executing");
        }

        // ***************************************

    }
}