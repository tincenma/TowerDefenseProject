using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;

    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    void Awake()
    {
        if (ui == null)
        {
            ui = GameObject.Find("PauseMenu");
            if (ui == null)
            {
                Debug.LogError("PauseMenu UI object is missing. Please assign it in the Inspector or check the Hierarchy.");
                return;
            }
        }

        if (sceneFader == null)
        {
            sceneFader = FindObjectOfType<SceneFader>();
            if (sceneFader == null)
            {
                Debug.LogError("SceneFader object is missing. Please add it to the scene.");
            }
        }
    }

    void Start()
    {
        if (ui == null)
        {
            ui = GameObject.Find("PauseMenu");
        }

        if (sceneFader == null)
        {
            sceneFader = FindObjectOfType<SceneFader>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (ui == null)
            {
                Debug.LogError("UI object is missing in PauseMenu. Reassign it in the inspector.");
                return;
            }
            Toggle();
        }
    }

    public void Toggle()
    {
        if (ui == null)
        {
            Debug.LogError("UI object is missing in PauseMenu. Reassign it in the inspector.");
            return;
        }

        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        Toggle();
        GameManager.StartGame(); // Use the class name directly to call the static method
        if (sceneFader != null)
        {
            sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.LogError("SceneFader is not assigned. Make sure it is set in the Inspector.");
        }
        WaveSpawner waveSpawner = FindObjectOfType<WaveSpawner>();
        if (waveSpawner != null)
        {
            waveSpawner.ResetWaveState();
        }
    }

    public void Menu()
    {
        Toggle();
        if (sceneFader != null)
        {
            sceneFader.FadeTo(menuSceneName);
        }
        else
        {
            Debug.LogError("SceneFader is not assigned. Make sure it is set in the Inspector.");
        }
    }
}
