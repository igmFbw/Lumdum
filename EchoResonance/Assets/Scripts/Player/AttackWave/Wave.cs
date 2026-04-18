using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public PlayerWaveState waveState;
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

}
