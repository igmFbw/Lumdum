using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    [Header("跟随目标")]
    [Tooltip("需要跟随的目标（如玩家）")]
    public Transform target;

    [Header("微动画设置")]
    [Tooltip("移动灵敏度（值越小，跟随越迟钝，建议0.005-0.02）")]
    [Range(0.001f, 1f)]
    public float sensitivity = 0.01f;

    [Tooltip("X轴移动幅度比例（0.01-0.1之间，越小晃动越轻微）")]
    [Range(0.01f, 0.3f)]
    public float xSwayRatio = 0.05f;

    [Tooltip("Y轴移动幅度比例（0.01-0.1之间，越小晃动越轻微）")]
    [Range(0.01f, 0.3f)]
    public float ySwayRatio = 0.05f;

    [Header("限制范围")]
    [Tooltip("最大X偏移（防止晃动过度）")]
    public float maxXSway = 0.5f;

    [Tooltip("最大Y偏移（防止晃动过度）")]
    public float maxYSway = 0.3f;

    private Vector3 _initialPos;       // 背景初始位置
    private Vector3 _targetStartPos;   // 目标初始位置
    private Vector3 _currentVelocity;  // 用于平滑阻尼的速度变量

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("请指定跟随目标！");
            enabled = false;
            return;
        }

        // 记录初始位置作为参考点
        _initialPos = transform.position;
        _targetStartPos = target.position;
    }

    private void LateUpdate()
    {
        // 计算目标相对于初始位置的偏移
        float targetXOffset = (target.position.x - _targetStartPos.x) * xSwayRatio;
        float targetYOffset = (target.position.y - _targetStartPos.y) * ySwayRatio;

        // 限制最大晃动幅度
        targetXOffset = Mathf.Clamp(targetXOffset, -maxXSway, maxXSway);
        targetYOffset = Mathf.Clamp(targetYOffset, -maxYSway, maxYSway);

        // 计算最终目标位置（保持Z轴不变）
        Vector3 targetPos = new Vector3(
            _initialPos.x + targetXOffset,
            _initialPos.y + targetYOffset,
            _initialPos.z
        );

        // 用平滑阻尼实现极其缓慢的跟随效果，模拟轻微晃动
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref _currentVelocity,
            1f - sensitivity * 20  // 阻尼时间，灵敏度越高，阻尼越小
        );
    }

    // 绘制辅助线显示最大晃动范围
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.8f, 0.8f, 1f, 0.2f);
        Vector3 center = _initialPos == Vector3.zero ? transform.position : _initialPos;
        
        // 绘制最大晃动范围框
        Gizmos.DrawWireCube(center, new Vector3(
            maxXSway * 2, 
            maxYSway * 2, 
            0.1f
        ));
    }
}
