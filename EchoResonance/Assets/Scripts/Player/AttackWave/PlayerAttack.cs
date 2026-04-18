using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerWaveState playerAttackState;
    [SerializeField]private float holdtime=0;
    [Range(0,5)][SerializeField]private float holdRangeTime=0.5f;
    public GameObject wavePrefab;
    void OnEnable()
    {
        EventHolder.OnAttackStateChange += OnAttackStateChange;
    }
    void OnDisable()
    {
        EventHolder.OnAttackStateChange -= OnAttackStateChange;
    }
    void OnAttackStateChange(PlayerWaveState state)
    {
        playerAttackState = state;
    }
    void Update()
    {
        if(AttackSwitchUI.Instance.IsShow()){return;}

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Debug.Log("Attack");
            Attack();
        }else if (Input.GetKey(KeyCode.Mouse0))
        {
            holdtime+=Time.deltaTime;
            if(holdtime>=holdRangeTime)
            {
                holdtime = holdRangeTime;
            }
        }
    }
    void Attack()
    {
        switch (playerAttackState)
        {
            case PlayerWaveState.RedWave:
                break;
            case PlayerWaveState.GreenWave:
                break;
            case PlayerWaveState.BlueWave:
                break;
            case PlayerWaveState.YellowWave:
                break;
            case PlayerWaveState.None:
                break;
            default:
                break;
        }
        holdtime = 0;
    }
}
