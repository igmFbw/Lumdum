using System;
public class EventHolder
{
    public static event Action<PlayerAttackState> OnAttackStateChange;
    public static event Action OnPlayerSpiked;
    public static void CallOnAttackStateChange(PlayerAttackState state)
    {
        OnAttackStateChange?.Invoke(state);
    }
    public static void CallOnPlayerSpiked()
    {
        OnPlayerSpiked?.Invoke();
    }
}
