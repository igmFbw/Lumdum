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
        Debug.Log("重玩");
    }
    private void ReturnMenu()
    {
        Debug.Log("返回主菜单");
    }
    private void OnDisable()
    {
        BtnPlayAgain.onClick.RemoveAllListeners();
        BtnReturnMenu.onClick.RemoveAllListeners();
    }
}
