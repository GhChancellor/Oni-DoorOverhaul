using HarmonyLib;
using static Door_Overhaul.TextLocalization;
using static Door_Overhaul.BuildTechIntegrationManager;

namespace Door_Overhaul
{
    internal class Patch

    {
        private static readonly ManagementError err =
            new("# DoorOverhaul > ", "Patch.cs > ");
        
        [HarmonyPatch(typeof(Localization), "Initialize")]
        /// <summary>
        /// Patch Localization Initialize.
        /// </summary>
        public static class Localization_Initialize_Patch
        {
            /// <summary>
            /// Init Localization
            /// </summary>
            public static void Postfix()
            {
                try
                {
                    Translate(typeof(STRINGS));
                }
                catch (Exception ex)
                {
                    Debug.LogError(err.GetMessageAndCode() + $"1 Postfix() traslate {ex.Message}");
                }
            }
        }

        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch(nameof(Db.Initialize))]
        /// <summary>
        /// Patch Db Initialize.
        /// </summary>
        public static class Db_Initialize_Patch
        {
            /// <summary>
            /// Init Db_Initialize
            /// </summary>
            public static void Postfix()
            {
                try
                {
                    AddBuildingToMenu(PneumaticTrapDoor.GetCategoryMenu(), PneumaticTrapDoor.GetBuildingID(),
                        PneumaticTrapDoor.GetSubCategoryID(), PneumaticTrapDoor.GetTechID());
                }
                catch (Exception ex)
                {
                    Debug.LogError(err.GetMessageAndCode() + $"2 Postfix() add building menu {ex.Message}");
                }
                // AddBuildingToMenu(PneumaticTrapDoorReplace.GetCategoryMenu(), PneumaticTrapDoorReplace.GetBuildingID(),
                //     PneumaticTrapDoorReplace.GetSubCategoryID(), PneumaticTrapDoorReplace.GetTechID());
                // Debug.Log("Lele - pneumaticTrapDoorReplace.GetID() " + PneumaticTrapDoorReplace.GetBuildingID());
            }
        }
    }
}
