using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeEffect
{
    none,
    DownSpeed,
    UpSpeed,
    AddMony
}

public class ItemPicup : MonoBehaviour
{


    public GameObject objectParticle;
    public ParticleSystem FxPatricle;
    public bool UseFxParticle;
    public TypeEffect Mytype = TypeEffect.DownSpeed;
    public float destroyTimeDuration;
    ParticleSystem myFxParticle;
    GameObject ObjParticle;
    private HeroMove hero;

    private void Start()
    {
        hero = FindObjectOfType<HeroMove>();
    }
    //удаление обьекта со сцены
    public void DestroyItem()
    {
        //проверка на использование частиц или анимации
        if(UseFxParticle)
        {
            //создание эффекта
            myFxParticle = Instantiate(FxPatricle, gameObject.transform.position, Quaternion.identity);
            //удаление текушего обьекта
            Destroy(this.gameObject);
        }
        else
        {
            //создания обьекта с анимации
            ObjParticle = Instantiate(objectParticle, gameObject.transform.position, Quaternion.identity);
            //удаления анимации
            ObjParticle.GetComponent<ObjParticle>().timeDestroy = destroyTimeDuration;
            //тип эфекта
            hero.ItemEffects(Mytype);
            //удаление текушего обьекта
            Destroy(this.gameObject);
        }
    }




    
}
