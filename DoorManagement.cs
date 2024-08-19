namespace Door_Overhaul
{
    internal class DoorManagement
    {
        /// <summary>
        /// Add door to build menu
        /// </summary>
        /// <param name="categoryMenu">description</param>
        /// <param name="buildingID"></param>
        /// <param name="subCategoryID">subCategoryID</param>
        public static void AddDoorToBuildMenu(string categoryMenu, string buildingID, string subCategoryID)
        {
            ModUtil.AddBuildingToPlanScreen(categoryMenu, buildingID, subCategoryID);
        }

        /// <summary>
        /// Add Door to Tech Tree
        /// </summary>
        /// <param name="buildingID"></param>
        /// <param name="tech"></param>
        public static void AddDoorToTechTree(string buildingID, string tech)
        {
            if (tech == "none")
            {
                return;
            }
        }
    }
}
