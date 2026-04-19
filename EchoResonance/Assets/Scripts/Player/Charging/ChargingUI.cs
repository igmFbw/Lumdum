using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingUI : Singleton<ChargingUI>
{
    public GameObject chargingPanel;
    public RectTransform rectTransform;
    public Vector2 offset;
    public Charging charging;
    public void SetPosition(Vector2 position)
    {
        rectTransform.position = position + offset;
    }
    public void SetValue(float value)
    {
        charging.SetValue(value);
    }
    public void Show()
    {
        chargingPanel.SetActive(true);
    } 
    public void Hide()
    {
        chargingPanel.SetActive(false);
    }
}
