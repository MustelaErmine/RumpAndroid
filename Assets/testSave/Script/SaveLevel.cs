using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net.Http.Headers;

public static class SaveLevel
{
    // Start is called before the first frame update

    public static void SaveGameLevel(Buttons[] Level, int lengSave)
    {
        //BinaryFormatter formatet = new BinaryFormatter();
        string patch = Application.persistentDataPath + "/SaveLevel.RAMP";
        //FileStream stream = new FileStream(patch, FileMode.OpenOrCreate);
        DataSaveLevel data = new DataSaveLevel(Level, lengSave);
        //formatet.Serialize(stream, data);
        //stream.Close();
        File.WriteAllText(patch,JsonUtility.ToJson(data));
    }
    public static void SaveGameLevel(DataSaveLevel level)
    {
        string patch = Application.persistentDataPath + "/SaveLevel.RAMP";
        Debug.Log(patch);
        File.WriteAllText(patch, JsonUtility.ToJson(level));
    }

    public static DataSaveLevel Load()
    {
        string patch = Application.persistentDataPath + "/SaveLevel.RAMP";
        Debug.Log(patch);
        if (File.Exists(patch))
        {
            //BinaryFormatter formatet = new BinaryFormatter();
            //FileStream stream = new FileStream(patch, FileMode.Open);
            DataSaveLevel data = /*formatet.Deserialize(stream) as DataSaveLevel*/
                JsonUtility.FromJson<DataSaveLevel>(File.ReadAllText(patch));
            //stream.Close();
            return data;
        }
        else
        {
            Debug.Log("not File load");
            return null;
        }
    }
}
