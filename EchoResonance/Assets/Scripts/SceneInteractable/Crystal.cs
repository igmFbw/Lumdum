using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public Animator anim;
    public CrystalType crystalType;

    public void ActivateSwitch(PlayerWaveState waveState)
    {
        Debug.Log("ActivateSwitch");
        if(EqualEnum(waveState, crystalType))
        {
            // 触发晶石特性
            Debug.Log("触发晶石特性");
            switch(crystalType)
            {
                case CrystalType.Red:
                    // 触发红色晶石特性
                    anim.SetBool("Activate", true);
                    break;
                case CrystalType.Blue:
                    // 触发蓝色晶石特性
                    break;
                case CrystalType.Yellow:
                    // 触发黄色晶石特性
                    break;
                case CrystalType.Green:
                    // 触发绿色晶石特性
                    break;
                default:
                    break;
            }
        }else
        {
            // 没有触发晶石特性
            Debug.Log("没有触发晶石特性");
        }
    }
    private bool EqualEnum(PlayerWaveState waveState, CrystalType crystalType)
    {
        if(waveState==PlayerWaveState.RedWave && crystalType==CrystalType.Red)
        {
            return true;
        }else if(waveState==PlayerWaveState.BlueWave && crystalType==CrystalType.Blue)
        {
            return true;
        }
        else if(waveState==PlayerWaveState.YellowWave && crystalType==CrystalType.Yellow)
        {
            return true;
        }
        else if(waveState==PlayerWaveState.GreenWave && crystalType==CrystalType.Green)
        {
            return true;
        }
        return false;
    }
    public void RedCrystalSet()
    {        
        gameObject.layer = LayerMask.NameToLayer("Ground");
    }
}
