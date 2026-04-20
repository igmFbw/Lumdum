using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBuff : Singleton<DashBuff>
{
    public void Play(Transform pos)
    {
        transform.position = pos.position;
        gameObject.SetActive(true);
    }
    public void Stop()
    {
        gameObject.SetActive(false);
    }
    public void Destory()
    {
        gameObject.SetActive(false);
    }
}
