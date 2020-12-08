using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [Header("尺寸"),Range(100,150)]
    public int size = 100;
    [Header("重量"),Tooltip("車子的重量")]
    public float weight = 1.5f;
    [Header("品牌"),Tooltip("車子的品牌")]
    public string brand = "BMW";
    [Header("是否有天窗"),Tooltip("車子的天窗")]
    public bool haswindow = true;

    [Header("顏色")]
    public Color colorA = Color.blue;
    public Color colorB = Color.green;
    public Color colorC = new Color(1, 0, 0);
    public Color colorD = new Color(1, 0, 0, 0.5f);

    [Header("二維向量")]
    public Vector2 vecA = Vector2.zero;
    public Vector2 vecB = Vector2.one;
    public Vector2 vecC = new Vector2(0, 0);

    public AudioClip soundA;
    public Sprite picA;
    public GameObject trfA;
    public GameObject camA;

    public GameObject goA;
    public GameObject goB;

   
    private void Start()
    {
        print("哈囉，媽的發克??");
        print(size);
        print(brand);

        weight = 1.3f;
        haswindow = false;
    }
}
