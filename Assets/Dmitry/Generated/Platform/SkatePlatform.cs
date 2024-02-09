using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkatePlatform : MonoBehaviour
{
    private HeroMove hero;
    private GeneratedPlatform generation;
    private void Start()
    {
        generation = FindObjectOfType<GeneratedPlatform>();
        hero = FindObjectOfType<HeroMove>();
        generation.DoOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
