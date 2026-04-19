using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCanvas : Singleton<HealthCanvas>
{
    public List<GameObject> healthIcons;
    public int currentHealth;
    public Sprite image_1;
    public Sprite image_2;
    void Update()
    {
        switch (currentHealth)
        {
            case 3:
                healthIcons[0].GetComponent<Image>().sprite = image_1;
                healthIcons[1].GetComponent<Image>().sprite = image_1;
                healthIcons[2].GetComponent<Image>().sprite = image_1;
                break;
            case 2:
                healthIcons[0].GetComponent<Image>().sprite = image_1;
                healthIcons[1].GetComponent<Image>().sprite = image_1;
                healthIcons[2].GetComponent<Image>().sprite = image_2;
                break;
            case 1:
                healthIcons[0].GetComponent<Image>().sprite = image_1;
                healthIcons[1].GetComponent<Image>().sprite = image_2;  
                healthIcons[2].GetComponent<Image>().sprite = image_2;
                break;
            case 0:
                healthIcons[0].GetComponent<Image>().sprite = image_2;
                healthIcons[1].GetComponent<Image>().sprite = image_2;  
                healthIcons[2].GetComponent<Image>().sprite = image_2;
                break;
            default:
                break;
        }
    }
    public void SetCurrentHealth(int currentHealth)
    {
        this.currentHealth = currentHealth;
    }
}
