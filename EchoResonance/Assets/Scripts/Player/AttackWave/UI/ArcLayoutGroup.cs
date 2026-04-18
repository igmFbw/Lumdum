using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Layout/Arc Layout Group")]
public class ArcLayoutGroup : LayoutGroup
{
    [Header("弧形设置")]
    [SerializeField] private float radius = 200f;
    [SerializeField] private float totalAngle = 180f;
    [SerializeField] private float startAngle = 90f;
    [SerializeField] private bool rotateItems = true;

    public override void CalculateLayoutInputHorizontal()
    {
        SetLayoutHorizontal();
    }

    public override void CalculateLayoutInputVertical()
    {
        SetLayoutVertical();
    }

    public override void SetLayoutHorizontal()
    {
        Arrange();
    }

    public override void SetLayoutVertical()
    {
        Arrange();
    }

    private void Arrange()
    {
        int childCount = rectTransform.childCount;
        if (childCount <= 0) return;

        // 安全保护：只有1个子物体时不计算角度间隔
        float angleStep = childCount > 1 ? totalAngle / (childCount - 1) : 0;

        for (int i = 0; i < childCount; i++)
        {
            RectTransform child = rectTransform.GetChild(i) as RectTransform;
            if (child == null) continue;

            float angle = startAngle - i * angleStep;
            float rad = angle * Mathf.Deg2Rad;

            // 计算位置
            Vector2 pos = new Vector2(
                radius * Mathf.Cos(rad),
                radius * Mathf.Sin(rad)
            );
            child.anchoredPosition = pos;

            // 旋转子物体（安全版，不触发断言）
            if (rotateItems)
            {
                child.localEulerAngles = new Vector3(0, 0, angle);
            }
            else
            {
                child.localEulerAngles = Vector3.zero;
            }
        }
    }
    public void SetTotalAngle(float angle)
    {
        totalAngle = angle;
        Arrange();
    }
    public float GetTotalAngle()
    {
        return totalAngle;
    }
}