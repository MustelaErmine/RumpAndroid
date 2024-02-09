using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyItem : MonoBehaviour
{
    private HeroMove hero;
    private void Start()
    {
        hero = FindObjectOfType<HeroMove>();
    }
    //подбирание денег
    public void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            DataSaveLevel save = SaveLevel.Load();
            save.Money++;
            hero.collectedMoney++;
            SaveLevel.SaveGameLevel(save);
            Destroy(this.gameObject);
        }
    }
}
