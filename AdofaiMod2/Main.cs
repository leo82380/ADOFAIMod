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

                //OnToggle 안에 있어야지 안튕김
                
            }
            else
            {
                Harmony.UnpatchAll(modEntry.Info.Id);
            }

            return true;
        }

        private static void JunPyoFiveNine(bool isFiveNine)
        {
            TextMeshProUGUI junpyo = null;
            if (isFiveNine)
            {
                junpyo = new GameObject().AddComponent<TextMeshProUGUI>();
                junpyo.rectTransform.SetParent(GameObject.Find("Canvas").transform);
                junpyo.rectTransform.anchoredPosition = Vector2.left;
                junpyo.gameObject.transform.localPosition = new Vector3(100f, 100f, 0f);
                junpyo.text = "준표는 59";
                junpyo.font = RDString.GetFontDataForLanguage(RDString.language).fontTMP;
                junpyo.overflowMode = TextOverflowModes.Overflow;
                junpyo.fontSize = 100;
                Object.DontDestroyOnLoad(junpyo);
            }
            else
            {
                Object.DestroyImmediate(junpyo);
            }
        }

        static void TextUpdate(string cls, float fontSize = 50f)
        {
            TextMeshProUGUI text = null;
            TextMeshProUGUI editorText = null;
            TextMeshProUGUI clsText = null;
            switch (cls){
                case "Incls":
                    clsText = new GameObject().AddComponent<TextMeshProUGUI>();
                    clsText.rectTransform.SetParent(GameObject.Find("Canvas").transform);
                    clsText.rectTransform.anchoredPosition = Vector2.left + Vector2.up;
                    clsText.text = scnGame.instance.levelData.song + " / " + scnGame.instance.levelData.artist;
                    clsText.font = RDString.GetFontDataForLanguage(RDString.language).fontTMP;
                    clsText.overflowMode = TextOverflowModes.Overflow;
                    clsText.fontSize = fontSize;
                    Object.DontDestroyOnLoad(clsText);
                    Debug.Log(scnGame.instance.levelData.song + " / " + scnGame.instance.levelData.artist);
                    
                    Object.DestroyImmediate(text);
                    Object.DestroyImmediate(editorText);
                break;
                case "NoCls":
                    text = new GameObject().AddComponent<TextMeshProUGUI>();
                    text.rectTransform.SetParent(GameObject.Find("Canvas").transform);
                    text.rectTransform.anchoredPosition = Vector2.left + Vector2.up;
                    text.text = "Not in game";
                    text.font = RDString.GetFontDataForLanguage(RDString.language).fontTMP;
                    
                    Object.DontDestroyOnLoad(text);
                    Object.DestroyImmediate(clsText);
                    Object.DestroyImmediate(editorText);
                break;
                case "InclsEditor":
                    editorText = new GameObject().AddComponent<TextMeshProUGUI>();
                    editorText.rectTransform.SetParent(GameObject.Find("Canvas").transform);
                    editorText.rectTransform.anchoredPosition = Vector2.left + Vector2.up;
                    editorText.text = scnEditor.instance.levelData.song + " / " + scnEditor.instance.levelData.artist;
                    editorText.font = RDString.GetFontDataForLanguage(RDString.language).fontTMP;
                    Object.DontDestroyOnLoad(editorText);
                    Debug.Log(scnEditor.instance.levelData.song + " / " + scnEditor.instance.levelData.artist);
                    
                    Object.DestroyImmediate(clsText);
                    Object.DestroyImmediate(text);
                break;
            }
        }

        private static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (GUILayout.Button("준표 59"))
            {
                var isFiveNine = false;
                if (isFiveNine)
                    isFiveNine = false;
                if(!isFiveNine)
                    isFiveNine = true;
                JunPyoFiveNine(isFiveNine);
            }
            if(GUILayout.Button("TextUpdate"))
            {
                if (ADOBase.isScnGame)
                {
                    TextUpdate("Incls");
                }
                else if(ADOBase.isLevelEditor)
                {
                    TextUpdate("InclsEditor");
                }
                else
                {
                    TextUpdate("NoCls", 10f);
                }
            }

            if (GUILayout.Button("얼불춤 튕기게 하기"))
            {
                while (true)
                {
                    var a = new GameObject();
                }
            }
        }
        
        private static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            Setting.Save(modEntry);
        }
    }
}