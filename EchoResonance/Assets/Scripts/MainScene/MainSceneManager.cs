using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        Camera.main.transform.GetChild(0).gameObject.SetActive(false); 
    }


}
