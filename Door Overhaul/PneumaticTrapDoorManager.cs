using System;

namespace Door_Overhaul
{
    internal class PneumaticTrapDoorManager : IDoorOperations
    {
        private float[] constructionMass;
        private float constructionTime;

        /// <summary>
        /// Create Pneumatic Trap Door
        /// </summary>
        /// <returns>float[] constructionMass, float constructionTime</returns>
        public (float[], float) Create()
        {
            /* 50 kg */
            constructionMass = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;

            /* 5 seconds */
            constructionTime = 5f;

            return (constructionMass, constructionTime);
        }

        /// <summary>
        /// Destroy Pneumatic Trapdoor
        /// </summary>
        /// <param name="deconstructable"></param>
        public void Destroy(Deconstructable deconstructable)
        {
            /* 3 seconds */
            constructionTime = TUNING.BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER0;

            deconstructable.SetWorkTime(constructionTime);
            deconstructable.Trigger((int)GameHashes.MarkForDeconstruct, null);
            deconstructable.Trigger((int)GameHashes.RefreshUserMenu, null);
        }

        /// <summary>
        /// Replace Pneumatic Trap Door
        /// </summary>
        /// <returns>float[] constructionMass, float constructionTime</returns>
        public (float[], float) Replace()
        {
            /* 5 kg */
            constructionMass = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER_TINY;

            /* 3 seconds */
            constructionTime = 1; //TUNING.BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER0;

            return (constructionMass, constructionTime);
        }
    }
}