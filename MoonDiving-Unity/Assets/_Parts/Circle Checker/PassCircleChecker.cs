using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCircleChecker : MonoBehaviour {

    public ParticleSystem particle;
    public AudioClip sound;

    void OnTriggerEnter(Collider other)
    {
        particle.Play();
        GetComponent<AudioSource>().clip = sound;
        GetComponent<AudioSource>().Play();
    }


}
