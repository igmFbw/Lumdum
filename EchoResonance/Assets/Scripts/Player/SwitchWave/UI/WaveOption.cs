using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class WaveOption : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    public PlayerWaveState attackState;
    void Start()
    {
        if(attackState == PlayerWaveState.RedWave)
        {
            Selected();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {        
        Selected();            
    }
    public void Selected()
    {
        Debug.Log("切换攻击状态为" + attackState);
        // 选中攻击状态
        EventHandler.CallOnSelectOption(gameObject);
        // 切换攻击状态
        EventHandler.CallOnAttackStateChange(attackState);
        // 关闭切换面板
        AttackSwitchUI.Instance.Hide();
        // 切换轮廓颜色
        OutLineHolder.Instance.SetColorSmooth(checkColor(attackState));
    }
    private Color checkColor(PlayerWaveState state)
    {
        switch (state)
        {
            case PlayerWaveState.RedWave:
                return Color.red;
            case PlayerWaveState.BlueWave:
                return Color.blue;
            case PlayerWaveState.YellowWave:
                return Color.yellow;
            case PlayerWaveState.GreenWave:
                return Color.green;
            default:
                return Color.white;
        }
    }
}
