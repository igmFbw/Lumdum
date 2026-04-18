using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class WaveOption : MonoBehaviour, IPointerClickHandler
{
    public PlayerWaveState attackState;
    public void OnPointerClick(PointerEventData eventData)
    {        
        Selected();            
    }
    public void Selected()
    {
        Debug.Log("切换攻击状态为" + attackState);
        // 切换攻击状态
        EventHolder.CallOnAttackStateChange(attackState);
        // 关闭切换面板
        AttackSwitchUI.Instance.Hide();
        
    }
}
