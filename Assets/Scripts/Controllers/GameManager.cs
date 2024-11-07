using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour, IWaveObserver
{
    // Singleton instance of GameManager
    private static GameManager instance;

    // Property to get the instance of GameManager (Singleton pattern)
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

    // Static flag to determine if the game is over
    public static bool GameIsOver;

    // References to UI elements for game over and level complete screens
    public GameObject gameOverUI;
    public GameObject completeLevelUI;

    // Current state of the game (State pattern)
    private IGameState currentState;

    // References to various managers in the game
    WaveSpawner waveSpawner;
    BuildManager buildManager;
    GameManager gameManager;
    PauseMenu pauseMenu;

    // GameFacade instance for handling references
    public static GameFacade gameFacade;

    // Register event for scene loaded
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Unregister event for scene loaded
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Initialization of GameManager
    void Start()
    {
        // Initialize GameFacade if it is not already set
        if (gameFacade == null)
        {
            gameFacade = new GameFacade();
        }

        // Set the initial state to MenuState
        SetState(new MenuState(this));
    }

    // Called when a new scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindReferences();

        // Enable the CameraController if it exists in the scene
        CameraController cameraController = FindObjectOfType<CameraController>();
        if (cameraController == null)
        {
            Debug.LogError("CameraController not found in the scene. Make sure there is a camera with the script attached.");
        }
        else
        {
            cameraController.enabled = true;
        }

        // Register GameManager as an observer of WaveSpawner
        if (waveSpawner != null)
        {
            waveSpawner.RegisterObserver(this);
        }
    }

    // Find and assign references to various game objects in the scene
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

    // Update is called once per frame
    void Update()
    {
        // Update the current game state if it exists
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }

    // Set the current state of the game (State pattern)
    public void SetState(IGameState newState)
    {
        // Exit the current state if it exists
        if (currentState != null)
        {
            currentState.ExitState();
        }

        // Set the new state
        currentState = newState;

        // Enter the new state
        if (currentState != null)
        {
            currentState.EnterState();
        }
    }

    // Called when a wave is completed
    public void OnWaveComplete()
    {
        Debug.Log("Wave completed!");

        // If all waves are completed, the player wins the level
        if (waveSpawner != null && waveSpawner.waves.Length == PlayerStats.Rounds)
        {
            WinLevel();
        }
    }

    // Called when an enemy is spawned
    public void OnEnemySpawned()
    {
        Debug.Log("Enemy spawned!");
    }

    // Start the game and set the state to PlayingState
    public static void StartGame()
    {
        Instance.SetState(new PlayingState(Instance));
    }

    // End the game and set the state to GameOverState
    public void EndGame()
    {
        // Ensure the game over state is not already set
        if (GameIsOver) return;

        Debug.Log("End Game - Game Over!");

        SetState(new GameOverState(this));
        GameIsOver = true;

        // Show the game over UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

    // Win the level and set the state to WinState
    public void WinLevel()
    {
        // Ensure the game over state is not already set
        if (GameIsOver) return;

        Debug.Log("Win Level - Level completed!");

        SetState(new WinState(this));
        GameIsOver = true;

        // Show the level complete UI
        if (completeLevelUI != null)
        {
            completeLevelUI.SetActive(true);
        }
    }
}
