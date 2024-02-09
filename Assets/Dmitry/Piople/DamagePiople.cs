using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamagePiople : MonoBehaviour
{
    HeroMove hero;
    public ParticleSystem particleDestroy;
    private Rigidbody2D rigidbody2d;
    private Vector2 normalPosition;
    private Transform transfor;
    public byte distance
    {
        set
        {
            if (value < 0 || value > 10)
                throw new ArgumentException();
            normalPosition = 
                new Vector2(startDistance + (10-value) * 0.1f * (finishDistance - startDistance), transform.position.y);
            _distance = value;
        }
        get => _distance;
    }
    private byte _distance = 10;
    private float startDistance = 0f;
    private float finishDistance = 11f;
    // Start is called before the first frame update
    void Start()
    {
        hero = FindObjectOfType<HeroMove>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        transfor = transform;
        distance = 10;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        /*if(!hero.isJump)
        {
            transform.position += derection * Time.deltaTime * 7;
        }
        else
        {
            transform.position += derection * Time.deltaTime * 10;
        }*/
        if (distance != 0)
            rigidbody2d.velocity = new Vector2((normalPosition.x - transfor.position.x) * 1f, rigidbody2d.velocity.y);
        else
            rigidbody2d.velocity = new Vector2(0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obj")
        {
            Destroy(collision.gameObject);
            ParticleSystem part = Instantiate(particleDestroy, collision.transform.position, Quaternion.identity);
            part.transform.localScale.Scale(new Vector3(30, 30, 30));
        }
    }
}
