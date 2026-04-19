using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManager : Singleton<TeleportManager>
{
    public string sceneName;
    public CanvasGroup fadeScreen;
    public float fadeDuration;
    private bool isFade;
    void Start()
    {
        //Teleport(string.Empty,sceneName);
    }
    public void Teleport(string teleportFrom, string teleportTo)
    {
        StartCoroutine(TeleportCoroutine(teleportFrom, teleportTo));
    }
    public IEnumerator TeleportCoroutine(string teleportFrom, string teleportTo)
    {
        yield return FadeScreen(1);
        if (teleportFrom != string.Empty)
        {                       
            yield return SceneManager.UnloadSceneAsync(teleportFrom);
        }
        yield return SceneManager.LoadSceneAsync(teleportTo, LoadSceneMode.Additive);

        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
        yield return FadeScreen(0);
    }
    private IEnumerator FadeScreen(float targetAlpha)
    {
        isFade = true;
        fadeScreen.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeScreen.alpha - targetAlpha) / fadeDuration;
        while(!Mathf.Approximately(fadeScreen.alpha, targetAlpha))
        {
            fadeScreen.alpha = Mathf.MoveTowards(fadeScreen.alpha, targetAlpha, speed*Time.deltaTime);
            yield return null;
        }
        fadeScreen.blocksRaycasts = false;
        isFade = false;
    }
}
