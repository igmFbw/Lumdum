using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    public Rigidbody2D rb;
    public Animator anim;
    private bool isDeath = false;

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
    void OnEnable()
    {
        EventHolder.OnPlayerSpiked += OnPlayerSpiked;
    }
    void OnDisable()
    {
        EventHolder.OnPlayerSpiked -= OnPlayerSpiked;
    }
    void Update()
    {
        BoolCheck();
        Move();
        Jump();
        FlipController();
        AnimSet();
    }
    
    #region  EventHolder
    
    void OnPlayerSpiked()
    {
        Die();
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
            isMove = false;
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
        anim.Play("Player_Idle");
    }
    #endregion
    void OnDrawGizmos()
    {
        Gizmos.color = groundCheckColor;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
