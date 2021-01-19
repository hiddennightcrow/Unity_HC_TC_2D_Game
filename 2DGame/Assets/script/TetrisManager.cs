using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;

public class TetrisManager : MonoBehaviour
{
    #region 欄位
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
    [Header("方塊移動音效")]
    public AudioClip soundmove;
    [Header("方塊旋轉音效")]
    public AudioClip soundturn;
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

    private bool fastDown;

    private bool gameOver; 
    #endregion 
    /// <summary>
    /// 生成方塊
    /// </summary>
    public int indexNext;
    public RectTransform currentTeris;
    public float timer;

    private AudioSource aud;


    private void Update()
    {
        if (gameOver) return;

        ControlTertis();
        
        Fastdown();
    }
    private void Start()
    {
        aud = GetComponent<AudioSource>();
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


    public bool[] destoryRow = new bool[17];
    public float[] downHeight;

    private IEnumerator CheckTetris()
    {
        rectSmall = new RectTransform[traScoreArea.childCount];
        for (int i = 0; i < traScoreArea.childCount; i++)
        {
            rectSmall[i] = traScoreArea.GetChild(i).GetComponent<RectTransform>();
            float y = rectSmall[i].anchoredPosition.y;
            if (y >= 320 - 10 && y <= 320 + 10) gameover();
        }

        int row = 17;
        for (int i = 0; i < row; i++)
        {

            float bottom = -320;
            float step = 40;
            var small = rectSmall.Where(x => x.anchoredPosition.y >= bottom + step * i - 10 && x.anchoredPosition.y <= bottom + step * i + 10);
            //消除
            if (small.ToArray().Length == 16)
            {
               yield return StartCoroutine(Shine(small.ToArray()));
                destoryRow[i] = true;
                addscore(1000);
                aud.PlayOneShot(soundclip, Random.Range(0.8f, 1.2f));
            }
        }
        downHeight = new float[traScoreArea.childCount];
        for (int i = 0; i < downHeight.Length; i++) downHeight[i] = 0;

        for (int i = 0; i < destoryRow.Length; i++)
        {
            if (!destoryRow[i]) continue;
            for (int j = 0; j < rectSmall.Length; j++)
            {
                if (rectSmall[j].anchoredPosition.y > -300 + 40 * i - 10)
                {
                    downHeight[j] -= 40;
                }
            }
            destoryRow[i] = false;
        }
        for (int i = 0; i < rectSmall.Length; i++)
        {
            rectSmall[i].anchoredPosition += Vector2.up * downHeight[i];
        }
    }
    private IEnumerator Shine(RectTransform[] smalls)
    {
        
            float interval = 0.05f;
            for (int i = 0; i < 16; i++) smalls[i].GetComponent<Image>().enabled = false;
            yield return new WaitForSeconds(interval);
        for (int i = 0; i < 16; i++) smalls[i].GetComponent<Image>().enabled = true;
            yield return new WaitForSeconds(interval);
        for (int i = 0; i < 16; i++) smalls[i].GetComponent<Image>().enabled = false;
            yield return new WaitForSeconds(interval);
        for (int i = 0; i < 16; i++) smalls[i].GetComponent<Image>().enabled = true;


        yield return new WaitForSeconds(interval);
        for (int i = 0; i < 16; i++) Destroy(smalls[i].gameObject);

        yield return new WaitForSeconds(interval);
        rectSmall = new RectTransform[traScoreArea.childCount];
        for (int i = 0; i < traScoreArea.childCount; i++)
        {
            rectSmall[i] = traScoreArea.GetChild(i).GetComponent<RectTransform>();
        }



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
                aud.PlayOneShot(soundmove, Random.Range(0.8f, 1.2f));
                }

            }

            if (!tetris.wallLeft && !tetris.smallLeft)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    
                    currentTeris.anchoredPosition -= new Vector2(40, 0);
                    aud.PlayOneShot(soundmove, Random.Range(0.8f, 1.2f));
                }
            }
            if (tetris.canRotate)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    
                    currentTeris.eulerAngles += new Vector3(0, 0, 90);
                    aud.PlayOneShot(soundturn, Random.Range(0.8f, 1.2f));

                    tetris.Offset();
                }
            }

            if (!fastDown)
            {
                if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                speed = 0.2f;
                    aud.PlayOneShot(soundmove, Random.Range(0.8f, 1.2f));

                }

                else
            {
                speed = timeFllMax;
            }
            
                
            }

            #endregion
            if (tetris.floor||tetris.smallBottom)
            {
                SetGround();
                StartCoroutine(CheckTetris());
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
        for (int i = 0; i < count; i++) currentTeris.GetChild(0).SetParent(traScoreArea);
        Destroy(currentTeris.gameObject);
    }

    [Header("分數文字")]
    public Text textScore;
    [Header("等級文字")]
    public Text textLv;

    private float timeFllMax = 1.5f;
    /// <summary>
    /// 添加分數
    /// </summary>
    /// <param name="add"></param>
    public void addscore(int add)
    {
        score += add;
        textScore.text = "分數:" + score;
        level = 1 + score / 1000;
        textLv.text = "等級:" + level;

        timeFllMax = 1.5f - level / 2;
        timeFllMax = Mathf.Clamp(timeFllMax, 0.1f, 99f);
        speed = timeFllMax;

    }

    /// <summary>
    /// 遊戲時間
    /// </summary>
    private void gametime()
    {
    }

    [Header("目前分數")]
    public Text textCurrent;
    [Header("最高分數")]
    public Text textHeigth;

    /// <summary>
    /// 遊戲結束
    /// </summary>
    private void gameover()
    {
        if (!gameOver)
        {
            aud.PlayOneShot(soundgameover, Random.Range(0.8f, 1.2f));
            gameOver = true;
            StopAllCoroutines();
            objFinal.SetActive(true);

            textCurrent.text = "目前分數:" + score;
            if (score >= PlayerPrefs.GetInt("最高分數"))
            {
                PlayerPrefs.SetInt("最高分數", score);
                textHeigth.text = "最高分數" + score;
            }
            else textHeigth.text = "最高分數:" + PlayerPrefs.GetInt("最高分數");
        

        }
    }

    /// <summary>
    /// 重新遊戲
    /// </summary>
    public void restart()
    {
        SceneManager.LoadScene("遊戲場景");
    }

    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void exitgame()
    {
        Application.Quit();

    }


    private IEnumerator ShakeEffect()
    {


        RectTransform rect = traTetrisParent.GetComponent<RectTransform>();
        aud.PlayOneShot(sounddown, Random.Range(0.8f, 1.2f));


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
