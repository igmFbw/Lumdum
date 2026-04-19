using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("巡逻设置")]
    public float patrolSpeed = 2f;               // 巡逻速度
    public Vector3 patrolCenter;                 // 巡逻圆中心（可自定义）
    public float circleRadius = 5f;             // 巡逻半径
    public float minStayTime = 0.5f;             // 到达目标后最小停留时间
    public float maxStayTime = 2f;               // 到达目标后最大停留时间

    [Header("追逐设置")]
    public float detectRange = 8f;
    public float chaseSpeed = 4f;
    public float loseTime = 3f;

    [Header("攻击")]
    public float attackInterval = 5f;

    private Rigidbody2D rb;
    private Transform player;
    private Vector2 currentPatrolTarget;
    private bool isChasing;
    private float loseTimer;
    private float attackTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (patrolCenter == Vector3.zero)
            patrolCenter = transform.position;

        currentPatrolTarget = GetRandomPointInPatrolCircle();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        CheckPlayer();
    }

    void FixedUpdate()
    {
        if (isChasing && player != null)
            ChasePlayer();
        else
            DoPatrol();
    }

    Vector2 GetRandomPointInPatrolCircle()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        float randomDist = Random.Range(circleRadius * 0.2f, circleRadius);
        return (Vector2)patrolCenter + randomDir * randomDist;
    }

    void CheckPlayer()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go == null) return;
        player = go.transform;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist < detectRange)
        {
            isChasing = true;
            loseTimer = 0;
        }
        else
        {
            loseTimer += Time.deltaTime;
            if (loseTimer >= loseTime)
                isChasing = false;
        }
    }

    void ChasePlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        rb.velocity = dir * chaseSpeed;
        Flip(dir.x);
    }

    void DoPatrol()
    {
        float distToTarget = Vector2.Distance(transform.position, currentPatrolTarget);

        if (distToTarget > 0.5f)
        {
            Vector2 dir = (currentPatrolTarget - (Vector2)transform.position).normalized;
            rb.velocity = dir * patrolSpeed;
            Flip(dir.x);
        }
        else
        {
            rb.velocity = Vector2.zero;
            if (!IsInvoking(nameof(SetNewPatrolTarget)))
            {
                Invoke(nameof(SetNewPatrolTarget), Random.Range(minStayTime, maxStayTime));
            }
        }
    }

    void SetNewPatrolTarget()
    {
        currentPatrolTarget = GetRandomPointInPatrolCircle();
    }

    void Flip(float dirX)
    {
        if (dirX > 0.1f)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (dirX < -0.1f)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("攻击");
        if (other.collider.CompareTag("Player") && attackTimer >= attackInterval)
        {
            attackTimer = 0;
            Debug.Log("攻击玩家");
            other.collider.GetComponent<PlayerController>().DecHealth();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(patrolCenter, circleRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(currentPatrolTarget, 0.2f);
    }
}