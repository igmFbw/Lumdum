using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustFrequency : MonoBehaviour
{
    public Slider frequencySlider;
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
    }
}
