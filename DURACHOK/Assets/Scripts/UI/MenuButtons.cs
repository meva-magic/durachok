using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;


    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnStartButtonClick()
    {
        UIManager.instance.DisableMainMenu();
    }

    private void OnExitButtonClick()
    {}
}
