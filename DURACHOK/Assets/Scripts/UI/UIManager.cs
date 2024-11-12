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
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!mainMenu.activeSelf && !deathMenu.activeSelf && !winMenu.activeSelf)
            {
                EnablePauseMenu();
            }
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
        PlayerController.instance.isMovementAllowed = false;  // Блокировка движения
         Debug.Log("Movement disabled");
        SceneMusic.instance.PlayMenuMusic();
    }

    public void DisableMainMenu()
    {
        mainMenu.SetActive(false);
        PlayerController.instance.isMovementAllowed = true;   // Разблокировка движения
        SceneMusic.instance.PlayLevelMusic();
    }

    public void EnabmeDeathMenu()
    {
        deathMenu.SetActive(true);
        PlayerController.instance.isMovementAllowed = false;
        AudioManager.instance.Play("LooseSound");
        SceneMusic.instance.PlayMenuMusic();
    }

    public void DisabmeDeathMenu()
    {
        deathMenu.SetActive(false);
        PlayerController.instance.isMovementAllowed = true;
        SceneMusic.instance.PlayLevelMusic();
    }

    public void EnableWinMenu()
    {
        winMenu.SetActive(true);
        PlayerController.instance.isMovementAllowed = false;
        AudioManager.instance.Play("WinSound");
        SceneMusic.instance.PlayMenuMusic();
    }

    public void DisableWinMenu()
    {
        winMenu.SetActive(false);
        PlayerController.instance.isMovementAllowed = true;
        SceneMusic.instance.PlayLevelMusic();
    }

    public void EnablePauseMenu()
    {
        pauseMenu.SetActive(true);
        PlayerController.instance.isMovementAllowed = false;
        Time.timeScale = 0;
        SceneMusic.instance.PlayMenuMusic();
    }

    public void DisablePauseMenu()
    {
        pauseMenu.SetActive(false);
        PlayerController.instance.isMovementAllowed = true;
        Time.timeScale = 1;
        SceneMusic.instance.PlayLevelMusic();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
