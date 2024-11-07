using UnityEngine;

public class GameOverState : IGameState
{
    private GameManager gameManager;

    public GameOverState(GameManager manager)
    {
        gameManager = manager;
    }

    public void EnterState()
    {
        Debug.Log("Entered Game Over State");
        gameManager.gameOverUI.SetActive(true);
    }

    public void UpdateState()
    {
        // Game over logic here
    }

    public void ExitState()
    {
        Debug.Log("Exiting Game Over State");
    }
}
