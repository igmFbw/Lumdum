using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 必须加，用于控制UI图片透明度

public class GamingManager : MonoBehaviour
{
    public GameObject player;
    public GameWin gameWin;

    public GameObject gameWinPanel;
    public Image WinCG; // 改为Image，方便控制透明度
    public GameObject gameOverPanel;

    public GameObject pausePanel;
    private bool isPaused = false;

    public GameObject tipPanel;
    public Button tipCloseButton;

    public GameObject heartPanel;
    public GameObject mapPanel;
    

    // 协程只执行一次标记
    private bool hasPlayedWin = false;

    private void Awake()
    {
        if (Camera.main != null && player != null)
        {
            Camera.main.GetComponent<CameraFollow>().target = player.transform;
            Camera.main.transform.GetChild(0).gameObject.SetActive(true);
        }
        
        // 初始隐藏胜利CG
        if(WinCG != null)
        {
            WinCG.gameObject.SetActive(false);
            WinCG.color = new Color(WinCG.color.r, WinCG.color.g, WinCG.color.b, 0);
        }

        if(tipCloseButton != null)
        {
            tipCloseButton.onClick.AddListener(() =>
            {
                HideTipPanel();
            });
        }
        if(tipPanel != null)
        {
            tipPanel.SetActive(false);
        }
        ShowTipPanel();
    }

    private void Update()
    {
        // 胜利逻辑（只执行一次）
        if (gameWin.isWin && !hasPlayedWin)
        {
            hasPlayedWin = true;
            heartPanel.SetActive(false);
            mapPanel.SetActive(false);
            StartCoroutine(WinCoroutine());
        }

        // 暂停逻辑
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
            pausePanel.SetActive(isPaused);
        }

        // 死亡逻辑
        if (player == null) return;
        if (player.GetComponent<PlayerController>().GetIsDeath())
        {
            gameOverPanel.SetActive(true);
        }
    }

    // 补全：淡入 → 停留 → 淡出 → 显示胜利面板
    IEnumerator WinCoroutine()
    {
        // 1. 显示CG并开始淡入
        WinCG.gameObject.SetActive(true);
        float fadeDuration = 0.8f; // 淡入淡出速度
        float timer = 0;

        // 淡入
        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime; // 不受timeScale影响
            float alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            WinCG.color = new Color(WinCG.color.r, WinCG.color.g, WinCG.color.b, alpha);
            yield return null;
        }

        // 停留1秒
        yield return new WaitForSecondsRealtime(1f);

        // 淡出
        // timer = 0;
        // while (timer < fadeDuration)
        // {
        //     timer += Time.unscaledDeltaTime;
        //     float alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
        //     WinCG.color = new Color(WinCG.color.r, WinCG.color.g, WinCG.color.b, alpha);
        //     yield return null;
        // }

        // 隐藏CG，显示胜利界面
        //WinCG.gameObject.SetActive(false);
        gameWinPanel.SetActive(true);
    }
    public void ShowTipPanel()
    {
        tipPanel.SetActive(true);
    }
    public void HideTipPanel()
    {
        tipPanel.SetActive(false);
    }
}