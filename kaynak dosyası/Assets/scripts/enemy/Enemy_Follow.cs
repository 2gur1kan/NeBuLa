using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Follow : MonoBehaviour
{
    [SerializeField] private GameObject Exp;

    private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float ebad = 7;

    private Rigidbody2D enemyBody;
    private Animator animator;
    private bool expyapamadi = true;
    private bool drop = true;

    [SerializeField] private float CanBuyumeOrani = 1f;
    [SerializeField] private float MaxCan;
    [SerializeField] private float Can;
    private int seviye = 1;

    private bool stun = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        ebad = transform.localScale.x;
        seviye = player.GetComponent<player>().getSeviye();
        MaxCan += seviye * CanBuyumeOrani - CanBuyumeOrani;
        Can = MaxCan;
    }

    // Update is called once per frame
    void Update()
    {
        takip();
    }

    void takip()
    {
        float Uzaklik = Vector3.Distance(transform.position, player.transform.position);

        if (Can <= 0 || Uzaklik > 30)
        {
            if(Can <= 0 && expyapamadi)
            {
                Instantiate(Exp, transform.position, transform.rotation);
                expyapamadi = false;
            }

            animator.SetBool("DEAD", true);
            StartCoroutine(dead());
        }
        else if (!stun)
        {
            Vector2 yön = player.transform.position - transform.position;
            yön.Normalize();

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

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

    IEnumerator dead()
    {
        if (drop)
        {
            gameObject.GetComponent<StatDrop>().Drop();

            if (gameObject.GetComponent<bolun>() != null)
            {
                gameObject.GetComponent<bolun>().yaratikCikar();
            }

            drop = false;
        }  
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   //element özellikleri
        if(collision.transform.tag == "Lightning")
        {
            StartCoroutine(bekle(0.5f));
        }
        if (collision.transform.tag == "Ice")
        {
            speed /= 2;
        }
        if (collision.transform.tag == "Fire")
        {
            StartCoroutine(yan());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ice")
        {
            speed *= 2;
        }
        if (collision.transform.tag == "Fire")
        {
            StartCoroutine(yan());
        }
    }

    void ChangeDirection(float direction)
    {
        Vector3 tempsScale = transform.localScale;
        tempsScale.x = direction;
        transform.localScale = tempsScale;
    }

    public void TakeDamage(float hasar)
    {
        this.Can -= hasar;
    }

    public float GetSpeed()
    {
        return this.speed;
    }

    public float getCan()
    {
        return this.Can;
    }

    public int getSeviye()
    {
        return this.seviye;
    }

    IEnumerator bekle(float sure)
    {
        stun = true;
        yield return new WaitForSeconds(sure);
        stun = false;
    }
    IEnumerator yan()
    {
        for(int i = 0; i < 5; i++)
        {
            Can -= MaxCan / 100;
            yield return new WaitForSeconds(1f);
        }
    }

}
