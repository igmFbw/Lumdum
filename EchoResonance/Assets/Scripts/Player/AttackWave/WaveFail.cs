using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFail : Singleton<WaveFail>
{
    public void Play(Transform pos)
    {
        transform.position = pos.position;
        gameObject.SetActive(true);
    }
    public void Destory()
    {
        gameObject.SetActive(false);
    }
}
