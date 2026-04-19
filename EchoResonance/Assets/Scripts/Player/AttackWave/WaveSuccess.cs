using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSuccess : Singleton<WaveSuccess>
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
