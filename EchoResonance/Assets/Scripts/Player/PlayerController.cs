using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    public Rigidbody2D rb;
    public Animator anim;
    private bool isDeath = false;

    [Header("Player Health")]
    [SerializeField]private int health = 3;
    [SerializeField]private float invulnerableTime = 1.5f;
    private float invulnerableTimer = 0;

    [Header("Player Move")]
    [SerializeField]private float speed = 5f;
    [SerializeField]bool isMove = false;
    [SerializeField]bool isFacingRight = false;
    float Horizontal=>Input.GetAxis("Horizontal");

    [Header("Player Jump")]
    [SerializeField]private int jumpCount = 3;
    [SerializeField]private float jumpSpeed = 5f;
    [SerializeField]private float fallSpeed = 5f;
    [SerializeField]bool isAir = false;
    [Header("Ground Check")]
    public LayerMask groundLayer;
    public Color groundCheckColor = Color.red;
    public float groundCheckDistance = 0.5f;
    [SerializeField]bool isGrounded = false;

    private bool isCancelLink = false;
    void OnEnable()
    {
        EventHandler.OnPlayerSpiked += OnPlayerSpiked;
        EventHandler.OnCrystalYellowValueLink += OnCrystalYellowValueLink;
    }
    void OnDisable()
    {
        EventHandler.OnPlayerSpiked -= OnPlayerSpiked;
        EventHandler.OnCrystalYellowValueLink -= OnCrystalYellowValueLink;
    }
    void Update()
    {
        BoolCheck();
        Move();
        Jump();
        FlipController();
        AnimSet();

        MoveCancelLink();

        HealthCanvas.Instance.SetCurrentHealth(health); // 设置血量ui
        // 不可伤害时间
        invulnerableTimer += Time.deltaTime;
        if(invulnerableTimer >= invulnerableTime)
        {
            invulnerableTimer = invulnerableTime;
        }
        // 死亡检测
        if(health<=0)
            Die();
    }
    
    #region  EventHolder
    
    void OnPlayerSpiked()
    {        
        DecHealth();
    }
    void OnCrystalYellowValueLink(bool isLink)
    {
        isCancelLink = isLink;
    }
    #endregion

    #region  Player Action Logic
    void Move()
    {
        rb.velocity = new Vector2(Horizontal * speed, rb.velocity.y);    
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&jumpCount>0) // 跳跃
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            jumpCount--;
        }else if (Input.GetKey(KeyCode.LeftShift)&&!isGrounded) // 滑翔
        {
            rb.velocity = new Vector2(rb.velocity.x, fallSpeed);
        }
    }
    void Die()
    {
        Debug.Log("Player Die");
        isDeath = true;
        Destroy(gameObject, 1.5f);
    }
    void FlipController()
    {
        if (Horizontal > 0.1f && !isFacingRight)
            Flip();
        else if (Horizontal < -0.1f && isFacingRight)
            Flip();
    }
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180f, 0);
    }
    public void DecHealth()
    {
        health--;
        PosManager.Instance.ReturnCrystalGreenPos(transform);
    }
    #endregion
    
    #region Others
    void AnimSet()
    {
        anim.SetBool("IsMoving", isMove);
        anim.SetBool("IsFlying", isAir);
        anim.SetBool("Death", isDeath);
    }
    void BoolCheck()
    {
        // 地面检测
        isGrounded = CheckGround();

        if(isGrounded)
            jumpCount = 3;
            
        // 移动检测
        if(Mathf.Approximately(Horizontal, 0))
        {
            isMove = false;
        }  
        else
            isMove = true;
        // 空中检测
        if(Mathf.Approximately(rb.velocity.y, 0))
            isAir = false;
        else
            isAir = true;
    }
   
    bool CheckGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer).collider;
    }
    public void Reset()
    {
        // 重置位置
        Debug.Log("Player Reset");
        // 重置死亡状态
        isDeath = false;
        //anim.Play("Player_Idle");
    }
    void MoveCancelLink()
    {
        if ((isMove || isAir) && isCancelLink)
        {
            EventHandler.CallOnCrystalYellowValueReset();
            AdjustFrequencyUI.Instance.StopSwing();
            EventHandler.CallOnCrystalYellowValueLink(false);
        }
    }
    #endregion
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")&&invulnerableTimer >= invulnerableTime)
        {
            DecHealth();
            Debug.Log("玩家被敌人攻击");
            invulnerableTimer = 0;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = groundCheckColor;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
