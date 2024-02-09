using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neyizmimost : MonoBehaviour
{
    private HeroMove hero;


    // Start is called before the first frame update
    void Start()
    {
        hero = FindObjectOfType<HeroMove>(); 
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == hero.gameObject)
        {
            hero.Neyizvim();
            Destroy(this.gameObject);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
