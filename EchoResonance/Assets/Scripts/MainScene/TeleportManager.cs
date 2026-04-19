using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public void Fade(float fadeDur)
    {        
        StartCoroutine(FadeScreenCoroutine(fadeDur));
    }

    private IEnumerator FadeScreen(float targetAlpha)
    {
        isFade = true;
        fadeScreen.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeScreen.alpha - targetAlpha) / fadeDuration;
        while(!Mathf.Approximately(fadeScreen.alpha, targetAlpha))
        {
            fadeScreen.alpha = Mathf.MoveTowards(fadeScreen.alpha, targetAlpha, speed*Time.fixedDeltaTime);
            yield return null;
        }
        fadeScreen.alpha = targetAlpha;
        fadeScreen.blocksRaycasts = false;
        isFade = false;
    }
    private IEnumerator FadeScreen2(float targetAlpha, float fadeDur)
    {
        isFade = true;
        fadeScreen.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeScreen.alpha - targetAlpha) / fadeDur;
        while(!Mathf.Approximately(fadeScreen.alpha, targetAlpha))
        {
            fadeScreen.alpha = Mathf.MoveTowards(fadeScreen.alpha, targetAlpha, speed*Time.fixedDeltaTime);
            yield return null;
        }
        fadeScreen.alpha = targetAlpha;
        fadeScreen.blocksRaycasts = false;
        isFade = false;
    }
    private IEnumerator FadeScreenCoroutine(float fadeDur)
    {
        yield return FadeScreen2(1, fadeDur/5);
        
        yield return FadeScreen2(0, fadeDur);
    }
}
