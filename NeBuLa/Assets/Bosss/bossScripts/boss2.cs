using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2 : MonoBehaviour
{
    [SerializeField] private GameObject mermi;
    [SerializeField] private GameObject lazer;
    [SerializeField] private GameObject bossDusecek;

    private GameObject player;

    [SerializeField] private float maxCan;
    private float can;

    [SerializeField] private float hiz;
    private float ebad;

    [SerializeField] private Vector3 dashAtilacakKonum;
    [SerializeField] private float dashSuresi;
    private float dashSuresiSayaci = 0;
    private bool dashatiyor = false;
    private int dashSirasi = 0;
    private bool dashAttack = false;
    private bool sec = false;
    private float laserSuresi = 0;

    [SerializeField] private float atisSuresi;
    private float atisSuresiSayaci = 0;
    private bool AtisYapiyor;
    private bool tara = true;
    private float EskiDusmanSpawnSuresi;

    private Animator animator;


    private void Awake()
    {
        can = maxCan;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        ebad = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        can = gameObject.GetComponent<bossHealth>().getCan();

        saldir();
        hareket();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {

        }
    }

    void hareket()
    {
        if (can > maxCan / 2)
        {
            if (!dashatiyor)
            {
                Vector2 yön = player.transform.position - transform.position;
                yön.Normalize();

                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, hiz * Time.deltaTime);

                if (yön.x > 0)
                {
                    ChangeDirection(ebad);
                }
                else if (yön.x < 0)
                {
                    ChangeDirection(-ebad);
                }
            }
        }
        else
        {
            if (can <= 0)
            {
                animator.SetTrigger("dead");
                StartCoroutine(dead());
            }
            else if (!AtisYapiyor)
            {
                Vector2 yön = player.transform.position - transform.position;
                yön.Normalize();

                if (tara && laserSuresi <=0)
                {
                    laserSuresi = 0.2f;
                    GameObject laser = Instantiate(lazer, transform.position, transform.rotation);
                    laser.GetComponent<boss2Laser>().setFareKonumu(player.transform.position);
                    laser.GetComponent<boss2Laser>().setHiz(3);
                }
                else laserSuresi -= Time.deltaTime;

                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, hiz * 0.9f * Time.deltaTime);

                if (yön.x > 0)
                {
                    ChangeDirection(ebad);
                }
                else if (yön.x < 0)
                {
                    ChangeDirection(-ebad);
                }
            }


        }
    }

    void saldir()
    {
        if (can > maxCan / 2) dash();
        else AtisYap();
    }

    void AtisYap()
    {
        if (!AtisYapiyor && atisSuresiSayaci <= 0)
        {
            AtisYapiyor = true;
        }

        if (AtisYapiyor && atisSuresiSayaci <= 0)
        {
            atisSuresiSayaci = atisSuresi;

            StartCoroutine(AtisYapAnimation());

            GameObject MermI = Instantiate(mermi, transform.position, transform.rotation);
            MermI.GetComponent<boss2Mermi>().setFareKonumu(player.transform.position);

        }
        else atisSuresiSayaci -= Time.deltaTime;
    }

    void dash()
    {
        if (!dashatiyor && dashSuresiSayaci <= 0)
        {
            dashatiyor = true;
        }
        if (dashatiyor)
        {
             StartCoroutine(dashAtAnimation());
            
            if (dashAttack)
            {
                if (laserSuresi <= 0)
                {
                    laserSuresi = 0.15f;
                    GameObject laser = Instantiate(lazer, transform.position, transform.rotation);
                    laser.GetComponent<boss2Laser>().setFareKonumu(player.transform.position);
                }
                else laserSuresi -= Time.deltaTime;
            }
            else if (dashSirasi == 1)
            {
                Vector2 yön = dashAtilacakKonum - transform.position;
                yön.Normalize();

                transform.position = Vector2.MoveTowards(transform.position, dashAtilacakKonum, hiz* 3 * Time.deltaTime);

                if (yön.x > 0)
                {
                    ChangeDirection(ebad);
                }
                else if (yön.x < 0)
                {
                    ChangeDirection(-ebad);
                }
            }

            if (dashAtilacakKonum == transform.position)
            {
                dashSirasi = 2;
            }
        }
        else dashSuresiSayaci -= Time.deltaTime;
    }

    IEnumerator AtisYapAnimation()
    {
        animator.SetTrigger("fire");
        yield return new WaitForSeconds(0.3f);
        AtisYapiyor = false;
    }

    IEnumerator dashAtAnimation()
    {
        if (dashSirasi == 0)
        {
            dashSuresiSayaci = dashSuresi;

            animator.SetTrigger("dashStart");
            yield return new WaitForSeconds(1f);

            if (!sec)
            {
                dashAtilacakKonum = player.transform.position;
                sec = true;
            }

            dashSirasi = 1;
        }
        else if (dashSirasi == 1)
        {
            animator.SetTrigger("dash");
            yield return new WaitForSeconds(.1f);
        }
        else if (dashSirasi == 2)
        {
            animator.SetTrigger("dashStop");
            yield return new WaitForSeconds(.05f);
            dashSirasi = 3;
        }
        else if(dashSirasi == 3)
        {
            dashAttack = true;
            yield return new WaitForSeconds(2f);
            dashatiyor = false;
            dashAttack = false;
            sec = false;
            dashSirasi = 0;
        }
    }

    IEnumerator bekle (float sure)
    {
        dashAttack = true;
        yield return new WaitForSeconds(sure);
        dashatiyor = false;
        dashAttack = false;
    }

    IEnumerator dead()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    void ChangeDirection(float direction)
    {
        Vector3 tempsScale = transform.localScale;
        tempsScale.x = direction;
        transform.localScale = tempsScale;
    }
}
