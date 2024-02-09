using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomateItem : MonoBehaviour
{
    private HeroMove hero;
    // Start is called before the first frame update
    void Start()
    {
        hero = FindObjectOfType<HeroMove>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == hero.gameObject)
        {
            hero.TomatesItem();
        }
    }
}
