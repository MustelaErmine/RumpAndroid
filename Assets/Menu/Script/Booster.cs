using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Booster : MonoBehaviour
{
    public byte NumberOfBooster;
    public uint price;
    private void Start()
    {
        transform.GetChild(1).GetComponent<Text>().text = price.ToString();
    }
    public void BuyBooster()
    {
        if (Shop.save.Money >= price)
        {
            Shop.money = Shop.save.Money - price;
            List<byte> boosts = new List<byte>(Shop.save.BoughtBoosters);

            boosts.Add(NumberOfBooster);
            byte[] myBoosts = boosts.ToArray();
            Shop.save.BoughtBoosters = myBoosts;
            SaveLevel.SaveGameLevel(Shop.save);
        }
    }
    public void SelectBooster()
    {
        Shop.save.SelectedBooster = NumberOfBooster;
        SaveLevel.SaveGameLevel(Shop.save);
    }
}

