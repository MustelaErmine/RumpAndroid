using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllLevel : MonoBehaviour
{
    [SerializeField] Buttons[] buttons;
    //Назад в меню
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void StarButton(string str)
    {
        print(str);
        byte star = byte.Parse(str[0].ToString());
        byte level = byte.Parse(str[1].ToString());
        bool s1=false, s2=false, s3=false;
        DataSaveLevel data = SaveLevel.Load();
        switch (star) {
            case 1:
                s1 = true;
                s2 = false;
                s3 = false;
                break;
            case 2:
                s1 = true;
                s2 = true;
                s3 = false;
                break;
            case 3:
                s1 = true;
                s2 = true;
                s3 = true;
                break;
        }
        data.Level[level - 1].Star1 = s1;
        data.Level[level - 1].Star2 = s2;
        data.Level[level - 1].Star3 = s3;
        SaveLevel.SaveGameLevel(data);
        buttons[level - 1].Star1 = s1;
        buttons[level - 1].Star2 = s2;
        buttons[level - 1].Star3 = s3;
    }
}
