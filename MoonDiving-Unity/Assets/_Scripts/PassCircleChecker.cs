using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// チェッカー用Gameobjectに張り付けて利用します。
/// </summary>
public class PassCircleChecker : MonoBehaviour {

    public ParticleSystem particle;
    public AudioClip sound;


    private GameObject gPlayerWithHMD;
    private void Start()
    {
        gPlayerWithHMD = GameObject.Find("PlayerWithHMD");
    }

    void OnTriggerEnter(Collider other)
    {
        //パーティクルアニメーション
        particle.Play();

        //サウンド再生
        GetComponent<AudioSource>().clip = sound;
        GetComponent<AudioSource>().Play();

        //得点加算
        gPlayerWithHMD.GetComponent<MoonDivingMain>().addScoreForChecker();
    }


}
