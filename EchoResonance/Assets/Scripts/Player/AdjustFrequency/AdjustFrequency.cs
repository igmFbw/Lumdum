using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustFrequency : MonoBehaviour
{
    public Slider frequencySlider;
    public GameObject judgeObject;
    [SerializeField]private float swingSpeed = 1f;
    private bool isSwinging = false;

    public void SetSliderValue(float value)
    {
        frequencySlider.value = value;
    }
    public void AddSliderValue(float value)
    {
        frequencySlider.value += value;
        frequencySlider.value = Mathf.Clamp(frequencySlider.value, 0, 1);
    }
    public float GetSliderValue()
    {
        return frequencySlider.value;
    }
    public void Show()
    {
        frequencySlider.gameObject.SetActive(true);
    }
    public void Hide()
    {
        frequencySlider.gameObject.SetActive(false);
        StopSwing();
    }
    public bool IsSwinging()
    {
        return isSwinging;
    }
    public void StartSwing()
    {
        if (!isSwinging)
        {
            isSwinging = true;
            judgeObject.SetActive(true);
            StartCoroutine(SliderSwing());
        }
    }

    public void StopSwing()
    {
        isSwinging = false;
        judgeObject.SetActive(false);
        StopCoroutine(SliderSwing());
    }

    // 来回摆动的协程（0 ↔ 1 平滑循环）
    private IEnumerator SliderSwing()
    {
        while (isSwinging)
        {
            // 从 0 平滑到 1
            yield return SmoothSlider(0f, 1f, swingSpeed);
            // 从 1 平滑回到 0
            yield return SmoothSlider(1f, 0f, swingSpeed);
        }
    }

    // 平滑移动滑块
    private IEnumerator SmoothSlider(float from, float to, float speed)
    {
        float time = 0;
        while (time < 1f && isSwinging)
        {
            time += Time.deltaTime * speed;
            // 平滑曲线
            float t = Mathf.SmoothStep(0, 1, time);
            frequencySlider.value = Mathf.Lerp(from, to, t);
            yield return null;
        }
    }
}
