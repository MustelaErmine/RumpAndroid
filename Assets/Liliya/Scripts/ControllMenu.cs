using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AppodealAds.Unity.Api;

public class ControllMenu : MonoBehaviour
{
    public Text money;
    public GameObject settings;
    public static AudioSource unDestroyedBG = null;
    public AudioSource myBG;
    public uint Money
    {
        set => money.text = value.ToString();
    }
    //Если не было сохранений
    private void Start()
    {
        if (SaveLevel.Load() == null)
            SaveLevel.SaveGameLevel(new DataSaveLevel(new saveData[] {
                new saveData {Star1=true,Star2=false,Star3=false,Unlock=false},
                new saveData {Star1=true,Star2=false,Star3=false,Unlock=true},
                new saveData {Star1=true,Star2=false,Star3=false,Unlock=true},
                new saveData {Star1=true,Star2=false,Star3=false,Unlock=true} }));
        DataSaveLevel save = SaveLevel.Load();
        Money = save.Money;
        if (unDestroyedBG == null)
            unDestroyedBG = myBG;
        else
            Destroy(myBG);
    }
    //Открыть выбор уровня
    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    //Открыть настройки
    public void Settings(bool on)
    {
        settings.SetActive(on);
    }
    public void Profile()
    {
        SceneManager.LoadScene("Profile");
    }
    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }
    //Выход из игры
    public void Exit()
    {
        Application.Quit();
    }
    public void WatchAdvert()
    {
        ActionRewardVideoCallback callback = new ActionRewardVideoCallback(()=> {
            DataSaveLevel save = SaveLevel.Load();
            save.Money += 30;
            SaveLevel.SaveGameLevel(save);
            Money = save.Money;
        });
        Appodeal.setRewardedVideoCallbacks(callback);
        Appodeal.show(Appodeal.REWARDED_VIDEO);
    }
}
