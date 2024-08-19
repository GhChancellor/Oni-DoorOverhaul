using HarmonyLib;
using static Door_Overhaul.TextLocalization;
using static Door_Overhaul.DoorManagement;
using static Door_Overhaul.STRINGS.BUILDINGS.PREFABS;

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
                //AddDoorToBuildMenu(PneumaticTrapDoor.CategoryMenu, PneumaticTrapDoor.ID, PneumaticTrapDoor.SubCategoryID);
                //AddDoorToTechTree(PneumaticTrapDoor.ID, PneumaticTrapDoor.Tech);

                //AddDoorToBuildMenu(PneumaticTrapDoorReplace.CategoryMenu, PneumaticTrapDoorReplace.ID, PneumaticTrapDoorReplace.SubCategoryID);

                //AddDoorToTechTree(PneumaticTrapDoorReplace.ID, PneumaticTrapDoorReplace.Tech);

                //Debug.Log(">> pneumaticTrapDoor.GetID() " + PneumaticTrapDoor.ID);
                //Debug.Log(">> pneumaticTrapDoorReplace.GetID() " + PneumaticTrapDoorReplace.ID);

                AddDoorToBuildMenu(PneumaticTrapDoor.GetCategoryMenu(),
                    PneumaticTrapDoor.GetID(), PneumaticTrapDoor.GetSubCategoryID());

                AddDoorToTechTree(PneumaticTrapDoor.GetID(), PneumaticTrapDoor.GetTech());

                AddDoorToBuildMenu(PneumaticTrapDoorReplace.GetCategoryMenu(),
                    PneumaticTrapDoorReplace.GetID(), PneumaticTrapDoorReplace.GetSubCategoryID());

                AddDoorToTechTree(PneumaticTrapDoorReplace.GetID(), PneumaticTrapDoorReplace.GetTech());

                Debug.Log(">> pneumaticTrapDoor.GetID() " + PneumaticTrapDoor.GetID());
                Debug.Log(">> pneumaticTrapDoorReplace.GetID() " + PneumaticTrapDoorReplace.GetID());


            }
        }
    }
}
