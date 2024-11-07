using UnityEngine;

public class PlayingState : IGameState
{
    private GameManager gameManager;

    public PlayingState(GameManager manager)
    {
        gameManager = manager;
    }

    public void EnterState()
    {
        Debug.Log("Entered Playing State");
        GameManager.GameIsOver = false;
    }

    public void UpdateState()
    {
        if (PlayerStats.Lives <= 0)
        {
            gameManager.SetState(new GameOverState(gameManager));
        }
    }

    public void ExitState()
    {
        Debug.Log("Exiting Playing State");
    }
}
