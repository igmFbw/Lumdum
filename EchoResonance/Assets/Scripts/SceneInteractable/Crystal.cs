using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public Animator anim;
    public CrystalType crystalType;    
    [SerializeField]bool isActivated = false;
    [SerializeField] private int crystalYellowValue = 0;
    private bool isLink = false;

    void OnEnable()
    {
        EventHandler.OnCrystalYellowValueAdd += OnCrystalYellowValueChange;
        EventHandler.OnCrystalYellowValueReset += OnCrystalYellowValueReset;
    }
    void OnDisable()
    {
        EventHandler.OnCrystalYellowValueAdd -= OnCrystalYellowValueChange;
        EventHandler.OnCrystalYellowValueReset -= OnCrystalYellowValueReset;
    }
    void OnCrystalYellowValueChange()
    {
        if (crystalType == CrystalType.Yellow && isLink)
        {
            crystalYellowValue++;
            anim.SetInteger("Value", crystalYellowValue);
        }
    }
    void OnCrystalYellowValueReset()
    {
        if (crystalType == CrystalType.Yellow && isLink)
        {
            crystalYellowValue = 0;  
            anim.SetInteger("Value", crystalYellowValue);    
            isLink = false;
        }
    }

    public void ActivateSwitch(PlayerWaveState waveState)
    {
        if(isActivated)
        {
            return;
        }
        Debug.Log("ActivateSwitch");
        if(EqualEnum(waveState, crystalType))
        {
            // 触发晶石特性
            Debug.Log("触发晶石特性");
            switch(crystalType)
            {
                case CrystalType.Red:
                    // 触发红色晶石特性
                    PlayerController.Instance.PlayCrystalSound_1();
                    anim.SetBool("Activate", true);
                    RedCrystalSet();
                    WaveSuccess.Instance.Play(transform);
                    isActivated = true;
                    break;
                case CrystalType.Blue:
                    // 触发蓝色晶石特性
                    PlayerController.Instance.PlayCrystalSound_1();
                    anim.SetBool("Activate", true);
                    BlueCrystalSet();
                    WaveSuccess.Instance.Play(transform);
                    isActivated = true;
                    break;
                case CrystalType.Yellow:
                    // 触发黄色晶石特性
                    anim.SetInteger("Value", crystalYellowValue);
                    EventHandler.CallOnSliderSwing();
                    
                    break;
                case CrystalType.Green:
                    // 触发绿色晶石特性
                    PlayerController.Instance.PlayCrystalSound_1();
                    anim.SetBool("Activate", true);
                    WaveSuccess.Instance.Play(transform);
                    GreenCrystalSet();                    
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
        PlayerController.Instance.PlayCrystalSound_2();
        WaveFail.Instance.Play(transform);
        return false;
    }
    public void RedCrystalSet()
    {        
        //gameObject.layer = LayerMask.NameToLayer("Ground");
        PlayerController.Instance.StartCoroutine(PlayerController.Instance.ChangeOverHeight());
    }
    public void BlueCrystalSet()
    {
        PlayerController.Instance.StartCoroutine(PlayerController.Instance.ChangeJumpCount());
        gameObject.layer = LayerMask.NameToLayer("Blue");
    }
    public void YellowCrystalSet()
    {
        if(isActivated)
        {
            return;
        }
        PlayerController.Instance.PlayCrystalSound_1();
        AdjustFrequencyUI.Instance.StopSwing();
        isActivated = true;
        GetComponent<EnemyControl>().DestroyAllEnemies();
    }
    public void GreenCrystalSet()
    {
        if(isActivated)
        {
            return;
        }
        PlayerController.Instance.RecoverHealth();
        PosManager.Instance.UpdatePos(transform);
        isActivated = true;
    }
    public bool GetIsActivated()
    {
        return isActivated;
    }
    public void SetIsLink(bool isLink)
    {
        this.isLink = isLink;
    }
}