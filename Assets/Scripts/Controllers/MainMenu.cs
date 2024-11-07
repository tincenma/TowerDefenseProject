using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string levelToLoad = "LevelSelect";

	public SceneFader sceneFader;

	public void Play ()
	{
        if (sceneFader != null)
        {
            sceneFader.FadeTo(levelToLoad);
        }
        else
        {
            Debug.LogError("SceneFader is not assigned in the MainMenu script. Please assign it in the Inspector.");
        }
    }

	public void Quit ()
	{
		Debug.Log("Exciting...");
		Application.Quit();
	}

}
