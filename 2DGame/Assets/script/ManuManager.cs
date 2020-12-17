using UnityEngine.SceneManagement;
using UnityEngine;

public class ManuManager : MonoBehaviour
{
    
    public void Delaystartgame()
    {
       
        Invoke("startgame", 1.5f);
    }
    public void Delayexitgame()
    {

        Invoke("exitgame", 1.5f);
    }

   
    /// <summary>
    /// 開始遊戲
    /// </summary>
    private void startgame()
    {
        SceneManager.LoadScene("遊戲場景");
    }


    /// <summary>
    /// 離開遊戲
    /// </summary>
    private void exitgame()
    {
        Application.Quit();
    }
    
}
