//#define Testing // Uncomment for testing
using UnityEngine;
using AppodealAds.Unity.Api;
using System.Linq;

public class InitializeServices : MonoBehaviour
{
    public string APP_KEY = "paraparapam";
    public static bool isInitialized;
    public GameObject[] dontDestroy;
    public static IAPManager iapmanager;
    void Start()
    {
        if (isInitialized) 
            return;
        isInitialized = true;
#if Testing
        Appodeal.setTesting(true);
#endif
        Appodeal.initialize(APP_KEY,Appodeal.REWARDED_VIDEO|Appodeal.INTERSTITIAL);

        dontDestroy.ToList().ForEach((GameObject g) => { DontDestroyOnLoad(g); });
        print(isInitialized);
        iapmanager = new IAPManager();

        //IngameDebugConsole.DebugLogConsole.AddCommand("win","",()=> { GameObject.FindObjectOfType<HeroMove>().EndGame(); });
    }
}
