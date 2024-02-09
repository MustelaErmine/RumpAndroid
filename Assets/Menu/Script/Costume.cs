using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Costume : MonoBehaviour
{
    public byte NumberOfCostume;
    public uint price;
    public Sprite bought;
    public string[] costumes;
    public Sprite unBought;
    private void Start()
    {
        transform.GetChild(1).GetComponent<Text>().text = price.ToString();
        Button button = GetComponentInChildren<Button>();
        if (Shop.save.BoughtCostumes.Contains(NumberOfCostume))
        {
            button.image.sprite = bought;
            button.interactable = false;
        }
    }
    public void BuyCostume()
    {
        if (Shop.save.Money >= price)
        {
            Shop.money = Shop.save.Money - price;
            List<byte> boosts = new List<byte>(Shop.save.BoughtCostumes);

            boosts.Add(NumberOfCostume);
            byte[] myBoosts = boosts.ToArray();
            Shop.save.BoughtCostumes = myBoosts;
            SaveLevel.SaveGameLevel(Shop.save);
            Button button = GetComponentInChildren<Button>();
            button.image.sprite = bought;
            button.interactable = false;
        }
    }
    public void SelectCostume()
    {
        Animator dance = GameObject.Find("Canvas/GG").GetComponent<Animator>();
        foreach (string s in costumes)
        {
            if (s == costumes[NumberOfCostume])
                dance.SetBool(s, true);
            else
                dance.SetBool(s, false);
        }
        Shop.save.SelectedCostume = NumberOfCostume;
        SaveLevel.SaveGameLevel(Shop.save);
    }
}

