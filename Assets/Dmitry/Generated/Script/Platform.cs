using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform StartLocation;
    public Transform EndLocation;
    public GameObject cash;
    public Transform[] CactysGenerationPoint;
    public GameObject[] CactysGenerationObjects;
    public GameObject[] FloorObject;
    public Transform FloorObjPositions;
    private List<GameObject> GameObjSpaawm = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        int RandomPointCactus = Random.Range(0, CactysGenerationPoint.Length);
        int RandomObjCactus = Random.Range(0, CactysGenerationObjects.Length);
        for (int i = 0; i < CactysGenerationPoint.Length; i++)
        {
            Transform cactysSpawnPoint = CactysGenerationObjects[RandomObjCactus].GetComponent<GameObjGenerat>().spawnPoint;
            Vector3 posSpawnCactys = new Vector3(CactysGenerationPoint[RandomPointCactus].transform.position.x, CactysGenerationPoint[RandomPointCactus].transform.position.y - cactysSpawnPoint.transform.position.y, CactysGenerationPoint[RandomPointCactus].transform.position.z);
            GameObject cactys = Instantiate(CactysGenerationObjects[RandomObjCactus], posSpawnCactys, Quaternion.identity, transform);
            GameObjSpaawm.Add(cactys);
        }

        int randomObjFloor = Random.Range(0, FloorObject.Length-1);
        Transform posObjFloor = FloorObject[randomObjFloor].GetComponent<GameObjGenerat>().spawnPoint;
        if (FloorObjPositions != null)
        { 
            Vector3 posFloor = new Vector3(FloorObjPositions.transform.position.x, FloorObjPositions.transform.position.y - posObjFloor.transform.position.y, FloorObjPositions.transform.position.z);
        GameObject objFloor = Instantiate(FloorObject[randomObjFloor], posFloor, Quaternion.identity, transform);
        GameObjSpaawm.Add(objFloor);
        }
        float cashRandom = UnityEngine.Random.value;
        print(cashRandom);
        if (cashRandom > 0.5f)
        {
            cashRandom *= 2;
            Instantiate(cash, new Vector2(StartLocation.position.x + (EndLocation.position.x- StartLocation.position.x)*cashRandom,-2.5f),new Quaternion(),transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        foreach (var item in GameObjSpaawm)
        {
            Destroy(item);
            
        }
    }
}
