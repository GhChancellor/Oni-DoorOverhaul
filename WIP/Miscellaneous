using UnityEngine;

namespace Door_Overhaul
{
    internal class Miscellaneous : KMonoBehaviour
    {
        private string nameMod = "### DoorOverhaul ###";
        private string nameClass = "Miscellaneous.cs >";

        // Core components
        private BuildingDef buildingDef;

        public static string BuildingID { get; set; }

        private void CreateAndDestroyItems(object data)
        {
            try
            {
                Debug.Log("DoorOverhaul: Starting OnNewBuilding");

                GameObject newBuilding = data as GameObject;
                Vector3 position = newBuilding.transform.position;

                GameObject spawned = Util.KInstantiate(buildingDef.BuildingComplete, position);
                PrimaryElement spawnedPE = spawned.GetComponent<PrimaryElement>();

                spawnedPE.SetElement(SimHashes.Aluminum);
                spawned.SetActive(true);
                Util.KDestroyGameObject(newBuilding);

            }
            catch (Exception ex)
            {
                Debug.LogError($"DoorOverhaul: Error in OnNewBuilding: {ex.GetType()} - {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private void TrackItems(GameObject sourceDoor, GameObject targetDoor)
        {
            var sourceDoorObj = sourceDoor.GetComponent<KPrefabID>();
            var targetDoorObj = targetDoor.GetComponent<KPrefabID>();

            string sourceDoorName;
            string targetDoorName;

            PrimaryElement sourceDoorElement;
            PrimaryElement targetDoorElement;

            int sourceDoorID;
            int targetDoorID;

            if (sourceDoorObj == null)
            {
                Debug.Log($"{nameMod} {nameClass} TrackItems > sourceDoorId1 is null ");
            }

            if (targetDoorObj == null)
            {
                Debug.Log($"{nameMod} {nameClass} TrackItems > targetDoorId2 is null ");
            }

            if (sourceDoorObj.InstanceID == targetDoorObj.InstanceID)
            {
                sourceDoorID = sourceDoor.GetComponent<KPrefabID>().InstanceID;
                sourceDoorName = sourceDoor.GetComponent<KPrefabID>().PrefabTag.Name;
                sourceDoorElement = sourceDoor.GetComponent<PrimaryElement>();
                Debug.Log($"{nameMod} {nameClass} TrackItems > sourceDoorId1 e targetDoorId2 sono uguali ");
                Debug.Log($"{nameMod} {nameClass} TrackItems > {sourceDoorID} {sourceDoorName} {sourceDoorElement} ");
            }
            else
            {
                sourceDoorID = sourceDoor.GetComponent<KPrefabID>().InstanceID;
                sourceDoorName = sourceDoor.GetComponent<KPrefabID>().PrefabTag.Name;
                sourceDoorElement = sourceDoor.GetComponent<PrimaryElement>();

                targetDoorID = targetDoor.GetComponent<KPrefabID>().InstanceID;
                targetDoorName = targetDoor.GetComponent<KPrefabID>().PrefabTag.Name;
                targetDoorElement = targetDoor.GetComponent<PrimaryElement>();


                Debug.Log($"{nameMod} {nameClass} TrackItems > \n sourceDoorID: {sourceDoorID} \n sourceDoorName: {sourceDoorName} \n Element: {sourceDoorElement.ElementID} ");
                Debug.Log($"{nameMod} {nameClass} TrackItems > \n targetDoorID: {targetDoorID} \n targetDoorName: {targetDoorName} \n Element: {targetDoorElement.ElementID} ");
            }
        }
    }
}