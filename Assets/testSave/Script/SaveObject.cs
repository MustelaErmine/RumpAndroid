using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject : MonoBehaviour
{
    public Buttons[] buttons;
    public Buttons one;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void save()
    {
        Buttons level = new Buttons();
        buttons[0].Star1 = true;
        buttons[0].Star2 = true;
        buttons[0].Star3 = true;
        buttons[0].Locke = false;
        buttons[0].numberlevel = 1;
        buttons[1].Locke = false;
        SaveLevel.SaveGameLevel(buttons, buttons.Length);

    }
    public void load()
    {
        DataSaveLevel data = SaveLevel.Load();
        for (int i = 0; i < 3; i++)
        {
            
            buttons[i].Star1 = data.Level[i].Star1;
            buttons[i].Star2 = data.Level[i].Star2;
            buttons[i].Star3 = data.Level[i].Star3;
            buttons[i].Locke = data.Level[i].Unlock;
        }
    }


    public void SaveGameLevel(int CurentLevel)
    {
        DataSaveLevel data = SaveLevel.Load();
        Buttons[] array = new Buttons[data.Level.Length];
        for (int j = 0; j < data.Level.Length; j++)
        {
            array[j] = Instantiate(one, new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
        
        
        for(int i = 0; i < data.Level.Length; i++)
        {
            array[i].Star1 = data.Level[i].Star1;
            array[i].Star2 = data.Level[i].Star2;
            array[i].Star3 = data.Level[i].Star3;
            array[i].Locke = data.Level[i].Unlock;
        }
        array[CurentLevel -1].Star1 = true;
        array[CurentLevel -1].Star2 = true;
        array[CurentLevel -1].Star3 = true;
        array[CurentLevel -1].Locke = false;

        if (CurentLevel < data.Level.Length)
        {
            array[CurentLevel].Locke = false;
        }
        SaveLevel.SaveGameLevel(array, data.Level.Length);


    }
}
