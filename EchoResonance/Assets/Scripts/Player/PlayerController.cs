using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    public Rigidbody2D rb;
    public PlayerAttackState playerAttackState;

    [Header("Player Move")]
    [SerializeField]private float speed = 5f;
    [SerializeField]bool isMove = false;
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
    [SerializeField]bool isFacingRight = false;
    void OnEnable()
    {
        EventHolder.OnAttackStateChange += OnAttackStateChange;
    }
    void OnDisable()
    {
        EventHolder.OnAttackStateChange -= OnAttackStateChange;
    }
    void OnAttackStateChange(PlayerAttackState state)
    {
        playerAttackState = state;
    }

    void Update()
    {
        BoolCheck();
        Move();
        Jump();
        FlipController();
    }
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

        // 方法1：旋转Y轴 180°（推荐，不影响子物体）
        transform.Rotate(0, 180f, 0);

        // 方法2：缩放Scale(-1,1,1)（如果你喜欢用缩放翻转，用这个）
        // Vector3 scale = transform.localScale;
        // scale.x *= -1;
        // transform.localScale = scale;
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
    void OnDrawGizmos()
    {
        Gizmos.color = groundCheckColor;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
