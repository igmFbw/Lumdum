using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonTextSize : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [Header("关联按钮上的 Text 或 TMP_Text")]
    public Text uiText;

    [Header("缩放设置")]
    public float pressScale = 0.85f;

    private Vector3 normalScale;

    void Awake()
    {
        if (uiText != null)
        {
            normalScale = uiText.transform.localScale;
        }
    }

    // 按下
    public void OnPointerDown(PointerEventData eventData)
    {
        if (uiText != null)
        {
            uiText.transform.localScale = normalScale * pressScale;
        }
    }

    // 松开
    public void OnPointerUp(PointerEventData eventData)
    {
        RecoverScale();
    }

    // 鼠标/手指滑出去
    public void OnPointerExit(PointerEventData eventData)
    {
        RecoverScale();
    }

    // 恢复正常大小
    void RecoverScale()
    {
        if (uiText != null)
        {
            uiText.transform.localScale = normalScale;
        }
    }
}