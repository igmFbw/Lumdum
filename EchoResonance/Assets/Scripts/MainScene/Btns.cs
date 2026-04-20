using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btns : MonoBehaviour
{
    public GameObject exitBtn;
    public GameObject CreditsBtn;
    public GameObject CreditsCloseBtn;
    public GameObject CredistPanel;
    void Awake()
    {
        exitBtn.GetComponent<Button>().onClick.AddListener(ExitGame);
        CreditsBtn.GetComponent<Button>().onClick.AddListener(ShowCredits);
        CreditsCloseBtn.GetComponent<Button>().onClick.AddListener(CloseCredits);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ShowCredits()
    {
        CredistPanel.SetActive(true);
    }
    public void CloseCredits()
    {
        CredistPanel.SetActive(false);
    }
}
