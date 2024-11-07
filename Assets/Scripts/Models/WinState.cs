using UnityEngine;

public class WinState : IGameState
{
    private GameManager gameManager;

    public WinState(GameManager manager)
    {
        gameManager = manager;
    }

    public void EnterState()
    {
        Debug.Log("Entered Win State");
        if (gameManager.completeLevelUI != null)
        {
            gameManager.completeLevelUI.SetActive(true);
        }
    }

    public void UpdateState()
    {
        // Logic for the win state
    }

    public void ExitState()
    {
        Debug.Log("Exiting Win State");
        if (gameManager.completeLevelUI != null)
        {
            gameManager.completeLevelUI.SetActive(false);
        }
    }
}
