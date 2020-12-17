using UnityEngine;

public class TetrisManager : MonoBehaviour
{
    [Header("掉落時間"),Range(0.1f,3)]
    public float speed = 1.5f;
    [Header("目前分數")]
    public int score;
    [Header("最高分數")]
    public int highscore;
    [Header("等級")]
    public int level = 1;
    [Header("結束畫面")]
    public GameObject objFinal;
    [Header("方塊掉落音效")]
    public AudioClip sounddown;
    [Header("方塊移動與旋轉音效")]
    public AudioClip soundmove;
    [Header("方塊消除音效")]
    public AudioClip soundclip;
    [Header("遊戲結束音效")]
    public AudioClip soundgameover;
    [Header("下一個俄羅斯方塊")]
    public Transform traNextArea;
    [Header("畫布")]
    public Transform tranConver;
    /// <summary>
    /// 生成方塊
    /// </summary>
    public int indexNext;
    private void Start()
    {
        Randomtetris();
       
    }
    private void Randomtetris()
    {
      indexNext=Random.Range(0, 7);
      traNextArea.GetChild(indexNext).gameObject.SetActive(true);    
    }
    public void Startgame()
    {
        GameObject tetris = traNextArea.GetChild(indexNext).gameObject;
        GameObject current= Instantiate(tetris,tranConver);

        current.GetComponent<RectTransform>().anchoredPosition = new Vector2(-40, 360);

        tetris.SetActive(false);
        
        Randomtetris();

    }
    

   
    /// <summary>
    /// 添加分數
    /// </summary>
    /// <param name="score"></param>
    public void addscore(int score)
    {
        score += 10;
        print("累加後的數字" + score);

    }

    /// <summary>
    /// 遊戲時間
    /// </summary>
    private void gametime()
    {
    }

    /// <summary>
    /// 遊戲結束
    /// </summary>
    private void gameover()
    {
    }

    /// <summary>
    /// 重新遊戲
    /// </summary>
    public void restart()
    {

    }

    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void exitgame()
    {
    }

    
}
