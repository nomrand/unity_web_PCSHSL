using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour {
    public string folderName = "040xxyy";
    public string sceneName = "SampleScene 1";

    private string scenePath;

    // Start is called before the first frame update
    void Start () {
        scenePath = folderName + "/" + sceneName;

        GetComponentInChildren<Text> ().text = folderName;
    }

    // Update is called once per frame
    void Update () {

    }

    public void loadScene () {
        SManager.addTargetScene = scenePath;
        SceneManager.LoadScene ("AddScene");
    }
}