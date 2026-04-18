using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerWaveState playerAttackState;
    [SerializeField]private Transform attackPosition;
    [SerializeField]private float waveSpeed=12f;
    [SerializeField]private float attackFrequency=0.5f;
    private float attackTime=0;
    private bool isAttack = false;
    [SerializeField]private float holdtime=0;
    [Range(0,5)][SerializeField]private float holdRangeTime=0.5f;
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
        
        if (Input.GetKey(KeyCode.Mouse0))
        {            
            holdtime+=Time.deltaTime;
            if(holdtime>=holdRangeTime)
            {
                holdtime = holdRangeTime;
            }
        }else if (Input.GetKeyUp(KeyCode.Mouse0) && isAttack)
        {
            Attack();
        }
        attackTime+=Time.deltaTime;
        if(attackTime>=attackFrequency)
        {
            attackTime = attackFrequency;
            isAttack = true;
        }

    }
    void Attack()
    {
        SpawnWave();

        isAttack = false;
        attackTime = 0;
        holdtime = 0;
    }
    void SpawnWave()
    {
        GameObject wave = WavePool.Instance.GetWave(playerAttackState);
        if (wave == null) return;

        Wave waveComponent = wave.GetComponent<Wave>();
        waveComponent.LifeTime = holdtime;

        wave.transform.position = attackPosition.position;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector2 dir = (mousePos - attackPosition.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        wave.transform.Rotate(0, 0, angle+90f);

        Rigidbody2D rb = wave.GetComponent<Rigidbody2D>();
        rb.velocity = dir * waveSpeed;
    }

}
