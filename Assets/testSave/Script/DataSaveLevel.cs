using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct saveData
{
    public bool Star1;
    public bool Star2;
    public bool Star3;
    public bool Unlock;
    public int Loses;
}
[System.Serializable]
public class DataSaveLevel 
{
    public bool Audio = true;
    public byte Loses = 0;
    public bool First = true;
    public uint Money = 0;
    public byte[] BoughtBoosters = new byte[0];
    public byte[] BoughtCostumes = new byte[]{0};
    public byte SelectedBooster = 0;
    public byte SelectedCostume = 0;
    public saveData[] Level;
    public DataSaveLevel(Buttons[] data, int lengSave)
    {
        Level = new saveData[lengSave];
        for (int i = 0; i < lengSave; i++)
        {
            Level[i].Star1 = data[i].Star1;
            Level[i].Star2 = data[i].Star2;
            Level[i].Star3 = data[i].Star3;
            Level[i].Unlock = data[i].Locke;
        }

    }
    public DataSaveLevel(saveData[] data)
    {
        Level = data;
    }
}
