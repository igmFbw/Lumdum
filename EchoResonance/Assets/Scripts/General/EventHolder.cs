using System;
public class EventHolder
{
    public static event Action<PlayerAttackState> OnAttackStateChange;
    public static void CallOnAttackStateChange(PlayerAttackState state)
    {
        OnAttackStateChange?.Invoke(state);
    }
}
