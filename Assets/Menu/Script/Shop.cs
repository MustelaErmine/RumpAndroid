using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Text MoneyS;
    public Text Money;
    public static DataSaveLevel save = null;
    public RectTransform content;
    public Scrollbar scrollbar;
    public float y1;
    public float y2;
    public Text JetpackCost, JetpackAmount;
    public Button ClownBuy, SpaceXBuy;
    byte jetpackAmount = 3;
    public static uint money
    {
        set 
        { 
            MoneyS.text = value.ToString();
            save.Money = value;
            SaveLevel.SaveGameLevel(save);
        }
    }
    private void Awake()
    {
        save = SaveLevel.Load();
    }
    void Start()
    {
        MoneyS = Money;
        money = save.Money;
        if (save.BoughtCostumes.ToList().Contains(1))
        {
            ClownBuy.enabled = false;
            ClownBuy.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        }
        if (save.BoughtCostumes.ToList().Contains(2))
        {
            SpaceXBuy.enabled = false;
            SpaceXBuy.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
    }
    public void OnChangedScroll(float s)
    {
        content.localPosition = new Vector2(0, y1 + (y2 - y1) * scrollbar.value);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Buy (string id)
    {
        InitializeServices.iapmanager.InitializePurchase(id);
        print($"Buy {id}");
    }
    public void BuyJetpacks ()
    {
        Buy($"jetpack_{jetpackAmount}");
    }
    public void PlusJetpack ()
    {
        if (jetpackAmount == 3)
            jetpackAmount = 6;
        else if (jetpackAmount == 6)
            jetpackAmount = 9;
        JetpackCost.text = $"{jetpackAmount*35},00 ₽";
        JetpackAmount.text = $"x{jetpackAmount}";
    }
    public void MinusJetpack ()
    {
        if (jetpackAmount == 9)
            jetpackAmount = 6;
        else if (jetpackAmount == 6)
            jetpackAmount = 3;
        JetpackCost.text = $"{jetpackAmount * 35},00 ₽";
        JetpackAmount.text = $"x{jetpackAmount}";
    }
}
