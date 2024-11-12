using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor; // Include for Editor-specific code
#endif

public class MenuButtons : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;

    void Awake()
    {
        // Find all EventSystems in the scene
        EventSystem[] eventSystems = FindObjectsOfType<EventSystem>();

        // If there's more than one, destroy any extra ones
        if (eventSystems.Length > 1)
        {
            for (int i = 1; i < eventSystems.Length; i++)
            {
                Destroy(eventSystems[i].gameObject);
            }
        }
    }

    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnStartButtonClick()
    {
        UIManager.instance.DisableMainMenu();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnExitButtonClick()
    {
        // Quit the game in a build
        Application.Quit();

        // If running in the Unity Editor, stop playing the scene
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
