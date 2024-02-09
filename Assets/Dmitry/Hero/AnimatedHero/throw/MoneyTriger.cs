using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTriger : MonoBehaviour
{
    private HeroMove hero;
    public ParticleSystem particleDestroy;
    public Text moneyPoint;
    private int moneyColvo;
    //удаление через время
    private void Start()
    {
        hero = FindObjectOfType<HeroMove>();
        moneyPoint = GetComponent<HeroMove>().moneyPoint;
        StartCoroutine(WaitDestroy());
    }
    //пермещение самого объекта по оси х
    private void Update()
    {
        float speed = 25;
        Vector3 derection = new Vector3(1, 0, 0);
        //Перемешение вперед денег
        transform.position += derection * Time.deltaTime * speed;
    }

    //удаление при соприкосновении с объектами
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obj")
        {
            //moneyColvo -= 1;
            Destroy(collision.gameObject);
            ParticleSystem part = Instantiate(particleDestroy, collision.transform.position, Quaternion.identity);
            part.transform.localScale.Scale(new Vector3(30, 30, 30));
            moneyColvo = int.Parse(moneyPoint.text);
             //Destroy(this.gameObject);
            moneyColvo -= 1;
            moneyPoint.text = moneyColvo.ToString();
            Destroy(this.gameObject);
        }
        StartCoroutine(WaitDestroy());

    }
    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(3);
        moneyColvo = int.Parse(moneyPoint.text);
         //Destroy(this.gameObject);
        moneyColvo -= 1;
        moneyPoint.text = moneyColvo.ToString();
        Destroy(this.gameObject);
    }
}
