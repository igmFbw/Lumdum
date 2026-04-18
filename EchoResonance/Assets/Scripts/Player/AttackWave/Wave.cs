using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public PlayerWaveState waveState;
    public float LifeTime{get;set;}
    void OnEnable()
    {
        StartCoroutine(LifrTime());
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        switch(collider.tag)
        {
            case "Ground":
                // wave 消散
                break;
            case "Crystal":
                Crystal crystal = collider.GetComponent<Crystal>();
                crystal.ActivateSwitch(waveState);
                break;
            default:
                break;
        }
    }
    private IEnumerator LifrTime()
    {
        while(LifeTime > 0)
        {
            LifeTime -= Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }

}
