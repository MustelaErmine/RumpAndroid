using AppodealAds.Unity.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;



public class HeroMove : MonoBehaviour
{

    public float speed;
    private float NormallySpeed;
    private bool isSpeedNormally = true;
    public Animator HeroAnim;
    public Transform jumpPoint, trans;
    public LayerMask Ground;
    public bool isGround;
    private AudioSource Audio;
    public AudioClip run;
    private float timeFottStep;
    public float timeFootStepDelay;
    private bool usedAdvert = false;
    private bool endGame = false;

    public bool canJump = true;
    public LayerMask ObjMask;
    public GameObject LoseMenu;
    public GameObject overlapEffect;
    private Rigidbody2D rigidbody2d;
    bool tomat = false;
    public float JumpVelocity = 7;
    private Rigidbody2D bodys;
    public GameObject moneyAttackSpawn;//объект деньги который кидаем
    public Transform SpawnMoney;//точка спауна денег
    private GeneratedPlatform generate;
    private DamagePiople piople;
    public List<Action> boosters;
    private GameObject starEffect;
    public byte[] myBoosts;
    private byte selectedBooster;
    public GameObject damagePoint;
    Vector3 distance;
    public short collectedMoney = 0;
    public byte levelNum = 1;
    public float difficulty = 1;
    string[] costumes = {"NormalCostume", "ClownCostume" , "SpaceXCostume" };
    public AudioClip steps, crowd, jetpack, jump, slide, end;
    public GameObject WinGame;
    bool DownSpeedUse = true;
    Camera cam;


