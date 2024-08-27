﻿namespace Door_Overhaul
{
    internal class BuildTechIntegrationManager
    {
        /// <summary>
        /// Adds a building to the construction menu and unlocks it in the tech tree if applicable.
        /// </summary>
        /// <param name="categoryMenu">The category in the construction menu where the building should be added</param>
        /// <param name="buildingID">The unique identifier for the building</param>
        /// <param name="subCategoryID">The subcategory in the construction menu (can be null)</param>
        /// <param name="techID">The technology ID required to unlock the building ("none" if no tech is required)</param>       
        public static void AddBuildingToMenu(string categoryMenu, string buildingID, string subCategoryID, string techID)
        {
            ModUtil.AddBuildingToPlanScreen(categoryMenu, buildingID, subCategoryID);

            var groupID = Db.Get().Techs.TryGet(techID);

            if (techID == "none")
                return;

            groupID.unlockedItemIDs.Add(buildingID);
        }
    }
}