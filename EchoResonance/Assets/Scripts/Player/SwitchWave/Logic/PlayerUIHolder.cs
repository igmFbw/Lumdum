using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIHolder : MonoBehaviour
{
    private bool isCrystalYellowValueLink = false;
    void OnEnable()
    {
        EventHandler.OnAttackChange += OnAttackChange;
        EventHandler.OnCrystalYellowValueLink += OnCrystalYellowValueLink;
    }
    void OnDisable()
    {
        EventHandler.OnAttackChange -= OnAttackChange;
        EventHandler.OnCrystalYellowValueLink -= OnCrystalYellowValueLink;
    }


    void Update()
    {
        if (isCrystalYellowValueLink)
        {
            SetAdjustFrequencyUI();
            return;
        }
            
        SwitchWave();
        SetSwitchWaveUI();
        SetAdjustFrequencyUI();
        SetAdjustFrequencyValue();
    }

    private void SetAdjustFrequencyValue()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        AddAdjustFrequencyValue(scroll);
    }

    void SetSwitchWaveUI()
    {      
        // if(!AttackSwitchUI.Instance.IsShow())
        //     return;  
        AttackSwitchUI.Instance.SetPosition(transform.position);
    }
    void SetAdjustFrequencyUI()
    {
        // if(!AdjustFrequencyUI.Instance.IsShow())
        //     return;        
        AdjustFrequencyUI.Instance.SetPosition(transform.position);
    }
    void AddAdjustFrequencyValue(float value)
    {
        if(!AdjustFrequencyUI.Instance.IsShow())
            return;
        AdjustFrequencyUI.Instance.AddSliderValue(value);
    }
    private void SwitchWave()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetSwitchWaveUI();
            AttackSwitchUI.Instance.Show();

        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            AttackSwitchUI.Instance.Hide();
        }
    }
        private void OnAttackChange(bool isAttack)
    {
        if(isAttack)
            AdjustFrequencyUI.Instance.Show();
        else
            AdjustFrequencyUI.Instance.Hide();
    }
    private void OnCrystalYellowValueLink(bool isLink)
    {
        isCrystalYellowValueLink = isLink;
    }
}
