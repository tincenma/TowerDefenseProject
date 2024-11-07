using UnityEngine;

public class PausedState : IGameState
{
    private GameManager gameManager;

    public PausedState(GameManager manager)
    {
        gameManager = manager;
    }

    public void EnterState()
    {
        Debug.Log("Entered Paused State");

        Time.timeScale = 0f; // Pause the game by setting time scale to 0
    }

    public void UpdateState()
    {
        // You can add logic here if anything should happen while paused
    }

    public void ExitState()
    {
        Debug.Log("Exiting Paused State");

        Time.timeScale = 1f; // Resume the game by resetting time scale to 1
    }
}
