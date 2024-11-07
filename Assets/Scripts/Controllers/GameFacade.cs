using UnityEngine;

public class GameFacade
{
    public WaveSpawner waveSpawner { get; private set; }
    public BuildManager buildManager { get; private set; }
    public GameManager gameManager { get; private set; }
    public PauseMenu pauseMenu { get; private set; }

    public void FindReferences()
    {
        waveSpawner = UnityEngine.Object.FindObjectOfType<WaveSpawner>();
        buildManager = UnityEngine.Object.FindObjectOfType<BuildManager>();
        gameManager = UnityEngine.Object.FindObjectOfType<GameManager>();
        pauseMenu = UnityEngine.Object.FindObjectOfType<PauseMenu>();
    }
}
