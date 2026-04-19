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

    private bool isAdjustFrequency = false;
    private float FrequencyValue=>AdjustFrequencyUI.Instance.GetSliderValue();
    void OnEnable()
    {
        EventHolder.OnAttackStateChange += OnAttackStateChange;
        EventHolder.OnSliderSwing += OnSliderSwing;
    }
    void OnDisable()
    {
        EventHolder.OnAttackStateChange -= OnAttackStateChange;
        EventHolder.OnSliderSwing -= OnSliderSwing;
    }
    void OnAttackStateChange(PlayerWaveState state)
    {
        playerAttackState = state;
    }
    void OnSliderSwing()
    {
        // 执行共振
        Debug.Log("共振共振");
        EventHolder.CallOnAttackChange(true);
        AdjustFrequencyUI.Instance.Swing();
    }
    void Update()
    {

        ChangePlayerAttackState();

        if (AttackSwitchUI.Instance.IsShow()){return;}
        
        if (Input.GetKey(KeyCode.Mouse0))
        {            
            holdtime+=Time.deltaTime;
            if(holdtime>=holdRangeTime)
            {
                holdtime = holdRangeTime;
            }

            if (!isAdjustFrequency && (playerAttackState == PlayerWaveState.RedWave || playerAttackState == PlayerWaveState.BlueWave))
            {
                EventHolder.CallOnAttackChange(true);
                isAdjustFrequency = true;
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

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(FrequencyValue>=.4 && FrequencyValue<=5)
            {
                EventHolder.CallOnCrystalYellowValueAdd();
            }
            else
            {
                EventHolder.CallOnCrystalYellowValueReset();
                AdjustFrequencyUI.Instance.StopSwing();
            }
        }

    }
    void Attack()
    {        
        if(FrequencyValue>=.8 && FrequencyValue<=1 && playerAttackState==PlayerWaveState.RedWave){
            SpawnWave(PlayerWaveState.RedWave);
        }else if(FrequencyValue>=0 && FrequencyValue < .2 && playerAttackState==PlayerWaveState.BlueWave)
        {
            SpawnWave(PlayerWaveState.BlueWave);
        }else if (playerAttackState == PlayerWaveState.YellowWave)
        {
            SpawnWave(PlayerWaveState.YellowWave);
        }else
        {
            SpawnWave(PlayerWaveState.None);
        }

        EventHolder.CallOnAttackChange(false);
        isAttack = false;
        isAdjustFrequency = false;
        attackTime = 0;
        holdtime = 0;
    }
    void SpawnWave(PlayerWaveState waveState)
    {
        // 生成波
        GameObject wave = WavePool.Instance.GetWave(waveState);
        if (wave == null) return;
        // 设置波的生命周期
        Wave waveComponent = wave.GetComponent<Wave>();
        waveComponent.LifeTime = holdtime;
        // 设置波的位置
        wave.transform.position = attackPosition.position;
        // 设置波的方向
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = -Camera.main.transform.position.z; 
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        worldPos.z = 0;
        Vector3 dir = (worldPos - wave.transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        wave.transform.rotation = Quaternion.AngleAxis(angle+90, Vector3.forward);
        Rigidbody2D rb = wave.GetComponent<Rigidbody2D>();
        rb.velocity = dir * waveSpeed;
    }
    void ChangePlayerAttackState()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            playerAttackState = PlayerWaveState.RedWave;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            playerAttackState = PlayerWaveState.BlueWave;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            playerAttackState = PlayerWaveState.YellowWave; 
        if (Input.GetKeyDown(KeyCode.Alpha4))
            playerAttackState = PlayerWaveState.GreenWave;
    }
    public bool IsAttack()
    {
        return isAttack;
    }
}
