using HarmonyLib;
using KMod;

namespace Door_Overhaul
{
    public class Mod : UserMod2
    {
        private readonly ManagementError err =
            new("# DoorOverhaul > ", "Mod.cs > ");
        public override void OnLoad(Harmony harmony)
        {
            try
            {
                base.OnLoad(harmony);
                // harmony.PatchAll();
            }
            catch (Exception ex)
            {
                Debug.LogError(err.GetMessageAndCode() +
                    $"1 OnLoad(): Exception - {ex.Message}  Stack: \n{ex.StackTrace}");
            }
        }
    }
}