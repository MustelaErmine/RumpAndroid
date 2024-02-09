using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public Image Star1Img, Star2Img, Star3Img;
    public Sprite StarTrue;
    public Sprite StarFalse;
    public Sprite Locked, Unlocked;
    public bool Star1
    {
        set
        {
            Star1Img.sprite = value ? StarTrue : StarFalse;
            star1 = value;
        }
        get => star1;
    }
    public bool Star2
    {
        set
        {
            Star2Img.sprite = value ? StarTrue : StarFalse;
            star2 = value;
        }
        get => star2;
    }
    public bool Star3
    {
        set
        {
            Star3Img.sprite = value ? StarTrue : StarFalse;
            star3 = value;
        }
        get => star3;
    }
    private bool star1, star2, star3;
    public bool Locke = false;
    public Text textLevel;
    public string levelName;
    public int numberlevel;
    AsyncOperation load;
    public GameObject LoadScreen;
    public Slider loadLevel;
    public Text progressText;
    private Button thisButton;


    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.002f);
        LoadScreen.SetActive(false);
        DataSaveLevel data = SaveLevel.Load();

        Star1 = data.Level[numberlevel - 1].Star1;
        Star2 = data.Level[numberlevel - 1].Star2;
        Star3 = data.Level[numberlevel - 1].Star3;
        Locke = data.Level[numberlevel - 1].Unlock;
        thisButton = transform.GetComponentInChildren<Button>();
        levelName = "Level" + numberlevel.ToString();
        if (Locke)
        {
            thisButton.image.sprite = Locked;
            Star1Img.color = Star2Img.color = Star3Img.color = new Color(0, 0, 0, 0);
        }
        else
        {
            thisButton.image.sprite = Unlocked;
            Star1Img.color = Star2Img.color = Star3Img.color = new Color(1, 1, 1, 1);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (load != null)
        {
            if (LoadScreen.activeSelf)
            {
                Debug.Log(load.progress);
                float progress = /*Mathf.Clamp01(*/load.progress/* / .9f)*/;
                loadLevel.value = progress;
                progressText.text = Mathf.CeilToInt(progress * 100) + "%";
            }
        }
        
        //textLevel.text = "Level" + numberlevel.ToString();
    }
    //Запуск первого уровня
    public IEnumerator OpenLevelCoroutine()
    {
        if (!Locke)
        {
            // SceneManager.LoadScene(textLevel.text);

            LoadScreen.SetActive(true);
            loadLevel.value = 0.1f;
            progressText.text = "10%";
            yield return new WaitForSeconds(3);
            load = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);

        }
    }
    public void OpenLevel()
    {
        StartCoroutine(OpenLevelCoroutine());
    }

}
