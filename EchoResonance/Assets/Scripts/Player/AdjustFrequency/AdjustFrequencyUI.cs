using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustFrequencyUI : Singleton<AdjustFrequencyUI>
{
    public RectTransform rectTransform;
    public AdjustFrequency adjustFrequency;
    [SerializeField] private bool isShow = false;
    [SerializeField] private Vector2 offset = new Vector2(0, 0);
    
    private void Start()
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
    public void AddSliderValue(float value)
    {
        adjustFrequency.AddSliderValue(value);
    }
    public float GetSliderValue()
    {
        return adjustFrequency.GetSliderValue();
    }
    public void Show()
    {
        adjustFrequency.Show();
        adjustFrequency.SetSliderValue(.5f);
        isShow = true;
    }
    public void Hide()
    {
        adjustFrequency.Hide();
        isShow = false;
    }
    public bool IsShow()
    {
        return isShow;
    }

}
