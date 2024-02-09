using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllPause : MonoBehaviour
{
    public GameObject Menu;
    public GameObject TrigerEndGame;
    private bool pause;

    void Start()
    {
        Menu.SetActive(false);

    }

    public void Pause()
    {
        if (!pause)
        {
            Time.timeScale = 0f; pause = true;
        }
        else
        {
            Time.timeScale = 1f; pause = false;
        }
        Menu.SetActive(pause);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1f;
    }
    public void MenuOpen()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
    public void Next()
    {
        Time.timeScale = 1f;
        pause = false;
        Menu.SetActive(false);
    }
    void Update()
    {

    }
}
