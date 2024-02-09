using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controll : MonoBehaviour
{
    // Start is called before the first frame update
    public SwipeController Controller;
    private HeroMove hero;
    // Start is called before the first frame update
    void Start()
    {
        hero = FindObjectOfType<HeroMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.backSwipe)
        {
            Controller.backSwipe = false;
            hero.crounch();
        }
        if (Controller.tap)
        {
            Controller.tap = false;
            hero.useBoost();
            print("tap");
        }
        if (Controller.upSwipe)
        {
            Controller.upSwipe = false;
            hero.Jump();
        }
    }
}
