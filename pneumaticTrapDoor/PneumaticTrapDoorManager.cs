namespace Door_Overhaul
{
    internal class PneumaticTrapDoorManager : IDoorOperations
    {
        private float[] constructionMass;
        private float constructionTime;

        /// <summary>
        /// Change recipe value constructionMass and constructionTime 
        /// for the construction
        /// </summary>
        /// <returns>float[] constructionMass, float constructionTime</returns>
        public (float[] constructionMass, float constructionTime) Create()
        {
            /* 50 kg */
            constructionMass = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;

            /* 5 seconds */
            constructionTime = 5f;

            return (constructionMass, constructionTime);
        }

        /// <summary>
        /// Change recipe value constructionMass and constructionTime 
        /// for the construction
        /// </summary>
        /// <returns>float[] constructionMass, float constructionTime</returns>
        public (float[] constructionMass, float constructionTime) Replace()
        {
            /* 5 kg */
            constructionMass = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER_TINY;

            /* 3 seconds */
            constructionTime = 1f; //TUNING.BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER0;

            return (constructionMass, constructionTime);
        }

        /// <summary>
        /// Change recipe value for Destroy Item
        /// </summary>
        /// <param name="deconstructable"></param>
        public void Destroy(Deconstructable deconstructable)
        {
            /* 3 seconds */
            constructionTime = 1f; // TUNING.BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER0;

            deconstructable.SetWorkTime(constructionTime);
            deconstructable.Trigger( (int) GameHashes.MarkForDeconstruct, null);
            deconstructable.Trigger( (int) GameHashes.RefreshUserMenu, null);
        }
    }
}