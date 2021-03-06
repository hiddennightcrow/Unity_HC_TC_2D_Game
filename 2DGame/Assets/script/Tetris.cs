﻿
using UnityEngine;
using System.Linq;

public class Tetris : MonoBehaviour
{
    #region 欄位
    [Header("角度為零，線條的長度")]
    public float length0;
    [Header("角度為90，線條的長度")]
    public float length90;
    [Header("旋轉位移:左右")]
    public int offx;
    [Header("旋轉位移:上下")]
    public float offy;
    [Header("偵測是否能旋轉")]
    public float lengthRotate0r;
    public float lengthRotate0l;

    public float lengthRotate90r;
    public float lengthRotate90l;



    public bool wallRight;
    public bool wallLeft;
    public bool floor;
    public bool canRotate = true;

    private RectTransform rect;

    private float length;
    private float lengthdown;
    private float lengthRotateR;
    private float lengthRotateL;
    #endregion

    [Header("每一顆小方塊的射線長度"), Range(0f, 2f)]
    public float smallLength = 0.5f;

    #region 事件
    private void OnDrawGizmos()
    {
        
        #region 每一顆判定
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawRay(transform.GetChild(i).position, Vector2.down * smallLength);
        }
        for (int i = 0; i < transform.childCount; i++)

        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.GetChild(i).position, Vector2.right * smallLength);
            Gizmos.DrawRay(transform.GetChild(i).position, Vector2.left * smallLength);


        }

        #endregion

    }
   private void SettingLength()


    {
        #region 判定牆壁和地板
        int z = (int)transform.eulerAngles.z;

        if (z == 0 || z == 180)
        {
            length = length0;
           
            lengthdown = length90;
            

            lengthRotateR = lengthRotate0r;
            lengthRotateL = lengthRotate0l;
            




        }
        else if (z == 90 || z == 270)
        {
            length = length90;
           
            lengthdown = length0;
           
            lengthRotateR = lengthRotate90r;
            lengthRotateL = lengthRotate90l;
           


        }
        #endregion
    }
 private void Update()
    {
        SettingLength();
        CheckWall();
        CheckBottom();
        CheckLeftAndRight();
    }
    private void Start()
    {
        length = length0;

        rect = GetComponent<RectTransform>();
        //偵測有幾個子物件(小方塊)就新增幾個陣列
        smallRightAll = new bool[transform.childCount];
        smallLeftAll = new bool[transform.childCount];

        
    }
    #endregion
 
    public bool smallBottom;
    public bool smallRight;
    public bool smallLeft;


    //所有方塊右邊是否有其他方塊
    public bool[] smallRightAll;
    public bool[] smallLeftAll;

   
    private void CheckLeftAndRight()
    {
        //迴圈執行每一顆方塊
        for (int i = 0; i < transform.childCount; i++)
        {
            //每一顆小方塊 射線(每一顆小方塊的中心點,長度,圖層)
            RaycastHit2D hitR = Physics2D.Raycast(transform.GetChild(i).position, Vector3.right, smallLength, 1 << 10);
            if (hitR && hitR.collider.name =="方塊") smallRightAll[i] = true;
            else smallRightAll[i] = false;


            RaycastHit2D hitL = Physics2D.Raycast(transform.GetChild(i).position, Vector3.left, smallLength, 1 << 10);
            if (hitL && hitL.collider.name =="方塊") smallLeftAll[i] = true;
            else smallLeftAll[i] = false;
        }
        var allRight =  smallRightAll.Where(x => x == true);
        smallRight = allRight.ToArray().Length > 0;
        var allLeft = smallLeftAll.Where(x => x == true);
        smallLeft = allLeft.ToArray().Length > 0;
    }

    private void CheckBottom()
    {
        //迴圈執行每一顆方塊
        for (int i = 0; i < transform.childCount; i++)
        {
            //每一顆小方塊 射線(每一顆小方塊的中心點,長度,圖層)
            RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(i).position, Vector3.down, smallLength, 1 << 10);
            if(hit&&hit.collider.name =="方塊") smallBottom=true;

        }
    }
    #region 方法
    private void CheckWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right, length, 1 << 8);
        if (hit && hit.transform.name == "牆:右邊")
        {
            wallRight = true;

        }
        else
        {
            wallRight = false;
        }
        
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, -Vector3.right, length, 1 << 8);
        if (hit1 && hit1.transform.name == "牆:左邊")
        {
            wallLeft = true;

        }
        else
        {
            wallLeft= false;
        }
        RaycastHit2D hitRotateR = Physics2D.Raycast(transform.position, Vector3.right,  lengthRotateR, 1 << 8);
        RaycastHit2D hitRotateL = Physics2D.Raycast(transform.position, -Vector3.right, lengthRotateL, 1 << 8);
        if (hitRotateR && hitRotateR.transform.name == "牆:右邊"|| hitRotateL && hitRotateL.transform.name == "牆:左邊")
        {
            canRotate = false;

        }
        else
        {
            canRotate = true;
        }



        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -Vector3.up, lengthdown, 1 << 9);
        if (hit2 && hit2.transform.name == "地板")
        {
            floor = true;

        }
        
    }
    public void Offset()
    {
        int z = (int)transform.eulerAngles.z;
        if (z == 90 || z == 270)
        {
            rect.anchoredPosition -= new Vector2(offx, offy);
        }
        else if(z==0||z==180)
            {
           rect.anchoredPosition += new Vector2(offx, offy);
        }
    }
    #endregion

}
