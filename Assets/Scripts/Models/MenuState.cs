using UnityEngine;

public class MenuState : IGameState
{
    private GameManager gameManager;

    public MenuState(GameManager manager)
    {
        gameManager = manager;
    }

    public void EnterState()
    {
        Debug.Log("Entered Menu State");
        // Ensure game over and level complete UIs are hidden
        if (gameManager.gameOverUI != null)
        {
            gameManager.gameOverUI.SetActive(false);
        }

        if (gameManager.completeLevelUI != null)
        {
            gameManager.completeLevelUI.SetActive(false);
        }

        Time.timeScale = 1f; // Ensure time is running when in menu
    }

    public void UpdateState()
    {
        // You can add code here to manage the main menu if needed
    }

    public void ExitState()
    {
        Debug.Log("Exiting Menu State");
    }
}
