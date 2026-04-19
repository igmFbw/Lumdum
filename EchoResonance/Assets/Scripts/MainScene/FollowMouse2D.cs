using UnityEngine;

public class FollowMouse2D : MonoBehaviour
{
    [Tooltip("Z轴固定值（2D场景通常设为0）")]
    public float zPosition = 0;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("场景中没有主相机！请给相机添加Tag：MainCamera");
        }
    }

    void Update()
    {
        if (mainCamera == null) return;

        // 将屏幕鼠标位置转换为世界坐标（2D专用）
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // 固定Z轴，避免2D物体因Z轴问题显示异常
        mouseWorldPos.z = zPosition;
        // 移动物体到鼠标位置
        transform.position = mouseWorldPos;
    }
}