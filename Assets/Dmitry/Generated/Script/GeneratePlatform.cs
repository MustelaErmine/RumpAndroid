using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GeneratePlatform : MonoBehaviour
{
    public GameObject[] Platform;
    public GameObject[] PlatformStep2;
    public GameObject[] PlatformStep3;


    public GameObject Finish;
    public GameObject Step1;
    public int LengeCreate;
    public GameObject ScatePlatform;

    public GameObject[] AllPlatform;
    public GameObject[] Props;
    public GameObject[] StartAllPlatform;

    Vector3 StartCreateLocation;
    Vector3 EndLocationPlatform;
    float OverlapPlatforCount;
    public float FinishGenerateIndex;
    int platformDestroy;
    private int CounterDestroy;
    public bool DoOnce = true;

    //widget
    public Slider slid;

    void Start()
    {
        platformDestroy = 5;
        AllPlatform = new GameObject[LengeCreate];

        for (int i=0; i < LengeCreate; i++)
        {
            //случайное число для выбора случайной платформы и ее генерации
            //число определяется от 0 до длины генерации указанной с длины Масива Platform[]
            int RandomPlatform = 0;
            if(i > 0)
            {
                EndLocationPlatform = AllPlatform[i - 1].GetComponent<Platform>().EndLocation.transform.position;
                StartCreateLocation = StartAllPlatform[RandomPlatform].GetComponent<Platform>().StartLocation.transform.position;
            }
            else
            {
                //StartCreateLocation = new Vector3(0, 0, 0);
                //EndLocationPlatform = new Vector3(0, 0, 0);
            }

            GameObject NewPlatform = Instantiate(StartAllPlatform[RandomPlatform], EndLocationPlatform - StartCreateLocation, Quaternion.identity);
            AllPlatform[i] = NewPlatform;
        }
    }

    
    void Update()
    {
        slide();
    }

    public void OnDestroyAndCreate()
    {

        if (OverlapPlatforCount == LengeCreate - 2)
        {
            if (platformDestroy == FinishGenerateIndex)
            {
                GenerateNewPlatform(Finish);
            }
            else {
                if (platformDestroy == FinishGenerateIndex / 3)
                {
                    GenerateNewPlatform(Step1);
                }
                else if (platformDestroy >= (FinishGenerateIndex / 3) + 1 && platformDestroy <= FinishGenerateIndex - (FinishGenerateIndex / 3))
                {
                    int RandomPlatform = Random.Range(0, PlatformStep2.Length);
                    GenerateNewPlatform(PlatformStep2[RandomPlatform]);
                    Debug.Log("generated2");
                }
                else
                {

                    if (platformDestroy > FinishGenerateIndex - (FinishGenerateIndex / 3))
                    {
                        if (DoOnce)
                        {
                            Debug.Log("generatedSpecual");
                            GenerateNewPlatform(ScatePlatform);
                            
                        }
                        else
                        {
                            int RandomPlatform = Random.Range(0, PlatformStep3.Length);
                            GenerateNewPlatform(PlatformStep3[RandomPlatform]);
                            Debug.Log("generated");
                        }
                    }
                    else
                    {
                        int RandomPlatform = Random.Range(0, Platform.Length);
                        GenerateNewPlatform(Platform[RandomPlatform]);
                    }
                }
            }
        }
        else
        {
            OverlapPlatforCount += 1;
        }
    }





    void GenerateNewPlatform(GameObject platform)
    {
        if (CounterDestroy == 0)
        {
            StartCoroutine(waitDestroy(platform));
            CounterDestroy = 1;
        }
        else
        {
            CounterDestroy = 0;
        }
    }

    IEnumerator waitDestroy(GameObject platform)
    {
        yield return new WaitForSeconds(1);
        platformDestroy += 1;
        Destroy(AllPlatform[0]);
        for (int i = 0; i < AllPlatform.Length; i++)
        {
            if (i > 0)
            {
                AllPlatform[i - 1] = AllPlatform[i];
            }
        }
        AllPlatform[AllPlatform.Length - 1] = null;
        EndLocationPlatform = AllPlatform[AllPlatform.Length - 2].GetComponent<Platform>().EndLocation.transform.position;
        StartCreateLocation = platform.GetComponent<Platform>().StartLocation.transform.position;
        GameObject NewPlatform = Instantiate(platform, EndLocationPlatform - StartCreateLocation, Quaternion.identity);
        AllPlatform[AllPlatform.Length - 1] = NewPlatform;
    }

    void slide()
    {
        slid.value = (platformDestroy  - 3 )/ FinishGenerateIndex;
        
    }
}
