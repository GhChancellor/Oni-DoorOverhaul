namespace Door_Overhaul
{
    internal class PneumaticTrapDoorManager : IDoorOperations
    {
        private readonly ManagementError err =
            new("# DoorOverhaul > ", "PneumaticTrapDoorManager.cs > ");

        private float[] constructionMass;
        private float constructionTime;

        /// <summary>
        /// Change recipe value constructionMass and constructionTime 
        /// for the construction
        /// </summary>
        /// <returns>float[] constructionMass, float constructionTime</returns>
        public (float[] constructionMass, float constructionTime) Create()
        {
            try
            {
                /* 50 kg */
                constructionMass = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;

                /* 5 seconds */
                constructionTime = 1f; //5f;

                return (constructionMass, constructionTime);
            }
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() +
                    $"1 Create() {ex.Message} Stack: \n{ex.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// Change recipe value constructionMass and constructionTime 
        /// for the construction
        /// </summary>
        /// <returns>float[] constructionMass, float constructionTime</returns>
        public (float[] constructionMass, float constructionTime) Replace()
        {
            try
            {
                /* 5 kg */
                constructionMass = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER_TINY;

                /* 3 seconds */
                constructionTime = 1f; //TUNING.BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER0;

                return (constructionMass, constructionTime);
            }
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() +
                    $"2 Replace() {ex.Message} Stack: \n{ex.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// Change recipe value for Destroy Item
        /// </summary>
        /// <param name="deconstructable"></param>
        public void Destroy(Deconstructable deconstructable)
        {
            try
            {
                /* 3 seconds */
                constructionTime = 1f; // TUNING.BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER0;

                deconstructable.SetWorkTime(constructionTime);
                deconstructable.Trigger((int)GameHashes.MarkForDeconstruct, null);
                deconstructable.Trigger((int)GameHashes.RefreshUserMenu, null);
            }
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() +
                   $"3 Destroy() {ex.Message} Stack: \n{ex.StackTrace}");
            }
        }
    }
}