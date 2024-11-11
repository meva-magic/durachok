using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject deathMenu;
    public GameObject winMenu;
    public GameObject pauseMenu;

    public static UIManager instance;

    private void Awake()
    {
         if (instance == null)
        {
            instance = this; // Set the instance to this object
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed on scene load
        }
        else
        {
            Destroy(gameObject); // If instance already exists, destroy this duplicate
        }
    }

    private void EnsureEventSystem()
    {
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>(); // Add the required input module
        }
    }
    
    private void Start()
    {
        //EnableMainMenu();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!mainMenu.activeSelf || !deathMenu.activeSelf || !winMenu.activeSelf)
            {EnablePauseMenu();}
        }
    }

    
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);

        SceneMusic.instance.PlayMenuMusic();
    }

    public void DisableMainMenu()
    {
        mainMenu.SetActive(false);

        SceneMusic.instance.PlayLevelMusic();
    }

    public void EnabmeDeathMenu()
    {
        deathMenu.SetActive(true);

        SceneMusic.instance.PlayMenuMusic();
    }

    public void DisableDeathMenu()
    {
        deathMenu.SetActive(false);

        SceneMusic.instance.PlayLevelMusic();
    }

    public void EnablePauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;

        SceneMusic.instance.PlayMenuMusic();
    }

    public void DisablePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

        SceneMusic.instance.PlayLevelMusic();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
