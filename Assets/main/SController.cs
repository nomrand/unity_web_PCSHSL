using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SController : MonoBehaviour {
    public GameObject button;
    private string[] dirs = {
        "040401",
        "040404",
        "040408",
        "040409",
        "040410",
        "040411",
        "040412",
        "040413",
        "040414",
        "040415",
        "040416",
        "040417",
        "040418",
        "040419",
        "040420",
        "040421",
        "040423",
        "040424",
    };

    // Start is called before the first frame update
    void Start () {
        foreach (var name in dirs) {
            var b = Instantiate (button, transform);
            b.GetComponent<LoadButton> ().folderName = name;
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