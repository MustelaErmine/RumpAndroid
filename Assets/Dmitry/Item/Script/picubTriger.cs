using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class picubTriger : MonoBehaviour
{
    private ItemPicup picup;
    private HeroMove hero;
    public AudioClip clip;
    private AudioSource source;

    private void Start()
    {
        //Поиск экземпляра обьекта персонажа
        hero = FindObjectOfType<HeroMove>();
        //Получение экземпляра обьекта Item
        picup = GetComponent<ItemPicup>();     
        //Получение звукового класа воспроизведения
        source = GetComponent<AudioSource>();
        
    }
    //пересечение с тригером
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //проверка на сопрекосновение с персонажем
        if(collision.gameObject == hero.gameObject)
        {
            //проигрование звука
            playSounds();
            //picup.DestroyItem();
            //запуск задержки
            StartCoroutine(waitDestroy());
        }
        
    }
    
    //проигрования звуков
    void playSounds()
    {
        source.PlayOneShot(clip);
    }
    // задержка и удаление обьекта  
    IEnumerator waitDestroy()
    {
        //задержка перед дестроем
        yield return new WaitForSeconds(0.2f);
        //Запуск метода удаления объекта
        picup.DestroyItem();
    }
}
