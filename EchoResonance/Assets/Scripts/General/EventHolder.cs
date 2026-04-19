using System;
using UnityEngine;
public class EventHandler
{
    public static event Action<PlayerWaveState> OnAttackStateChange;
    public static event Action OnPlayerSpiked;
    public static event Action<bool> OnAttackChange;
    public static event Action OnSliderSwing;
    public static event Action OnCrystalYellowValueAdd;
    public static event Action OnCrystalYellowValueReset;
    public static event Action<bool> OnCrystalYellowValueLink;
    public static event Action<GameObject> OnSelectOption;

    public static void CallOnAttackStateChange(PlayerWaveState state)
    {
        Console.WriteLine(state);
        OnAttackStateChange?.Invoke(state);
    }
    public static void CallOnPlayerSpiked()
    {
        OnPlayerSpiked?.Invoke();
    }
    public static void CallOnAttackChange(bool isAttack)
    {
        OnAttackChange?.Invoke(isAttack);
    }
    public static void CallOnSliderSwing()
    {
        OnSliderSwing?.Invoke();
    }
    public static void CallOnCrystalYellowValueAdd()
    {
        OnCrystalYellowValueAdd?.Invoke();
    }
    public static void CallOnCrystalYellowValueReset()
    {
        OnCrystalYellowValueReset?.Invoke();
    }
    public static void CallOnCrystalYellowValueLink(bool isLink)
    {
        OnCrystalYellowValueLink?.Invoke(isLink);
    }
    public static void CallOnSelectOption(GameObject option)
    {
        OnSelectOption?.Invoke(option);
    }

}
