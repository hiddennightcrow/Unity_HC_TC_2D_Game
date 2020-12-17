using UnityEngine;

public class APInostatic : MonoBehaviour
{
    public Transform traA;
    public Transform traB;

    public GameObject myObject;
    public Transform mytra;

    private void Start()
    {
        print("物件A座標:" + traA.position);
        traB.position = new Vector3(0, 1, 2);
        print("物件圖層:" + myObject.layer);
        myObject.layer = 4;
    }
    private void Update()
    {
        mytra.Rotate(0, 0, 3);
        mytra.Translate(1, 0, 0);

    }
    
}
