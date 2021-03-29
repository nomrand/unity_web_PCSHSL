using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SController : MonoBehaviour {
    public GameObject button;
    public string[] dirs = {};

    public Text titleText;

    // Start is called before the first frame update
    void Start () {
        var clSet = new HashSet<string>();
        foreach (var name in dirs) {
            var b = Instantiate (button, transform);
            b.GetComponent<LoadButton> ().folderName = name;
            
            clSet.Add(name.Substring(name.Length-4, 2));
        }

        if(titleText){
            titleText.text += "(class" + String.Join(",", clSet) +")";
        }
    }

    public void EndGame () {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL ("");
#else
        Application.Quit ();
#endif
    }
}