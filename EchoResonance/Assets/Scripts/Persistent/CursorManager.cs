using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [Header("鼠标纹理")]
    public Texture2D cursorTexture;

    [Header("热点位置")]
    public Vector2 hotSpot = Vector2.zero;

    [Header("鼠标模式")]
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        // 启动时立即替换
        ChangeCursor(cursorTexture);
    }

    // 通用更新鼠标的函数
    public void ChangeCursor(Texture2D newCursorTex)
    {
        if (newCursorTex == null) return;
        
        // 参数1：纹理，参数2：热点（点击中心的位置），参数3：模式
        Cursor.SetCursor(newCursorTex, hotSpot, cursorMode);
    }

    // 恢复默认鼠标
    public void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}