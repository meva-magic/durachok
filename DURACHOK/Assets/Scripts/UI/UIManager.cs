using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject mainMenu;
    public GameObject deathMenu;
    public GameObject pauseMenu;

    private void Awake ()
	{
		instance = this;
	}
    
    private void Start()
    {
        //LockCursor();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EnablePauseMenu();
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
    }

    public void DisableMainMenu()
    {
        mainMenu.SetActive(false);
    }

    public void EnabmeDeathMenu()
    {
        deathMenu.SetActive(true);
    }

    public void DisableDeathMenu()
    {
        deathMenu.SetActive(false);
    }

    public void EnablePauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void DisablePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
