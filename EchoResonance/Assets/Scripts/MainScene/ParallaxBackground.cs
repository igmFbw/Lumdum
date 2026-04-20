using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 3f;
    public float moveRate = 0.8f; // 背景移动速度比例

    private Vector3 lastPlayerPos;

    void Start()
    {
        lastPlayerPos = player.position;
    }

    void LateUpdate()
    {
        Vector3 delta = player.position - lastPlayerPos;
        lastPlayerPos = player.position;

        Vector3 targetPos = transform.position + delta * moveRate;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            smoothSpeed * Time.deltaTime
        );
    }
}