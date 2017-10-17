using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip music1;
    public AudioClip music2;
    public AudioClip music3;
    public AudioClip music4;
    public bool musicIsActive = true;

    public AudioSource BGM;
    public static SoundManager instance = null;

    
	void Awake ()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

       
    }
    public void StartMusic()
    {
        RandomMusic(music1, music2, music3, music4);
    }
    void RandomMusic(params AudioClip[] audio)
    {
        int randomIndex = Random.Range(0, audio.Length);
        BGM.clip = audio[randomIndex];
        BGM.Play();
    }
}
