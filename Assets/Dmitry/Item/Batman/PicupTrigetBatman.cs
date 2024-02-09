using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicupTrigetBatman : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Glaider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == FindObjectOfType<HeroMove>().gameObject)
        {
            FindObjectOfType<HeroMove>().BatManUse();
            Destroy(Glaider);
        }
    }
}
