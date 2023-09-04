using System;
using UnityEngine;

namespace AdofaiMod2
{public class GUIClass : MonoBehaviour
    {
        public string text = string.Empty;
        public void OnGUI()
        {
            GUI.Label(new Rect(0f, 0f, 100f, 100f), text);
            GUILayout.Label("Hello World!(GUL.Label)");
            
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontSize = 50;
            textStyle.alignment = TextAnchor.MiddleCenter;
            textStyle.normal.textColor = Color.red;
            textStyle.font = RDString.GetFontDataForLanguage(RDString.language).font;
            GUI.Label(new Rect(0,0,100,100), "Hello World!(GUI.Label)", textStyle);

            GUIClass test = new GameObject().AddComponent<GUIClass>();
            UnityEngine.Object.DontDestroyOnLoad(test);
            
            if(GUILayout.Button("Hello World!(GUL.Button)"))
            {
                text = "Hello World!(GUL.Button)";
                Console.WriteLine(text);
            }
            
            float val = 0f;
            
            float newVal = GUILayout.HorizontalSlider(val, 0f, 100f);
            if (newVal != val)
            {
                val = newVal;
                text = "Slider: " + val;
            }
        }
    }
}