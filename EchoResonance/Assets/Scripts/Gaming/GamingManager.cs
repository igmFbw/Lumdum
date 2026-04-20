using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamingManager : MonoBehaviour
{
    public GameObject player;
    public GameObject gameOverPanel;

    private void Awake()
    {
        Camera.main.GetComponent<CameraFollow>().target = player.transform;
        Camera.main.transform.GetChild(0).gameObject.SetActive(true);
    }
    private void Update()
    {
        if(player == null){return;}
        if(player.GetComponent<PlayerController>().GetIsDeath() == true)
        {
            // 游戏结束
            gameOverPanel.SetActive(true);
        }
    }
}
