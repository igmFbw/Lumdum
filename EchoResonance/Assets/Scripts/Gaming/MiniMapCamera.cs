// using UnityEngine;

// public class MiniMapCamera : MonoBehaviour
// {
//     // 小地图相机要俯瞰的目标点（整个地图中心）
//     public Transform mapCenter;

//     // 小地图相机高度（越高看的范围越大）
//     public float cameraHeight = 50f;

//     void Start()
//     {
//         // 放在地图正上方俯视
//         transform.position = mapCenter.position + Vector3.up * cameraHeight;
//         // 俯视角度
//         transform.rotation = Quaternion.Euler(90f, 0f, 0f);
//     }
// }