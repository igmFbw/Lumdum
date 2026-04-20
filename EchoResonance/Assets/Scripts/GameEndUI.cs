using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameEndUI : MonoBehaviour
{
    [SerializeField] private Button BtnPlayAgain;
    [SerializeField] private Button BtnReturnMenu;
    private void Awake()
    {
        BtnReturnMenu.onClick.AddListener(ReturnMenu);
        BtnPlayAgain.onClick.AddListener(PlayAgain);
    }
    private void PlayAgain()
    {
        TeleportManager.Instance.ResetScene();
    }
    private void ReturnMenu()
    {
        TeleportManager.Instance.Teleport("GameScene", "MainScene");
    }
    private void OnDisable()
    {
        BtnPlayAgain.onClick.RemoveAllListeners();
        BtnReturnMenu.onClick.RemoveAllListeners();
    }
}
