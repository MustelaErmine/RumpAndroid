using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDestroyAllObject : MonoBehaviour
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
       
        if (hero.HeroAnim.GetBool("NotDead"))
        {
            
            if (collision.gameObject.tag == "Obj")
            {
            Debug.Log(collision.gameObject.tag);
            Destroy(collision.gameObject);
            }
        }
    }
}
