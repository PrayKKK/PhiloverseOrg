using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio : MonoBehaviour
{
    public string SoundScene;
    AudioSource BackGroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        BackGroundMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        SoundScene = SceneManager.GetActiveScene().name;
        BackGroundMusic.clip = Resources.Load("Audio/BackGroundMusic" + SoundScene) as AudioClip;
        if (SoundScene != null)
        {
          //  BackGroundMusic.Play;
        }
    }
}
