using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSwitchUI : Singleton<AttackSwitchUI>
{
    public RectTransform rectTransform;
    public GameObject attackSwitchPanel;
    public AttackSwitchUI attackSwitchUI;
    public ArcLayoutGroup arcLayoutGroup;
    public Vector2 offset = new Vector2(0, 0);

    public float totalAngle = 140;
    public float fadeDuration = 0.7f;
    private bool isShow = false;

    void Start()
    {
        Hide();
    }
    public void SetOffset(Vector2 offset)
    {
        this.offset = offset;
    }
    public void SetPosition(Vector2 position)
    {
        rectTransform.position = position + offset;
    }

    public void Hide()
    {
        arcLayoutGroup.SetTotalAngle(0);
        attackSwitchPanel.SetActive(false);
        isShow = false;
    }
    public void Show()
    {
        attackSwitchPanel.SetActive(true);
        arcLayoutGroup.SetTotalAngle(0);
        StartCoroutine(FadeAngle(arcLayoutGroup.GetTotalAngle(), totalAngle, fadeDuration));
        isShow = true;
    }
    public bool IsShow()
    {
        return isShow;
    }
    private IEnumerator FadeAngle(float start, float target, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            t = Mathf.SmoothStep(0, 1, t);

            float angle = Mathf.Lerp(start, target, t);
            arcLayoutGroup.SetTotalAngle(angle);

            yield return null;
        }

        arcLayoutGroup.SetTotalAngle(target);
    }

}
