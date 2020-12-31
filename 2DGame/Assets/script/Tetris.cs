
using UnityEngine;

public class Tetris : MonoBehaviour
{
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

    private void OnDrawGizmos()
    {
        
        int z = (int)transform.eulerAngles.z;

        if (z == 0 || z == 180)
        {
            length = length0;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.right * length0);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, -Vector3.right * length0);

            lengthdown = length90;
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, -Vector3.up * length90);

            lengthRotateR = lengthRotate0r;
            lengthRotateL = lengthRotate0l;
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, Vector3.right * lengthRotate0r);
            Gizmos.DrawRay(transform.position, -Vector3.right * lengthRotate0l);




        }
        else if (z == 90 || z == 270)
        {
            length = length90;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.right * length90);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, -Vector3.right * length90);

            lengthdown = length0;
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, -Vector3.up * length0);

            lengthRotateR = lengthRotate90r;
            lengthRotateL = lengthRotate90l;
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, Vector3.right * lengthRotate90r);
            Gizmos.DrawRay(transform.position, -Vector3.right * lengthRotate90l);


        }
    }
    

    private void Update()
    {
        CheckWall();
       
    }
    private void Start()
    {
        length = length0;

        rect = GetComponent<RectTransform>();
    }
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
            canRotate = true;

        }
        else
        {
            canRotate = false;
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
    
}
