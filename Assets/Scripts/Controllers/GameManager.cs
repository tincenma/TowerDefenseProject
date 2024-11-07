using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour, IWaveObserver
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    public static bool GameIsOver;

    public GameObject gameOverUI;
    public GameObject completeLevelUI;

    private IGameState currentState;

    WaveSpawner waveSpawner;
    BuildManager buildManager;
    GameManager gameManager;
    PauseMenu pauseMenu;

    // Add GameFacade property
    public static GameFacade gameFacade;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        // Initialize GameFacade
        if (gameFacade == null)
        {
            gameFacade = new GameFacade();
        }

        SetState(new MenuState(this));
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindReferences();

        CameraController cameraController = FindObjectOfType<CameraController>();
        if (cameraController == null)
        {
            Debug.LogError("CameraController not found in the scene. Make sure there is a camera with the script attached.");
        }
        else
        {
            cameraController.enabled = true;
        }

        // Register GameManager as an observer
        if (waveSpawner != null)
        {
            waveSpawner.RegisterObserver(this);
        }
    }

    public void FindReferences()
    {
        if (GameObject.Find("GameOver") != null)
        {
            gameOverUI = GameObject.Find("GameOver");
        }
        else
        {
            Debug.LogError("GameOver object not found in the scene. Make sure it exists.");
        }

        if (GameObject.Find("CompleteLevel") != null)
        {
            completeLevelUI = GameObject.Find("CompleteLevel");
        }
        else
        {
            Debug.LogError("CompleteLevel object not found in the scene. Make sure it exists.");
        }

        pauseMenu = FindObjectOfType<PauseMenu>();
        if (pauseMenu == null)
        {
            Debug.LogError("PauseMenu object not found in the scene. Make sure it exists.");
        }

        waveSpawner = FindObjectOfType<WaveSpawner>();
        if (waveSpawner != null)
        {
            waveSpawner.spawnPoint = GameObject.Find("START").transform;
            waveSpawner.waveCountdownText = GameObject.Find("Timer").GetComponent<Text>();
        }

        buildManager = FindObjectOfType<BuildManager>();
        if (buildManager != null)
        {
            buildManager.nodeUI = GameObject.Find("NodeUI").GetComponent<NodeUI>();
        }

        CameraController cameraController = FindObjectOfType<CameraController>();
        if (cameraController == null)
        {
            Debug.LogError("CameraController not found in the scene. Make sure it exists in the new level.");
        }
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }

    public void SetState(IGameState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.EnterState();
        }
    }

    public void OnWaveComplete()
    {
        Debug.Log("Wave completed!");

        if (waveSpawner != null && waveSpawner.waves.Length == PlayerStats.Rounds)
        {
            // If the player has completed all waves, they win the level
            WinLevel();
        }
    }


    public void OnEnemySpawned()
    {
        Debug.Log("Enemy spawned!");
    }

    public static void StartGame()
    {
        Instance.SetState(new PlayingState(Instance));
    }

    public void EndGame()
    {
        // ѕроверка, чтобы убедитьс€, что состо€ние "Game Over" уже не установлено
        if (GameIsOver) return;

        Debug.Log("End Game - Game Over!");

        SetState(new GameOverState(this));
        GameIsOver = true;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

    public void WinLevel()
    {
        // ѕроверка, чтобы убедитьс€, что состо€ние "Game Over" уже не установлено
        if (GameIsOver) return;

        Debug.Log("Win Level - Level completed!");

        SetState(new WinState(this)); // »спользуем новое состо€ние WinState
        GameIsOver = true;

        if (completeLevelUI != null)
        {
            completeLevelUI.SetActive(true);
        }
    }
}
