using System;
using UnityEngine;
public class EventHolder
{
    public static event Action<PlayerWaveState> OnAttackStateChange;
    public static event Action OnPlayerSpiked;
    public static void CallOnAttackStateChange(PlayerWaveState state)
    {
        Console.WriteLine(state);
        OnAttackStateChange?.Invoke(state);
    }
    public static void CallOnPlayerSpiked()
    {
        OnPlayerSpiked?.Invoke();
    }
}
