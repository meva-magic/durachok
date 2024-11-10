using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject gameOver;
    public GameObject mainMenu;
    void Awake ()
	{
		instance = this;
	}
    

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }


    public void EnableMenu()
    {
        mainMenu.SetActive(true);
    }


    public void EnabmeGameOver()
    {
        gameOver.SetActive(true);
    }

    private void OnEnable()
    {
        //PlayerHealth.OnPlayerDeath += EnableGameOver;
    }



    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }
}
