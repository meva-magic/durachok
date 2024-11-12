using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    public static SceneMusic instance;

    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        PlayMenuMusic();
    }

    public void PlayLevelMusic()
    {
        AudioManager.instance.Stop("MenuMusic");
        AudioManager.instance.Play("LevelMusic");
        AudioManager.instance.Play("Embient");
    }

    public void PlayMenuMusic()
    {
        AudioManager.instance.Stop("LevelMusic");
        AudioManager.instance.Play("MenuMusic");
    }
}
