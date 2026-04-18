using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIHolder : MonoBehaviour
{
    void Update()
    {
        SwitchWave();
        SetUI();
    }
    void SetUI()
    {      
        if(!AttackSwitchUI.Instance.IsShow())
            return;  
        AttackSwitchUI.Instance.SetPosition(transform.position);
    }
    private void SwitchWave()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetUI();
            AttackSwitchUI.Instance.Show();

        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            AttackSwitchUI.Instance.Hide();
        }
    }
}
