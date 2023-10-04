using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace AdofaiMod2
{
    public class ClsTextClass : MonoBehaviour
    {
        public static string host = "localhost";
        public static string port = "5000";
        
        UnityWebRequest www;
        public static void ImageLoad()
        {
            Image image = new GameObject().AddComponent<Image>();
            image.rectTransform.SetParent(GameObject.Find("Canvas").transform);
        }
        public static void TextUpdate(string cls, float fontSize = 50f)
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
                    clsText.alignment = TextAlignmentOptions.Center;
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

        public void Data()
        {
            StartCoroutine(TestGet());
        }

        private IEnumerator TestGet()
        {
            var www = UnityWebRequest.Get("http://" + host + ":" + port + "/test.txt");
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.uri.ToString());
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}