    void Start()
    {
        //Ишем аниматор у персонажа
        HeroAnim = transform.GetChild(4).GetComponent<Animator>();
        //Ишем камеру на сцене
        WinGame.SetActive(false);
        timeFottStep = timeFootStepDelay;
        bodys = GetComponent<Rigidbody2D>();
        Audio = GetComponent<AudioSource>();
        //moneyPoint = GetComponent<MoneyItem>().moneyPoint;
        generate = FindObjectOfType<GeneratedPlatform>();
        NormallySpeed = speed;
        cam = FindObjectOfType<Camera>();
        DataSaveLevel save = SaveLevel.Load();
        boosters = new List<Action> { Jetpack };
        if (save.First)
        {
            Time.timeScale = 0;
            save.First = false;
            SaveLevel.SaveGameLevel(save);
            FindObjectOfType<PauseMenu>().Tutorial.SetActive(true);
        }
        else
           FindObjectOfType<PauseMenu>().Tutorial.SetActive(false);
        myBoosts = save.BoughtBoosters;
        selectedBooster = save.SelectedBooster;
        rigidbody2d = GetComponent<Rigidbody2D>();
        trans = transform;
        // Difficulty
        saveData level = save.Level[levelNum - 1];
        if (level.Star3)
            difficulty = 1.44f;
        else if (level.Star2)
            difficulty = 1.2f;
        else
            difficulty = 1;
        Time.timeScale = difficulty;
        foreach (string s in costumes)
        {
            if (s == costumes[save.SelectedCostume])
                HeroAnim.SetBool(s, true);
            else
                HeroAnim.SetBool(s, false);
        }

        //Audio
        GameObject bm = GameObject.Find("BackgroundMusic");
        if (bm != null)
            bm.GetComponent<AudioSource>().priority = 129;
    }
    private void FixedUpdate()
    {
        piople = FindObjectOfType<DamagePiople>();
        distance = transform.position - piople.gameObject.transform.position;

        Vector3 derection = new Vector3(1, 0, 0);
        //Перемешение в перед
        /*transform.position += derection * Time.deltaTime * speed;*/
        foreach (GameObject p in generate.AllPlatform.Append(generate.wall).Append(starEffect))
            if (p != null)
                p.transform.Translate(-derection * Time.deltaTime * speed);
        foreach (GameObject p in generate.Props)
            p.transform.Translate(-derection * Time.deltaTime * speed * 0.05f);
        //Анимация бега/спокойствие
        HeroAnim.SetInteger("Speed", (int)speed);

        //Колизия с землей по Слою "Ground"
        isGround = Physics2D.OverlapCircle(jumpPoint.position, 0.3f, Ground + ObjMask);
        //переключение анимации падения
        HeroAnim.SetBool("Falen", !isGround);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Neyizvim();
        }

        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 4.1f, -12);
        //AttackHeroAnim(false);

        //Normal Speed change
        NormallySpeed = 10 + 10 * generate.slid.value;
        JumpVelocity = 8 + 5 * generate.slid.value;
        Physics2D.gravity = new Vector2(0, -9.81f - 9.81f * generate.slid.value * 2);
        if (isSpeedNormally)
            speed = NormallySpeed;
    }


    public void ItemEffects(TypeEffect effect)
    {
        if (effect == TypeEffect.DownSpeed)
        {
            if (speed == 10)
            {
                //if()
                isSpeedNormally = false;
                speed -= 0.3f;
                StartCoroutine(DestroyItemEffect());
            }
        }
    }


    IEnumerator DestroyItemEffect()
    {
        yield return new WaitForSeconds(5);
        isSpeedNormally = true;
    }


    public void TomatesItem()
    {
        if (!HeroAnim.GetBool("NotDead"))
        {
            tomat = true;
            if (HeroAnim.GetBool("Podkat"))
            {
                StartCoroutine(waitTomatesSpeedUp());
            }
            else
            {

                HeroAnim.SetBool("Padenie", true);
                StartCoroutine(waitTomates());
            }
        }
    }
    IEnumerator waitTomates()
    {
        float texspeed = speed;
        isSpeedNormally = false;
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < texspeed; i++)
        {
            yield return new WaitForSeconds(0.03f);
            if (speed > 0)
            {
                speed -= 1;
            }
            else
            {
                break;
            }
        }
        HeroAnim.SetBool("Padenie", false);

        isSpeedNormally = true;
        tomat = false;
    }
    IEnumerator waitTomatesSpeedUp()
    {
        isSpeedNormally = false;
        for (int i = 0; i < 18; i++)
        {
            yield return new WaitForSeconds(0.03f);
            if (speed < 18)
            {
                speed += 1;
            }
            else
            {
                break;
            }
        }
        HeroAnim.SetBool("Padenie", false);
        isSpeedNormally = true;

    }




    public void Jump()
    {
        if (!HeroAnim.GetBool("NotDead"))
        {
            if (isGround == true)
            {
                if (!HeroAnim.GetBool("Padenie"))
                {

                    if (canJump)
                    {
                        HeroAnim.SetBool("Falen", true);
                        canJump = false;
                        bodys.AddForce(new Vector2(0, JumpVelocity), ForceMode2D.Impulse);
                        if (UnityEngine.Random.value < 0.5f)
                        {
                            print("jump");
                            HeroAnim.SetBool("Jump", true);
                        }
                        else
                        {
                            HeroAnim.SetBool("Jump2", true);
                            print("jump2");
                        }
                        StartCoroutine(PlayOnce(jump));
                        StartCoroutine(FinisJump());
                    }
                }
            }
        }
    }
    IEnumerator FinisJump()
    {
        yield return new WaitForSeconds(0.5f);
        HeroAnim.SetBool("Jump", false);
        HeroAnim.SetBool("Jump2", false);
        canJump = true;
    }

    public void crounch()
    {
        if (!HeroAnim.GetBool("NotDead"))
        {
            Debug.Log("Crounch");
            StartCoroutine(PlayOnce(slide));
            HeroAnim.SetBool("Podkat", true);
            StartCoroutine(waitCrounch());
            GetComponent<CapsuleCollider2D>().offset = new Vector2(0f, -1.34f);
            GetComponent<CapsuleCollider2D>().size = new Vector2(2.09f, 2f);
        }
    }

    IEnumerator waitCrounch()
    {
        if (!HeroAnim.GetBool("NotDead"))
        {
            yield return new WaitForSeconds(0.45f);
            //GetComponent<CapsuleCollider2D>().offset = new Vector2(0f, -0.61f);
            //GetComponent<CapsuleCollider2D>().size = new Vector2(2.09f, 2f);
            yield return new WaitForSeconds(0.85f);
            HeroAnim.SetBool("Podkat", false);
            GetComponent<CapsuleCollider2D>().offset = new Vector2(0f, 0f);
            GetComponent<CapsuleCollider2D>().size = new Vector2(2.09f, 5f);
        }
    }



    public void Neyizvim()
    {
        HeroAnim.SetBool("NotDead", true);
        Debug.Log("neyizvim");
        StartCoroutine(NeyezvimDelay());
    }

    IEnumerator NeyezvimDelay()
    {
        yield return new WaitForSeconds(5);
        HeroAnim.SetBool("NotDead", false);
    }

    public void Overlap(Transform point)
    {
        /*if (!HeroAnim.GetBool("NotDead"))
        {
            isSpeedNormally = false;
            GameObject starEffect = Instantiate(overlapEffect, new Vector3(point.position.x, point.position.y + 2, point.position.z - 1), Quaternion.identity);
            speed = 0;
            bodys.AddForce(new Vector2(-5f, 3), ForceMode2D.Impulse);
            StartCoroutine(StarEffectDestroy(starEffect));
            HeroAnim.SetBool("Padenie", true);

        }*/
        //GameObject starEffect = Instantiate(overlapEffect, new Vector3(point.position.x, point.position.y + 2, point.position.z - 1), Quaternion.identity);
        GameObject starEffect = Instantiate(overlapEffect, damagePoint.transform.position, damagePoint.transform.rotation);
        this.starEffect = starEffect;
        StartCoroutine(StarEffectDestroy(starEffect));
        piople.distance--;
        if (piople.distance == 0)
            StartCoroutine(LoseGame());
        print("overlap");
    }
    IEnumerator StarEffectDestroy(GameObject star)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(star);
    }
    IEnumerator EndPodenie()
    {
        yield return new WaitForSeconds(2f);
        HeroAnim.SetBool("Padenie", false);
        isSpeedNormally = true;
    }

    public IEnumerator LoseGame()
    {
        Time.timeScale = 1;
        isSpeedNormally = false;
        endGame = true;
        speed = 0;
        StartCoroutine(PlayOnce(end, true));
        Audio.mute = true;
        HeroAnim.SetBool("Lose", true);
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
        LoseMenu.SetActive(true);
        DataSaveLevel save = SaveLevel.Load();
        GameObject.Find("Canvas/LoseMenu/Image/Money").GetComponent<Text>().text = collectedMoney.ToString();
        collectedMoney = 0;
        save.Loses++;
        if (save.Loses == 4)
        {
            save.Loses = 0;
#if !UNITY_EDITOR
            Appodeal.show(Appodeal.INTERSTITIAL);
#else
            print("INTERSTITIAL");
#endif
        }
        SaveLevel.SaveGameLevel(save);
        if (!usedAdvert) 
        {
            usedAdvert = true;
        }
        else
        {
            LoseMenu.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
    }
    public void ResumeGame()
    {
        piople.distance = 10;
        isSpeedNormally = true;
        HeroAnim.SetBool("Lose", false);
        
        LoseMenu.SetActive(false);
        Time.timeScale = difficulty;
        endGame = false;
        Audio.mute = false;
    }
    //кидание денег 
    public Text moneyPoint;
    private int moneyColvo;
    /*public void AttackHeroAnim(bool use)
    {
        moneyColvo = int.Parse(moneyPoint.text);
        if (HeroAnim.GetBool("Podkat") == false && HeroAnim.GetBool("Padenie") == false)
        { 
            if (Input.GetKeyDown(KeyCode.Mouse0) && moneyColvo>0)// 
            {
                HeroAnim.SetBool("AttackHeroAnim", true);
                Debug.Log("кинул!");
                Instantiate(moneyAttackSpawn, SpawnMoney.transform.position, Quaternion.identity);
                HeroAnim.Play("Run");
                moneyColvo -= 1;
                moneyPoint.text = moneyColvo.ToString();
                StartCoroutine(waitAttack());
            }
            else
            {
                
            }
        }
    }
    */
    IEnumerator waitAttack()
    {
        yield return new WaitForSeconds(0.5f);
        HeroAnim.SetBool("AttackHeroAnim", false);
    }


    public void EndGame()
    {
        FindObjectOfType<HeroMove>().speed = 0;
        endGame = true;
        Time.timeScale = 0;
        WinGame.SetActive(true);
        Audio.mute = true;
        StartCoroutine(PlayOnce(end, true));
    }

    public void skate()
    {
        isSpeedNormally = false;
        speed = 13;
        HeroAnim.SetBool("Podkat", false);
        HeroAnim.SetBool("AttackHeroAnim", false);
        HeroAnim.SetBool("NotDead", false);
        HeroAnim.SetBool("Padenie", false);
        HeroAnim.SetBool("Skate", true);
        DownSpeedUse = false;
        StartCoroutine(waitDestoySkate());
    }
    IEnumerator waitDestoySkate()
    {

        yield return new WaitForSeconds(2);
        HeroAnim.SetBool("Skate", false);
        yield return new WaitForSeconds(0.5f);
        isSpeedNormally = true;
    }


    public void BatManUse()
    {
        isSpeedNormally = false;
        speed = 13;
        DownSpeedUse = false;
        bodys.gravityScale = 0.5f;
        HeroAnim.SetBool("BatMan", true);
        bodys.AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
        StartCoroutine(waitBatMan());
    }
    IEnumerator waitBatMan()
    {

        yield return new WaitForSeconds(2.5f);
        HeroAnim.SetBool("BatMan", false);
        isSpeedNormally = true;
        bodys.gravityScale = 1;
    }

    public void DownSpeed()
    {

        if (DownSpeedUse && speed > 8 && distance.x > 7.3f)
        {
            StartCoroutine(WaitDownSpeed());
        }
    }
    public void useBoost()
    {
#if UNITY_EDITOR
        boosters[selectedBooster]();
#else
        List<byte> boosts = new List<byte>(myBoosts);
        if (boosts.Contains(selectedBooster))
        {
            boosts.Remove(selectedBooster);
            boosters[selectedBooster]();

            myBoosts = boosts.ToArray();
            DataSaveLevel save = SaveLevel.Load();
            save.BoughtBoosters = myBoosts;
            SaveLevel.SaveGameLevel(save);
        }
#endif
    }

    IEnumerator WaitDownSpeed()
    {

        yield return new WaitForSeconds(0.8f);
        if (DownSpeedUse)
        {
            speed -= 1;
            StartCoroutine(WaitDownSpeed());
        }
    }
    public void Jetpack()
    {
        print("use jetpack!");
        StartCoroutine(JetpackCoroutine());
    }
    public IEnumerator JetpackCoroutine()
    {
        StartCoroutine(PlayOnce(jetpack));
        HeroAnim.SetBool("Jetpack", true);
        rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
        float a = transform.position.y, b = 2f;
        for (float t1 = Time.time, t2 = Time.time; t2 - t1 <= 1; t2 = Time.time)
        {
            //yield return new WaitForSeconds(0.001f);
            yield return new WaitForFixedUpdate();
            trans.position = new Vector2(trans.position.x, Mathf.Lerp(a, b, t2-t1));
        }
        yield return new WaitForSeconds(3);
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        HeroAnim.SetBool("Jetpack", false);
    }
    public IEnumerator PlayOnce(AudioClip clip, bool b = false)
    {
        AudioSource s = gameObject.AddComponent<AudioSource>();
        if (!b)
            Audio.mute = true;
        s.PlayOneShot(clip);
        yield return new WaitForSecondsRealtime(clip.length);
        if (!b && !endGame)
        {
            Audio.mute = false;
            print("end3" + clip.name);
        }
        Destroy(s);
    }
}
