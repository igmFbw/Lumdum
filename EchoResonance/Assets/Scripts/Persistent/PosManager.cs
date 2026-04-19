using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosManager : Singleton<PosManager>
{
    public Vector2 oriPos;
    public Vector2 CrystalGreenPos;
    public void UpdatePos(Transform transf)
    {
        CrystalGreenPos = transf.position;
    }
    public void ReturnCrystalGreenPos(Transform transf)
    {
        TeleportManager.Instance.Fade(2);
        if(CrystalGreenPos != Vector2.zero)
        {
            transf.position = CrystalGreenPos;
        }
        else
        {
            transf.position = oriPos;
        }
    }
    public void ReturnOriPos(Transform transf)
    {
        TeleportManager.Instance.Fade(2);
        transf.position = oriPos;
    }
    public void ResetPos()
    {
        CrystalGreenPos = Vector2.zero;
    }

}
