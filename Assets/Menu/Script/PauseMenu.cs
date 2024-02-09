using AppodealAds.Unity.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuObj;
    public GameObject LoseMenu;
    public GameObject Tutorial;
    private AsyncOperation load;
    private HeroMove hero;

    // Start is called before the first frame update
    void Start()
    {
        hero = FindObjectOfType<HeroMove>();
        hero.LoseMenu = LoseMenu;
        LoseMenu.SetActive(false);
        PauseMenuObj.SetActive(false);
    }

    // Update is called once per frame
    public void activePause()
    {
        PauseMenuObj.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        PauseMenuObj.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ResumeByAdvert()
    {
#if UNITY_EDITOR
        hero.ResumeGame();
#else
        ActionRewardVideoCallback callback = new ActionRewardVideoCallback(hero.ResumeGame);
        Appodeal.setRewardedVideoCallbacks(callback);
        Appodeal.show(Appodeal.REWARDED_VIDEO);
#endif
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        LoseMenu.SetActive(false);
        PauseMenuObj.SetActive(false);
        load = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelMenu()
    {
        
        PauseMenuObj.SetActive(false);
        SceneManager.LoadSceneAsync("LevelSelect");
        Time.timeScale = 1f;
    }
    public void CloseTutorial()
    {
        Time.timeScale = 1;
        Tutorial.SetActive(false);
    }
}
