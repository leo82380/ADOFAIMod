using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityModManagerNet;
using TMPro;
using UnityEngine.UI;

namespace AdofaiMod2
{
    public class Main
    {
        public static UnityModManager.ModEntry.ModLogger Logger;
        public static Harmony Harmony;
        public static bool IsEnabled = false;

        public static Setting Setting;
        
        public static void Setup(UnityModManager.ModEntry modEntry)
        {
            Logger = modEntry.Logger;
            modEntry.OnToggle = OnToggle;
            Setting = new Setting();
            Setting = UnityModManager.ModSettings.Load<Setting>(modEntry);
        }
        
        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            IsEnabled = value;

            if (value)
            {
                modEntry.OnGUI = OnGUI;
                modEntry.OnSaveGUI = OnSaveGUI;
                Harmony = new Harmony(modEntry.Info.Id);
                Harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            else
            {
                Harmony.UnpatchAll(modEntry.Info.Id);
            }

            return true;
        }

        private static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (ADOBase.isCLSLevel)
            {
                Text text = new GameObject().AddComponent<Text>();
                text.text = ADOBase.currentLevel;
                text.font = RDString.GetFontDataForLanguage(RDString.language).font;
                Object.DontDestroyOnLoad(text);
            }
        }
        
        private static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            Setting.Save(modEntry);
        }
    }
}