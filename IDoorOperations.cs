
namespace Door_Overhaul
{
    internal interface IDoorOperations
    {
        /// <summary>
        /// Change recipe value constructionMass and constructionTime 
        /// for the construction
        /// </summary>
        /// <returns>float[] constructionMass, float constructionTime</returns>
        (float[] constructionMass, float constructionTime) Create();

        /// <summary>
        /// Change recipe value constructionMass and constructionTime 
        /// for the construction
        /// </summary>
        /// <returns>float[] constructionMass, float constructionTime</returns>
        (float[] constructionMass, float constructionTime) Replace();

        /// <summary>
        /// Destroy Item
        /// </summary>
        /// <param name="deconstructable"></param>
        void Destroy(Deconstructable deconstructable);
    }
}
