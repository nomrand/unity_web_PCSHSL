using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SManager : MonoBehaviour {
    public static string addTargetScene;
    public static string[] playerNames = {
        "MaleFreeSimpleMovement1",
    };

    public static GameObject target;
    public float jumpForce = 10f;
    public float moveForce = 5f;
    AsyncOperation loadingOperation;

    float h = 0;
    float v = 0;
    
    float time = 0;
    
    // Start is called before the first frame update
    void Start () {
        if (addTargetScene != null && addTargetScene.Length > 0) {
            loadingOperation = SceneManager.LoadSceneAsync (addTargetScene, LoadSceneMode.Additive);
        }
    }

    void Update () {
        if (loadingOperation != null && loadingOperation.isDone) {
            foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType (typeof (GameObject))) {
                if (obj.activeInHierarchy) {
                    // GameObjectの名前を表示.
                    foreach (var name in playerNames) {
                        if (name == obj.name) {
                            target = obj;
                            break;
                        }
                    }
                }
                if (target) {
                    break;
                }
            }
            foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType (typeof (GameObject))) {
                // シーン上に存在するオブジェクトならば処理.
                if (obj.activeInHierarchy) {
                    var scr = obj.GetComponent<SatoshiScript> ();
                    if (scr) {
                        if (!target) {
                            target = obj;
                        }
                        scr.cameraFollowObject = target.gameObject;
                        break;
                    }
                }
            }

            loadingOperation = null;
        }
    }

    public void goBack () {
        SceneManager.LoadScene (0);
    }

    public void playerJump () {
        if (target) {
            var r = target.GetComponentInChildren<Rigidbody>();
            if(r){
                r.AddForce(Vector3.up * jumpForce * r.mass, ForceMode.Impulse);
            }
        }
    }

    public void playerMoveUp (bool isUp) {
        if (isUp) {
            playerMove (isUp: true);
        } else {
            playerMove (isDown: true);
        }
    }
    public void playerMoveLeft (bool isLeft) {
        if (isLeft) {
            playerMove (isLeft: true);
        } else {
            playerMove (isRight: true);
        }

    }

    public void playerMove (bool isUp = false, bool isDown = false, bool isLeft = false, bool isRight = false) {
        if (target) {
            var r = target.GetComponentInChildren<Rigidbody>();
            if(r){
                if(isUp){
                    v += 1f;
                }
                if(isDown){
                    v -= 1f;
                }
                if(isLeft){
                    h += 1f;
                }
                if(isRight){
                    h -= 1f;
                }

                var scr = target.GetComponent<SimpleSampleCharacterControl>();
                if(scr){
                    time = 0;
                } else {
                    r.AddForce(new Vector3(v, 0, h) * moveForce, ForceMode.Impulse);
                }
            }
        }
    }

    void FixedUpdate(){
        time += Time.deltaTime;
        if (target != null && time < 0.5f) {
            var scr = target.GetComponent<SimpleSampleCharacterControl>();
            if(scr != null){
                scr.DirectUpdatePublic(h, v);
            }
        }
    }
}