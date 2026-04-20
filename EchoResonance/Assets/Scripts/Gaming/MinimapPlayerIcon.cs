using UnityEngine;

public class MinimapPlayerIcon : MonoBehaviour
{
    [Header("玩家")]
    public Transform player;

    [Header("真实地图边界")]
    public Transform mapMin;    // 地图左下角
    public Transform mapMax;    // 地图右上角

    [Header("小地图UI区域")]
    public RectTransform minimapRect;  // 小地图背景框

    private RectTransform iconRect;

    void Awake()
    {
        iconRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        UpdatePlayerPosOnMinimap();
    }

    void UpdatePlayerPosOnMinimap()
    {
        // 1. 计算玩家在真实地图里的 0~1 比例
        float xPercent = Mathf.InverseLerp(mapMin.position.x, mapMax.position.x, player.position.x);
        float yPercent = Mathf.InverseLerp(mapMin.position.y, mapMax.position.y, player.position.y);

        // 2. 按比例映射到小地图UI内
        float uiX = Mathf.Lerp(0, minimapRect.rect.width, xPercent);
        float uiY = Mathf.Lerp(0, minimapRect.rect.height, yPercent);

        // 3. 设置图标位置
        iconRect.anchoredPosition = new Vector2(uiX, uiY);
    }
}