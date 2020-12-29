using System.Collections;
using System.Collections.Generic;
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

    private float length;
   private float lengthdown;
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

        }
    }
    public bool wallRight;
    public bool wallLeft;
    public bool floor;

    private void Update()
    {
        CheckWall();
    }
    private void Start()
    {
        length = length0;
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
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -Vector3.up, lengthdown, 1 << 9);
        if (hit2 && hit2.transform.name == "地板")
        {
            floor = true;

        }
        
    }
}
