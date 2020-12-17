using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIstatic : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        print(Mathf.Rad2Deg);
        print(Mathf.Epsilon);

        print("攝影機總數:"+Camera.allCamerasCount);
        Cursor.visible = false;

        print("隨機範圍1-100:"+Random.Range(1 , 100));

        //Application.OpenURL("https://www.google.com/");

        print("10.11去小數點後:"+ Mathf.Floor(10.11f));
    }
    private void Update()
    {
        //print("是否按任意鍵:"+Input.anyKey);
        //print("時間:"+Time.time);
        print("是否有按下空白鍵:" + Input.GetKeyDown("space"));
    }
}

   
