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
        AudioManager.instance.Play("MenuMusic");
    }

    public void PlayLevelMusic()
    {
        AudioManager.instance.Stop("MenuMusic");
        AudioManager.instance.Play("LevelMusic");
    }

    public void PlayMenuMusic()
    {
        AudioManager.instance.Stop("LevelMusic");
        AudioManager.instance.Play("MenuMusic");
    }
}
