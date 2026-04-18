using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class WavePool : Singleton<WavePool>
{
    public GameObject waveRedPrefab;
    public GameObject waveBluePrefab;
    public GameObject waveGreenPrefab;
    public GameObject waveYellowPrefab;
    private List<GameObject> waveRedList = new List<GameObject>();
    private List<GameObject> waveBlueList = new List<GameObject>();
    private List<GameObject> waveGreenList = new List<GameObject>();
    private List<GameObject> waveYellowList = new List<GameObject>();   

    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            CreateWave(waveRedPrefab, waveRedList);
            CreateWave(waveBluePrefab, waveBlueList);
            CreateWave(waveGreenPrefab, waveGreenList);       
            CreateWave(waveYellowPrefab, waveYellowList);
        }
    }
    private void CreateWave(GameObject wave, List<GameObject> waveList)
    {
        GameObject wa = Instantiate(wave);
        waveList.Add(wa);
        wa.transform.SetParent(transform);
        wa.transform.localPosition = Vector3.zero;
        wa.SetActive(false);
    }
    private GameObject CreateWaveReturn(GameObject wave, List<GameObject> waveList)
    {
        GameObject wa = Instantiate(wave);
        waveList.Add(wa);
        wa.transform.SetParent(transform);
        wa.transform.localPosition = Vector3.zero;
        return wa;
    }
    public GameObject GetWave(PlayerWaveState waveState)
    {
        switch (waveState)
        {
            case PlayerWaveState.RedWave:
                return GetWaveRed();
            case PlayerWaveState.BlueWave:
                return GetWaveBlue();
            case PlayerWaveState.GreenWave:
                return GetWaveGreen();
            case PlayerWaveState.YellowWave:
                return GetWaveYellow();
            default:
                return null;
        }
    }
    private GameObject GetWaveRed()
    {
        foreach(GameObject wave in waveRedList)
        {
            if(!wave.activeSelf)
            {
                wave.SetActive(true);
                return wave;
            }
        }
        return CreateWaveReturn(waveRedPrefab, waveRedList);
    }
    private GameObject GetWaveBlue()
    {
        foreach(GameObject wave in waveBlueList)
        {
            if(!wave.activeSelf)
            {
                wave.SetActive(true);
                return wave;
            }
        }
        return CreateWaveReturn(waveBluePrefab, waveBlueList);
    }
    private GameObject GetWaveGreen()
    {
        foreach(GameObject wave in waveGreenList)
        {
            if(!wave.activeSelf)
            {
                wave.SetActive(true);
                return wave;
            }
        }
        return CreateWaveReturn(waveGreenPrefab, waveGreenList);
    }
    private GameObject GetWaveYellow()
    {
        foreach(GameObject wave in waveYellowList)
        {
            if(!wave.activeSelf)
            {
                wave.SetActive(true);
                return wave;
            }
        }
        return CreateWaveReturn(waveYellowPrefab, waveYellowList);
    }

}
