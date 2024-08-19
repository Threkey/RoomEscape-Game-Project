using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    AudioSource audiosource;
    public AudioClip tutorialBGM;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.clip = tutorialBGM;
        audiosource.loop = true;
        audiosource.playOnAwake = true;
        audiosource.Play();


    }
}
