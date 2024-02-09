using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjParticle : MonoBehaviour
{
    public float timeDestroy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitDestroy());
    }

    IEnumerator waitDestroy()
    {
        yield return new WaitForSeconds(timeDestroy);
        Destroy(gameObject);
    }
}
