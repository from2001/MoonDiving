using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoonDivingMain : MonoBehaviour {

    [Header("スコア")]
    [SerializeField]
    private int score;

    [Header("ゴールのZ座標(-150)")]
    [SerializeField]
    private int GoalZ = -150;

    bool fadeFlag = false;
    float fadeSpeed = 0.15f;
    GameObject scorePanel;
    UnityEngine.PostProcessing.PostProcessingBehaviour ppb;

    void Reset()
    {
        //スコアパネルを非表示に
        fadeFlag = false;
        var c = scorePanel.GetComponent<Image>().color;
        scorePanel.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0);
        GameObject.Find("ScoreText").GetComponent<Text>().text = "";
        scorePanel.SetActive(false);

        //Post Processing Stack
        Camera.main.gameObject.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>().enabled = true;
    }

    // Use this for initialization
    void Start () {

        scorePanel = GameObject.Find("ScorePanel");
        ppb = Camera.main.gameObject.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>();

        //初期リセット
        Reset();

    }
	
	// Update is called once per frame
	void Update () {


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
            //Camera.main.gameObject.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>().enabled = false;
            //ppb.enabled = false;
            ppb.profile = null;
            GameObject.Find("ScoreText").GetComponent<Text>().text = "Your Score is " + score;
            fadeFlag = true;
        }


        //Fade処理
        var c = scorePanel.GetComponent<Image>().color;
        var alpha = c.a + fadeSpeed * Time.deltaTime;
        scorePanel.GetComponent<Image>().color = new Color(c.r, c.g, c.b, alpha);

    }



}
