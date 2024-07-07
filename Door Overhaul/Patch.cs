using HarmonyLib;
using static Door_Overhaul.TextLocalization;
using static Door_Overhaul.DoorManagement;

namespace Door_Overhaul
{
    internal class Patch

    {
        [HarmonyPatch(typeof(Localization), "Initialize")]
        public static class Localization_Initialize_Patch
        {
            /// <summary>
            /// Init Localization
            /// </summary>
            public static void Postfix()
            {
               Translate(typeof(STRINGS));
            }
        }

        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch(nameof(Db.Initialize))]
        public static class Db_Initialize_Patch
        {
            /// <summary>
            /// Init Db_Initialize
            /// </summary>
            public static void Postfix()
            {
                AddDoorToBuildMenu(PneumaticTrapDoor.categoryMenu, PneumaticTrapDoor.ID, PneumaticTrapDoor.subCategoryID);
                AddDoorToTechTree(PneumaticTrapDoor.ID, PneumaticTrapDoor.tech);

                RegisterNewDoor();
            }

            private static void RegisterNewDoor()
            {
                PneumaticTrapDoor02 pneumaticTrapDoor02 = new PneumaticTrapDoor02();
                BuildingDef newBuildingDef = pneumaticTrapDoor02.CreateBuildingDef();
                if (newBuildingDef != null)
                {
                    Assets.AddBuildingDef(newBuildingDef);
                    Debug.Log($"Registered new building: {newBuildingDef.PrefabID}");
                }
            }

        }
    }
}
