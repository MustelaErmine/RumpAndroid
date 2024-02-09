using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerBox : MonoBehaviour
{

    private HeroMove hero;


    private void Start()
    {

        hero = FindObjectOfType<HeroMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject == hero.gameObject)
        {
            transform.parent.gameObject.SetActive(false);
            hero.Overlap(collision.transform);
        }


    }
}
