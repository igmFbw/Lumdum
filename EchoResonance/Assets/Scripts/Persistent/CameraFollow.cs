using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [Header("跟随目标")]
    public Transform target;

    [Header("相机设置")]
    public Vector3 offset = new Vector3(0, 2, -10); // 偏移
    public float followSpeed = 2f;                 // 平滑速度
    public float maxDistance = 10f;               // 超过这个距离直接瞬移

    private void LateUpdate()
    {
        if (target == null) return;

        // 目标位置 + 偏移
        Vector3 targetPos = target.position + offset;
        targetPos.z = transform.position.z; // 固定相机Z轴

        // 判断距离是否过远
        float distance = Vector3.Distance(transform.position, targetPos);

        if (distance > maxDistance)
        {
            // 过远直接跳过去
            transform.position = targetPos;
        }
        else
        {
            // 平滑跟随
            transform.position = Vector3.Lerp(
                transform.position, 
                targetPos, 
                followSpeed * Time.deltaTime
            );
        }
    }
}