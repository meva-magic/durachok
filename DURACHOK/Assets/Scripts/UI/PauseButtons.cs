using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour
{
    public Button restartButton;
    public Button continueButton;
    public Button menuButton;


    void Start()
    {
        restartButton.onClick.AddListener(OnRestartButtonClick);
        continueButton.onClick.AddListener(OnContinueButtonClick);
        menuButton.onClick.AddListener(OnMenuButtonClick);
    }


    private void OnRestartButtonClick()
    {
        UIManager.instance.RestartScene();
        UIManager.instance.DisablePauseMenu();
        //UIManager.instance.EnableMainMenu();
    }

    private void OnContinueButtonClick()
    {
        UIManager.instance.DisablePauseMenu();
    }

    private void OnMenuButtonClick()
    {
        UIManager.instance.DisablePauseMenu();
        UIManager.instance.EnableMainMenu();
    }
}
