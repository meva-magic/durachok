using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathButtons : MonoBehaviour
{
    public Button restartButton;
    public Button menuButton;


    void Start()
    {
        restartButton.onClick.AddListener(OnRestartButtonClick);
        menuButton.onClick.AddListener(OnMenuButtonClick);
    }

    private void OnRestartButtonClick()
    {
        UIManager.instance.RestartScene();
    }

    private void OnMenuButtonClick()
    {
        UIManager.instance.EnableMainMenu();
        UIManager.instance.DisableDeathMenu();
    }
}
