using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeSpawnObj
{
    Cactys,
    obstacle,
    FlyPlatform
}
public class GameObjGenerat : MonoBehaviour
{
    public TypeSpawnObj TypeSpawn;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(15);
        Destroy(this.gameObject);
    }
}
