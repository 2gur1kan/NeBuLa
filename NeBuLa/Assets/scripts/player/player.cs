using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private GameObject LevelPanel;
    [SerializeField] private GameObject maincamera;
    [SerializeField] private GameObject ExpBari;

    //karakter özellikleri
    public float speed = 5f;
    public int ebad = 5;

    private Rigidbody2D playerBody;
    private Animator animator;
    private AudioSource Audio;

    //hareket
    private Vector2 Hareket;
    private bool yon;

    //can malzemeleri
    [SerializeField] private GameObject CanBariSablonu;
    public float CanHavuzu = 10;
    public float Can = 0;
    private bool CanBariVarmi = false;
    private CanBari canbari;
    private bool dead = false;

    //seviye ve seviye atlama
    [SerializeField] private long Exp = 0;
    private long MaxExp = 20;
    [SerializeField] private int Seviye = 1;
    private int oncekiseviye = 1;

    //ses
    private float adimat = 0;

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Can = CanHavuzu;
    }

    // Update is called once per frame
    void Update()
    {
        if(Can <= 0)
        {
            StartCoroutine(isdead());
        }

        if(MaxExp <= Exp)
        {
            MaxExp += Seviye*10;
            Exp = 0;
            Seviye++;
        }


        CanBari();
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            SeviyeAtla();
            PlayerWalk();
        }    
    }

    IEnumerator isdead()
    {
        dead = true;
        animator.SetBool("dead",true);
        yield return new WaitForSeconds(0.5f);
        maincamera.GetComponent<isdead>().dead();
    }

    void SeviyeAtla()
    {
        if (oncekiseviye != Seviye)
        {
            oncekiseviye = Seviye;
            Time.timeScale = 0;
            LevelPanel.GetComponent<ButtonNum>().setDegis();
            
        }
    }

    void PlayerWalk()
    {
        Hareket = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (Hareket.x > 0)
        {
            playerBody.velocity = Hareket * speed;
            ChangeDirection(ebad);
            yon = true;

            if (adimat <= 0)
            {
                Audio.Play();
                adimat = 0.5f;
            }
            else adimat -= Time.deltaTime;
        }
        else if (Hareket.x < 0)
        {
            playerBody.velocity = Hareket * speed;
            ChangeDirection(-ebad);
            yon = false;

            if (adimat <= 0)
            {
                Audio.Play();
                adimat = 0.5f;
            }
            else adimat -= Time.deltaTime; 
           
        }
        else if (Hareket.y != 0)
        {
            playerBody.velocity = Hareket * speed;
            if (adimat <= 0)
            {
                Audio.Play();
                adimat = 0.5f;
            }
            else adimat -= Time.deltaTime;
        }
        else
        {
            playerBody.velocity = Hareket * 0;
        }
        animator.SetInteger("speed", Mathf.Abs((int)playerBody.velocity.x) + Mathf.Abs((int)playerBody.velocity.y));
    }

    void CanBari()
    {
        if (!CanBariVarmi)
        {
            Vector3 t = transform.position + new Vector3(0, 1, 0);
            GameObject CanBari = Instantiate(CanBariSablonu, t, transform.rotation, transform);
            canbari = CanBari.GetComponent<CanBari>();

            CanBariVarmi = true;
        }

        if (Can >= 0)
        {
            canbari.CanAta(CanHavuzu, Can);
        }   
    }

    void ChangeDirection(int direction)
    {
        Vector3 tempsScale = transform.localScale;
        tempsScale.x = direction;
        transform.localScale = tempsScale;
    } 

    public void HasarAl(float hasar)
    {
        this.Can -= hasar;
    }

    public long getExp()
    {
        return this.Exp;
    }

    public long getMaxExp()
    {
        return this.MaxExp;
    }

    public void addExp(int Exp)
    {
        ExpBari.GetComponent<expses>().expSes();
        this.Exp += Exp;
    }

    public int getSeviye()
    {
        return this.Seviye;
    }

    public void playerCanHavuzuAdd()
    {
        this.CanHavuzu += 10 + Seviye;
        this.Can += 10 + Seviye;
    }

    public void playerCanAdd()
    {
        this.Can += 20 + (2 * Seviye);
        if(Can > CanHavuzu)
        {
            Can = CanHavuzu;
        }
    }

    public void playerSpeedAdd()
    {
        this.speed += 0.2f;
    }

    public bool getYon()
    {
        return yon;
    }
}
