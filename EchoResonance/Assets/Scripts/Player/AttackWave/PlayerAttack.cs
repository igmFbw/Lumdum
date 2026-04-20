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
    private bool isAttack = true;
    [SerializeField]private float holdtime=0;
    [Range(0,5)][SerializeField]private float holdRangeTime=0.5f;

    private bool isAdjustFrequency = false;
    private float FrequencyValue=>AdjustFrequencyUI.Instance.GetSliderValue();
    private bool isCrystalYellowValueLink = false;
    void OnEnable()
    {
        EventHandler.OnAttackStateChange += OnAttackStateChange;
        EventHandler.OnSliderSwing += OnSliderSwing;
        EventHandler.OnCrystalYellowValueLink += OnCrystalYellowValueLink;
    }
    void OnDisable()
    {
        EventHandler.OnAttackStateChange -= OnAttackStateChange;
        EventHandler.OnSliderSwing -= OnSliderSwing;
        EventHandler.OnCrystalYellowValueLink -= OnCrystalYellowValueLink;
    }
    void OnAttackStateChange(PlayerWaveState state)
    {
        playerAttackState = state;
    }
    void OnCrystalYellowValueLink(bool isLink)
    {
        isCrystalYellowValueLink = isLink;
    }
    void OnSliderSwing()
    {
        // 执行共振
        Debug.Log("共振共振");
        EventHandler.CallOnAttackChange(true);
        EventHandler.CallOnCrystalYellowValueLink(true);
        AdjustFrequencyUI.Instance.Swing();
    }
    void Update()
    {
        if (isCrystalYellowValueLink)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(FrequencyValue>=.5 && FrequencyValue<=.8)
                {
                    EventHandler.CallOnCrystalYellowValueAdd();
                    PlayerController.Instance.PlayCrystalSound_1();
                }
                else
                {
                    EventHandler.CallOnCrystalYellowValueReset();
                    AdjustFrequencyUI.Instance.StopSwing();
                    EventHandler.CallOnCrystalYellowValueLink(false);
                    PlayerController.Instance.PlayCrystalSound_2();
                }
            }
            return;
        }
        if (AttackSwitchUI.Instance.IsShow()){return;}
        
        if (Input.GetKey(KeyCode.Mouse0))
        {      
            ChargingUI.Instance.SetPosition(attackPosition.position);      
            ChargingUI.Instance.Show();            
            ChargingUI.Instance.SetValue(holdtime);
            holdtime+=Time.deltaTime;
            if(holdtime>=holdRangeTime)
            {
                holdtime = holdRangeTime;
            }

            if (!isAdjustFrequency && playerAttackState != PlayerWaveState.None)
            {
                EventHandler.CallOnAttackChange(true);
                isAdjustFrequency = true;
            }
                
        } 
        if (Input.GetKeyUp(KeyCode.Mouse0) && isAttack)
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
        if(FrequencyValue>=.8 && FrequencyValue<=1 && playerAttackState==PlayerWaveState.RedWave){
            SpawnWave(PlayerWaveState.RedWave,90f,waveSpeed,Vector2.zero);
        }else if(FrequencyValue>=0 && FrequencyValue <= .2 && playerAttackState==PlayerWaveState.BlueWave)
        {
            SpawnWave(PlayerWaveState.BlueWave,90f,waveSpeed,Vector2.zero);
        }else if (FrequencyValue>.5 && FrequencyValue < .8 && playerAttackState == PlayerWaveState.YellowWave)
        {
            SpawnWave(PlayerWaveState.YellowWave,0,waveSpeed/3,Vector2.zero);
        }else if (FrequencyValue>.2 && FrequencyValue < .8 && playerAttackState == PlayerWaveState.GreenWave)
        {
            SpawnWave(PlayerWaveState.GreenWave,-90,waveSpeed/3,Vector2.zero);
        }
        ChargingUI.Instance.Hide();
        ChargingUI.Instance.SetValue(0);

        EventHandler.CallOnAttackChange(false);
        isAttack = false;
        isAdjustFrequency = false;
        attackTime = 0;
        holdtime = 0;
    }
    void SpawnWave(PlayerWaveState waveState,float ag,float force,Vector2 offset)   
    {
        // 生成波
        GameObject wave = WavePool.Instance.GetWave(waveState);
        if (wave == null) return;
        // 设置波的生命周期
        Wave waveComponent = wave.GetComponent<Wave>();
        waveComponent.lifeTime = holdtime;
        // 设置波的位置
        wave.transform.position = attackPosition.position+new Vector3(offset.x,offset.y,0);
        // 设置波的方向
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = -Camera.main.transform.position.z; 
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        worldPos.z = 0;
        Vector3 dir = (worldPos - wave.transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        wave.transform.rotation = Quaternion.AngleAxis(angle+ag, Vector3.forward);
        Rigidbody2D rb = wave.GetComponent<Rigidbody2D>();
        rb.velocity = dir * force;
    }
    public bool IsAttack()
    {
        return isAttack;
    }
}
