using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;

public class TetrisManager : MonoBehaviour
{
    [Header("掉落時間"), Range(0.1f, 3)]
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
    [Header("生成俄羅斯方塊的父物件")]
    public Transform traTetrisParent;
    [Header("生成的起始位置")]
    public Vector2[] posSpawn =
        {
        new Vector2(0,320),
        new Vector2(0,340),
        new Vector2(20,340),
        new Vector2(0,360),
        new Vector2(0,360),
        new Vector2(-20,340),
        new Vector2(20,340)
   };


    /// <summary>
    /// 生成方塊
    /// </summary>
    public int indexNext;
    public RectTransform currentTeris;
    public float timer;

    private void Start()
    {
        Randomtetris();


    }
    private void Randomtetris()
    {

        indexNext = Random.Range(0, 7);

        traNextArea.GetChild(indexNext).gameObject.SetActive(true);

    }
    public void Startgame()
    {
        fastDown = false;

        GameObject tetris = traNextArea.GetChild(indexNext).gameObject;
        GameObject current = Instantiate(tetris, traTetrisParent);

        current.GetComponent<RectTransform>().anchoredPosition = posSpawn[indexNext];

        tetris.SetActive(false);

        Randomtetris();

        currentTeris = current.GetComponent<RectTransform>();

    }
    private bool fastDown;
    private void Fastdown()
    {
        if (currentTeris && !fastDown)
        {
            if (currentTeris)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    fastDown = true;
                    speed = 0.018f;
                    
                }

            }
        }
    }

    [Header("分數判定區域")]
    public Transform traScoreArea;
    public RectTransform[] rectSmall;
    private void CheckTetris()
    {
        rectSmall = new RectTransform[traScoreArea.childCount];
        for (int i = 0; i < traScoreArea.childCount; i++)
        {
            rectSmall[i] = traScoreArea.GetChild(i).GetComponent<RectTransform>();
        }

        var small = rectSmall.Where(x => x.anchoredPosition.y == -300);
        if (small.ToArray().Length == 16)
        {
            
        }
    }
    private IEnumerable Shine(RectTransform[] smalls)
    {
        for (int i = 0; i < 16; i++)
        {
            float interval = 0.05f;
            smalls[i].GetComponent<Image>().enabled = false;
            yield return new WaitForSeconds(interval);
            smalls[i].GetComponent<Image>().enabled = true;
            yield return new WaitForSeconds(interval);
            smalls[i].GetComponent<Image>().enabled = false;
            yield return new WaitForSeconds(interval);
            smalls[i].GetComponent<Image>().enabled = true;
            yield return new WaitForSeconds(interval);
        }
    }
        private void Update()
    {
        ControlTertis();
        
        Fastdown();
    }
    private void ControlTertis()
    {
        if (currentTeris)
        {
            timer += Time.deltaTime;
            if (timer >= speed)
            {
                timer = 0;
                currentTeris.anchoredPosition -= new Vector2(0, 40);
            }




            #region 方塊左右、旋轉、加速
            Tetris tetris = currentTeris.GetComponent<Tetris>();
            if (!tetris.wallRight && !tetris.smallRight)
            {

                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    currentTeris.anchoredPosition += new Vector2(40, 0);
                }

            }

            if (!tetris.wallLeft && !tetris.smallLeft)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    currentTeris.anchoredPosition -= new Vector2(40, 0);
                }
            }
            if (tetris.canRotate)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentTeris.eulerAngles += new Vector3(0, 0, 90);

                    tetris.Offset();
                }
            }

            if (!fastDown)
            {
                if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                speed = 0.2f;
            }

            else
            {
                speed = 1.5f;
            }
            
                
            }

            #endregion
            if (tetris.floor||tetris.smallBottom)
            {
                SetGround();
                CheckTetris();
                Startgame();
                StartCoroutine(ShakeEffect());
            }
        }
    }
    private void SetGround()
    {
        int count = currentTeris.childCount;//取得 目前 方塊 的子物件數量
        for (int x = 0; x < count; x++)//迴圈 執行 子物件數量次數
        {
            currentTeris.GetChild(x).name = "方塊";//名稱改為方塊
            currentTeris.GetChild(x).gameObject.layer = 10;//圖層改為方塊
        }
        for (int i = 0; i < currentTeris.childCount; i++) currentTeris.GetChild(0).SetParent(traScoreArea);
        Destroy(currentTeris.gameObject);
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

    private IEnumerator ShakeEffect()
    {


        RectTransform rect = traTetrisParent.GetComponent<RectTransform>();


        float interval = 0.05f;
        rect.anchoredPosition += Vector2.up * 30;
        yield return new WaitForSeconds(interval);
        rect.anchoredPosition = Vector2.zero;
        yield return new WaitForSeconds(interval);
        rect.anchoredPosition += Vector2.up * 20;
        yield return new WaitForSeconds(interval);
        rect.anchoredPosition = Vector2.zero;
        yield return new WaitForSeconds(interval);
    }

}
