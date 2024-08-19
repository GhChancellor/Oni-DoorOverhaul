using System;
using System.IO;
using System.Reflection;
using static Localization;

namespace Door_Overhaul
{
    internal class TextLocalization
    {
        public static void Translate(Type root)
        {
            /* Basic intended way to register strings, keeps namespace */
            RegisterForTranslation(root);

            /* Load user created translation files */
            string loadTranslateFile = LoadTranslateFile();

            /* Register strings without namespace because we already loaded user translations, 
               custom languages will overwrite these */
            LocString.CreateLocStringKeys(root, null);

            /* Creates template for users to edit */
            GenerateStringsTemplate(root, loadTranslateFile);
        }

        /// <summary>
        /// Loads and returns the translation file based on the current locale.
        /// </summary>
        /// <returns>Path to the loaded translation file.</returns>
        private static string LoadTranslateFile()
        {
            /* Figure out where the mod loaded from 
             * Example: ....\Documents\Klei\OxygenNotIncluded\mods\Dev\DoorOverhaul 
             */
            string modTranslationsDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            /* Add "translations" to path
             * Example: ....\Documents\Klei\OxygenNotIncluded\mods\Dev\DoorOverhaul\translations
             */
            string translationFilePath = Path.Combine(modTranslationsDirectory, "translations");

            /* Pick up game localization settings and match to mod translation files
             * ( it_IT.po )
             */
            string file = Path.Combine(translationFilePath, GetLocale()?.Code + ".po");

            if (File.Exists(file))
            {
                OverloadStrings(LoadStringsFile(file, false));
            }           

            return translationFilePath;
        }
    }
}
