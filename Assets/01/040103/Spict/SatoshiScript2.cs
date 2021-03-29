using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatoshiScript2 : MonoBehaviour {
    // ตัวแปรส่วนกลาง (สามารถอ้างอิงได้จาก "Inspector Window")
    // jumpPower หมายถึงความสูงที่สามารถกระโดดได้
    public float jumpPower = 0.5F;
    // movePower หมายถึงความเร็วในการเคลื่อนที่
    public float movePower = 10;

    // Start is called before the first frame update
    void Start () {
        // Nothing
    }

    // ฟังก์ชันนี้จะถูกเรียก 50 ครั้งต่อวินาที
    void FixedUpdate () {
        // ได้รับค่าการเคลื่อนที่
        // เมื่อกด ปุ่มลูกศรซ้าย(left) = ได้รับค่า -1
        // เมื่อกด ปุ่มลูกศรขวา(right) = ได้รับค่า 1
        // เมื่อไม่กด ปุ่มลูกศรซ้ายและขวา = ได้รับค่า 0
        float h = Input.GetAxis ("Horizontal");
        // เมื่อกด ปุ่มลูกศรขึ้น(up) = ได้รับค่า -1
        // เมื่อกด ปุ่มลูกศรลง(down) = ได้รับค่า 1
        // เมื่อไม่กด ปุ่มลูกศรขึ้นและลง = ได้รับค่า 0
        float v = Input.GetAxis ("Vertical");
        // เพิ่มแรงแนวนอนให้กับ GameObject
        GetComponent<Rigidbody> ().AddForce (new Vector3 (h, 0, v) * movePower);

        // ตรวจสอบว่ากด ปุ่ม Enter(Spacebar) หรือไม่
        if (Input.GetButton ("Jump")) {
            // เมื่อกด ปุ่ม Enter(Spacebar) เพิ่มแรงแนวตั้งให้กับ GameObject
            GetComponent<Rigidbody> ().AddForce (new Vector3 (0, jumpPower, 0), ForceMode.Impulse);
        }

        // ทำให้กล้องเลื่อนไปตาม GameObject
        Transform careraTransform = Camera.main.GetComponent<Transform> ();
        careraTransform.LookAt (GetComponent<Transform> ());
        // (-10, 10, -10) หมายถึงระยะห่างของกล้องและ GameObject
        careraTransform.position = GetComponent<Transform> ().position + new Vector3 (-20, 20, -20);
    }

    // ฟังก์ชันนี้จะถูกเรียก เวลา GameObject โดนชนวัตถุอื่น
    void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.tag == "Finish") {
            // ถ้าวัตถุอื่นที่โดนชนมี tag ชื่อว่า Finish
            // เรียกใช้ฟังก์ชันด้านล่าง
            GameEnd ();
            // กำจัดวัตถุอื่นที่โดนช
            Destroy (collision.gameObject);
        }
    }

    // ฟังก์ชันนี้จะแสดงข้อความว่า "GAME CLEAR!" ในหน้าจอ
    // * เวลาต้องการแสดงข้อความในหน้าจอ ไม่แนะนำให้ใช้วิธีนี้
    // * วิธีที่ดีกว่าคือการสร้าง canvas ไว้ใน "Hierarchy Window" ล่วงหน้า 
    //   เช่น https://youtu.be/XEaEjZbt-xY
    private void GameEnd () {
        GameObject myGO;
        GameObject myText;
        Canvas myCanvas;
        Text text;
        RectTransform rectTransform;

        // Canvas
        myGO = new GameObject ();
        myGO.name = "TestCanvas";
        myGO.AddComponent<Canvas> ();

        myCanvas = myGO.GetComponent<Canvas> ();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        myGO.AddComponent<CanvasScaler> ();
        myGO.AddComponent<GraphicRaycaster> ();

        // Text
        myText = new GameObject ();
        myText.transform.parent = myGO.transform;
        myText.name = "wibble";

        text = myText.AddComponent<Text> ();
        text.font = Resources.GetBuiltinResource<Font> ("Arial.ttf");
        text.text = "GAME\nCLEAR!";
        text.fontSize = 100;
        text.color = Color.black;

        // Text position
        rectTransform = text.GetComponent<RectTransform> ();
        rectTransform.localPosition = new Vector3 (0, 0, 0);
        rectTransform.sizeDelta = new Vector2 (400, 400);
    }
}