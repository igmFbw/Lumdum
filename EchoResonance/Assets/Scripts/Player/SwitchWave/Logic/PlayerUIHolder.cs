using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIHolder : MonoBehaviour
{
    void OnEnable()
    {
        EventHolder.OnAttackChange += OnAttackChange;
    }
    void OnDisable()
    {
        EventHolder.OnAttackChange -= OnAttackChange;
    }

    void Update()
    {
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
        if(!AttackSwitchUI.Instance.IsShow())
            return;  
        AttackSwitchUI.Instance.SetPosition(transform.position);
    }
    void SetAdjustFrequencyUI()
    {
        if(!AdjustFrequencyUI.Instance.IsShow())
            return;        
        AdjustFrequencyUI.Instance.SetPosition(transform.position);
    }
    void AddAdjustFrequencyValue(float value)
    {
        if(!AdjustFrequencyUI.Instance.IsShow())
            return;
        Debug.Log("SetAdjustFrequencyUIValue");
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
}
