using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private DownLine downLine;
    [SerializeField] private TowerLine towerLine;

    private IObjectResolver objectResolver;
    private Factory factory;

    [Inject]
    public void Construct(IObjectResolver objectResolver, Factory factory)
    {
        this.objectResolver = objectResolver;
        this.factory = factory;

        /*downLine.onAddInList += (_) => OnDownLineChange();
        downLine.onRemoveOutList += (_) => OnDownLineChange();
        towerLine.onAddInList += (_) => OnTowerLineChange();
        towerLine.onRemoveOutList += (_) => OnTowerLineChange();*/
    }

    private void OnDownLineChange()
    {
        Save();
    }

    private void OnTowerLineChange()
    {
        Save();
    }

    public void Save()
    {
        var saveData = new SaveData();
        saveData.CubiksInTower = new CubikInfo[towerLine.Sortables.Count];
        saveData.CubiksInDownLine = new CubikInfo[downLine.Sortables.Count];

        for (int i = 0; i < downLine.Sortables.Count; i++)
        {
            var item = downLine.Sortables[i] as CubikMove;
            var info = item.CubikInfo;
            saveData.CubiksInDownLine[i] = info;
        }

        for (int i = 0; i < towerLine.Sortables.Count; i++)
        {
            var item = towerLine.Sortables[i] as CubikMove;
            var info = item.CubikInfo;
            saveData.CubiksInTower[i] = info;
        }

        PlayerPrefs.SetString("Save", JsonUtility.ToJson(saveData));
        Debug.Log("Save");
    }

    public bool Load()
    {
        string json = PlayerPrefs.GetString("Save", null);
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log("Load false");
            return false;
        }

        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        FullLine(downLine, saveData.CubiksInDownLine);
        FullLine(towerLine, saveData.CubiksInTower);

        Observable.Timer(TimeSpan.FromSeconds(Time.deltaTime)).Subscribe(_ =>
        {
            MessageBroker.Default.Publish(new TextLineLocalizMessage("Load", -1));
        });

        Debug.Log("Load true");
        return true;
    }

    private void FullLine(DownLine line, CubikInfo[] cubiks)
    {
        for (int i = 0; i < cubiks.Length; i++)
        {
            CubikInfo data = cubiks[i];
            Vector3 pos = line.GetPosition(i);

            var cubik = factory.CreateCubik(pos);
            cubik.SetInfo(data);
            line.AddInList(cubik);
        }
    }

    private void OnApplicationQuit()
    {
        Save();   
    }
}

[Serializable]
public class SaveData
{
    public CubikInfo[] CubiksInTower;
    public CubikInfo[] CubiksInDownLine;
}
