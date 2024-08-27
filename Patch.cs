using HarmonyLib;
using static Door_Overhaul.TextLocalization;
using static Door_Overhaul.BuildTechIntegrationManager;

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
                AddBuildingToMenu(PneumaticTrapDoor.GetCategoryMenu(), PneumaticTrapDoor.GetBuildingID(), 
                    PneumaticTrapDoor.GetSubCategoryID(), PneumaticTrapDoor.GetTechID());

                AddBuildingToMenu(PneumaticTrapDoorReplace.GetCategoryMenu(), PneumaticTrapDoorReplace.GetBuildingID(), 
                    PneumaticTrapDoorReplace.GetSubCategoryID(), PneumaticTrapDoorReplace.GetTechID() );

                Debug.Log("Lele - pneumaticTrapDoor.GetID() " + PneumaticTrapDoor.GetBuildingID());
                Debug.Log("Lele - pneumaticTrapDoorReplace.GetID() " + PneumaticTrapDoorReplace.GetBuildingID());

            }
        }
    }
}
