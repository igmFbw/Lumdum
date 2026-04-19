using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charging : MonoBehaviour
{    
    public Image circle;
    public void SetValue(float value)
    {
        circle.fillAmount = value;
    }

}
