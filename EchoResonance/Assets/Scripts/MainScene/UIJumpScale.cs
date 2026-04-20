using UnityEngine;

public class UIJumpScale : MonoBehaviour
{
    [Header("跳动设置")]
    public float jumpHeight = 10f;    // 上下跳动高度
    public float jumpSpeed = 3f;      // 跳动速度

    [Header("缩放设置")]
    public float scaleAmount = 0.15f; // 缩放幅度
    public float scaleSpeed = 3f;     // 缩放速度

    private Vector3 startPos;
    private Vector3 startScale;

    void Start()
    {
        startPos = transform.localPosition;
        startScale = transform.localScale;
    }

    void Update()
    {
        // 上下正弦跳动
        float yOffset = Mathf.Sin(Time.time * jumpSpeed) * jumpHeight;
        transform.localPosition = startPos + new Vector3(0, yOffset, 0);

        // 同步缩放（和跳动节奏匹配）
        float scaleOffset = Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        transform.localScale = startScale + new Vector3(scaleOffset, scaleOffset, 0);
    }
}