using System.Reflection;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityModManagerNet;

namespace AdofaiMod2
{
    public class Main
    {
        public static UnityModManager.ModEntry.ModLogger Logger;
        public static Harmony Harmony;
        public static bool IsEnabled;

        public static Setting Setting;
        public static ClsTextClass ClsTextClass;
        
        public static void Setup(UnityModManager.ModEntry modEntry)
        {
            Logger = modEntry.Logger;
            modEntry.OnToggle = OnToggle;
            Setting = new Setting();
            Setting = UnityModManager.ModSettings.Load<Setting>(modEntry);
            ClsTextClass = new ClsTextClass();
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
                //OnToggle 안에 있어야지 안튕김
            }
            else
            {
                Harmony.UnpatchAll(modEntry.Info.Id);
            }

            return true;
        }
        
        private static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if(GUILayout.Button("TextUpdate"))
            {
                if (ADOBase.isScnGame)
                {
                    ClsTextClass.TextUpdate("Incls");
                }
                else if(ADOBase.isLevelEditor)
                {
                    ClsTextClass.TextUpdate("InclsEditor");
                }
                else
                {
                    ClsTextClass.TextUpdate("NoCls", 10f);
                }
            }

            if (GUILayout.Button("얼불춤 튕기게 하기"))
            {
                while (true)
                {
                    var a = new GameObject();
                }
            }
            GUILayout.Label("조심! 얼불 크래시");

            if (GUILayout.Button("서버"))
            {
                ClsTextClass.Data();
            }

            GUILayout.Label("서버 연결?");
        }
        
        private static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            Setting.Save(modEntry);
        }
    }
}