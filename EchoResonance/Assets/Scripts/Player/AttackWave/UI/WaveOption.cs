using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class WaveOption : MonoBehaviour, IPointerClickHandler
{
    public PlayerAttackState attackState;
    public void OnPointerClick(PointerEventData eventData)
    {
        // 切换攻击状态
        EventHolder.CallOnAttackStateChange(attackState);
        // 关闭切换面板
        
    }
}
