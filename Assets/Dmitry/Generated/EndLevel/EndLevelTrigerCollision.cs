using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigerCollision : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == FindObjectOfType<HeroMove>().gameObject)
        {
            print("end1");
            FindObjectOfType<HeroMove>().EndGame();
        }
    }
}
