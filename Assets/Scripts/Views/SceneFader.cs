using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image blackScreen;
    public float fadeDuration = 1f;

    private void Awake()
    {
        if (blackScreen == null)
        {
            blackScreen = GetComponentInChildren<Image>();
        }
    }

    public void FadeTo(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeOut(string sceneName)
    {

        // Fade to black
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.Lerp(0f, 1f, timer / fadeDuration));
            yield return null;
        }

        SceneManager.LoadScene(sceneName);

        // Wait until the scene has fully loaded
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        // Fade from black
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.Lerp(1f, 0f, timer / fadeDuration));
            yield return null;
        }
    }
}
