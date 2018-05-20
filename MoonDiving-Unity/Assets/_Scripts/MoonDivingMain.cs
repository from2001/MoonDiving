using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoonDivingMain : MonoBehaviour {

    [Header("Postprocessing Profile")]
    public UnityEngine.PostProcessing.PostProcessingProfile ppp;

    [Header("スコア")]
    [SerializeField]
    private int score;

    [Header("ゴールのZ座標(-150)")]
    [SerializeField]
    private int GoalZ = -150;


    

    bool fadeFlag = false;
    float fadeSpeed = 0.15f;
    GameObject scorePanel;
    private float HmdRotationDifferenceY=0f;

    //初期化
    void Init()
    {
        //スコアパネルを非表示に
        fadeFlag = false;
        var c = scorePanel.GetComponent<Image>().color;
        scorePanel.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0);
        GameObject.Find("ScoreText").GetComponent<Text>().text = "";
        scorePanel.SetActive(false);

        //Post Processing Stack
        Camera.main.gameObject.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>().profile = ppp;

        //位置をリセット
        transform.position = new Vector3(0, 0, 0);

        //速度を0に
        transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        //Player Controllerを無効に
        transform.GetComponent<MoonVR.PlayerController>().enabled = false;

        //Scoreを0に
        score = 0;

    }


    void HmdReset()
    {
        //HMDのリセット
        //高さのリセット
        var NewY = transform.position.y - Camera.main.transform.position.y;
        transform.position = new Vector3(transform.position.x, NewY, transform.position.z);

        ////回転のリセット
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180, transform.localEulerAngles.z);
        //var NewRotationY = transform.localEulerAngles.y - Camera.main.transform.localEulerAngles.y - HmdRotationDifferenceY;
        //HmdRotationDifferenceY = NewRotationY;
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, NewRotationY, transform.localEulerAngles.z);

    }

    void PlayStart()
    {
        ////Player Controllerを有効に
        transform.GetComponent<MoonVR.PlayerController>().enabled = true;

        //初期サウンド再生
        transform.GetComponents<AudioSource>()[0].Play();
    }





    // Use this for initialization
    void Start () {

        scorePanel = GameObject.Find("ScorePanel");

        //初期化
        Init();

    }
	
	// Update is called once per frame
	void Update () {


        //Enter keyで初期化、ゲームスタート
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (transform.GetComponent<MoonVR.PlayerController>().enabled)
            {
                Init();
            }
            else
            {
                PlayStart();
            }
        }

        //RでHMDポジションリセット
        if (Input.GetKeyDown(KeyCode.R))
        {
            HmdReset();
        }


        //150メートル(シャトルの位置)以上移動したらゴール
        if (transform.position.z < GoalZ)
        {
            goalFunction();
        }
	}

    //チェッカー通過した際に呼び出される関数
    public void addScoreForChecker()
    {
        score += 10;
    }


    public void goalFunction()
    {
        //ゴール得点計算して加算


        //フェードアウトして点数表示
        FadeOut();
    }


    void FadeOut()
    {
        //Fadeoutが始まるときに1回実行
        if (fadeFlag == false)
        {
            scorePanel.SetActive(true);
            Camera.main.gameObject.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>().profile = null;

            GameObject.Find("ScoreText").GetComponent<Text>().text = "Your Score is " + score;
            fadeFlag = true;
        }


        //Fade処理
        var c = scorePanel.GetComponent<Image>().color;
        var alpha = c.a + fadeSpeed * Time.deltaTime;
        scorePanel.GetComponent<Image>().color = new Color(c.r, c.g, c.b, alpha);

    }



}
