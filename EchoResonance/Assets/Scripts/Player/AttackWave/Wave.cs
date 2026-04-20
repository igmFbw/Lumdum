using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public PlayerWaveState waveState;
    public float lifeTime;
    void OnEnable()
    {
        StartCoroutine(LifeTime());
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        switch(collider.tag)
        {
            case "Ground":
                // wave 消散
                PlayerController.Instance.PlayCrystalSound_2();
                break;
            case "Crystal":
                Crystal crystal = collider.GetComponent<Crystal>();
                crystal.ActivateSwitch(waveState);
                if(crystal.GetComponent<Crystal>().crystalType == CrystalType.Yellow)
                {
                    crystal.SetIsLink(true);
                }
                break;
            default:
                break;
        }
    }
    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy();
    }
    public void Destroy()
    {
        gameObject.SetActive(false);
    }

}
