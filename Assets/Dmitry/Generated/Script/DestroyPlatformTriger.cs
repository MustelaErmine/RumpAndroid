using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatformTriger : MonoBehaviour
{
    private HeroMove hero;
    private GeneratedPlatform generation;
    private void Start()
    {
        generation = FindObjectOfType<GeneratedPlatform>();
        hero = FindObjectOfType<HeroMove>();
    }
    // Start is called before the first frame update
    //пересечение с тригером
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //проверка на столкновения с персонажем
        if (collision.gameObject == hero.gameObject)
        {
            generation.OnDestroyAndCreate();
            Destroy(this.gameObject);
            //вызов метода дестрой в классе персонажа
        }
    
    }
}
