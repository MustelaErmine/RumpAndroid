using System.Linq;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;

public class IAPManager : IStoreListener
{

    private IStoreController controller;
    private IExtensionProvider extensions;

    public IAPManager()
    {
        ConfigurationBuilder builder = 
            ConfigurationBuilder.Instance(Google.Play.Billing.GooglePlayStoreModule.Instance());
        builder.AddProduct("jetpack_3", ProductType.Consumable);
        builder.AddProduct("jetpack_6", ProductType.Consumable);
        builder.AddProduct("jetpack_9", ProductType.Consumable);
        builder.AddProduct("clown_costume2", ProductType.NonConsumable);
        builder.AddProduct("spacex_costume2", ProductType.NonConsumable);
        
        UnityPurchasing.Initialize(this, builder);
    }

    /// <summary>
    /// Called when Unity IAP is ready to make purchases.
    /// </summary>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;
    }

    /// <summary>
    /// Called when Unity IAP encounters an unrecoverable initialization error.
    ///
    /// Note that this will not be called if Internet is unavailable; Unity IAP
    /// will attempt initialization until it becomes available.
    /// </summary>
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("InitializeFailed");
        switch (error)
        {
            case InitializationFailureReason.AppNotKnown:
                Debug.Log("AppNotKnown");
                break;
            case InitializationFailureReason.NoProductsAvailable:
                Debug.Log("NoProductsAvailable");
                break;
            case InitializationFailureReason.PurchasingUnavailable:
                Debug.Log("PurchasingUnavailable");
                break;
        }
    }

    /// <summary>
    /// Called when a purchase completes.
    ///
    /// May be called at any time after OnInitialized().
    /// </summary>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        Debug.Log("success " + e.purchasedProduct.definition.id);
        DataSaveLevel level = SaveLevel.Load();

        switch (e.purchasedProduct.definition.id)
        {
            case "jetpack_3":
                level.BoughtBoosters = 
                    level.BoughtBoosters.Concat(new byte[]{0, 0, 0}).ToArray();
                break;
            case "jetpack_6":
                level.BoughtBoosters = 
                    level.BoughtBoosters.Concat(new byte[]{0, 0, 0, 0, 0, 0}).ToArray();
                break;
            case "jetpack_9":
                level.BoughtBoosters = 
                    level.BoughtBoosters.Concat(new byte[]{0, 0, 0, 0, 0, 0, 0, 0, 0}).ToArray();
                break;
            case "clown_costume2":
                SceneManager.LoadScene("Shop");
                level.BoughtCostumes = level.BoughtCostumes.Append<byte>(1).ToArray();
                break;
            case "spacex_costume2":
                SceneManager.LoadScene("Shop");
                level.BoughtCostumes = level.BoughtCostumes.Append<byte>(2).ToArray();
                break;
            default:
                Debug.Log("oshibka");
                break;
        }
        SaveLevel.SaveGameLevel(level);
        return PurchaseProcessingResult.Complete;
    }

    /// <summary>
    /// Called when a purchase fails.
    /// </summary>
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log("failed " + i.definition.id);
        string s = "";
        switch (p)
        {
            case PurchaseFailureReason.DuplicateTransaction:
                s = "DuplicateTransaction";
                break;
            case PurchaseFailureReason.ExistingPurchasePending:
                s = "ExistingPurchasePending";
                break;
            case PurchaseFailureReason.PaymentDeclined:
                s = "PaymentDeclined";
                break;
            case PurchaseFailureReason.ProductUnavailable:
                s = "ProductUnavailable";
                break;
            case PurchaseFailureReason.PurchasingUnavailable:
                s = "PurchasingUnavailable";
                break;
            case PurchaseFailureReason.SignatureInvalid:
                s = "SignatureInvalid";
                break;
            case PurchaseFailureReason.Unknown:
                s = "Unknown";
                break;
            case PurchaseFailureReason.UserCancelled:
                s = "UserCancelled";
                break;
        }
        Debug.Log(s);
    }
    public void InitializePurchase(string productId)
    {
        controller.InitiatePurchase(productId);
    }
}