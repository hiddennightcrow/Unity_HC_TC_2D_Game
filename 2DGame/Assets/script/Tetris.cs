using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris : MonoBehaviour
{
    [Header("角度為零，線條的長度")]
    public float length0;
    [Header("角度為90，線條的長度")]
    public float length90;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        int z = (int)transform.eulerAngles.z;

        if (z == 0||z==180)
        {
            length = length0;
            Gizmos.DrawRay(transform.position, Vector3.right * length0);
        }
        else if (z == 90||z==270)
        {
            length = length90;
            Gizmos.DrawRay(transform.position, Vector3.right * length90);
        }
    }
    

}
