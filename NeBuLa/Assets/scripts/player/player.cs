using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [Header("Movment")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private int ebad = 5;
    private Vector2 Hareket;
    private bool yon;

    [Header("Health")]
    [SerializeField] private GameObject CanBariSablonu;
    private float maxHealth = 10;
    private float currentHeath = 0;
    private bool CanBariVarmi = false;
    private CanBari canbari;
    private bool dead = false;

    [Header("other")]
    public static player Instance;
    [SerializeField] private int level = 1;
    [SerializeField] private int healthPerLevel = 10;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource Audio;
    [SerializeField] private GameObject LevelPanel;
    [SerializeField] private GameObject maincamera;
    [SerializeField] private GameObject ExpBari;

    //ses
    private float adimat = 0;

    //seviye ve seviye atlama
    [SerializeField] private long Exp = 0;
    private long MaxExp = 20;
    private int oncekiseviye = 1;

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurrentHeath { get => currentHeath; set => currentHeath = value; }

    /////////////////////////// base fonc

    void Awake()
    {
        // eðer baþka bir player varsa onu yok eder
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        currentHeath = maxHealth;
    }

    void Update()
    {
        if(currentHeath <= 0)
        {
            StartCoroutine(isdead());
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

    /////////////////////////////////////// player fonc /////////////////////////////////////

    //////////////////////////////// hasar

    /// <summary>
    /// playerýn hasar almasýný saðlar
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage) { this.currentHeath -= damage; }

    /// <summary>
    /// player oldüðü zaman gerçekleþecek iþlevler
    /// </summary>
    /// <returns></returns>
    IEnumerator isdead()
    {
        dead = true;
        animator.SetBool("dead",true);
        yield return new WaitForSeconds(0.5f);
        maincamera.GetComponent<isdead>().dead();
    }

    public void AddHealth()
    {
        this.maxHealth += 10 + level;
        this.currentHeath += 10 + level;
    }

    public void heal()
    {
        this.currentHeath += 20 + (2 * level);
        if (currentHeath > maxHealth)
        {
            currentHeath = maxHealth;
        }
    }

    private void CanBari()
    {
        if (!CanBariVarmi)
        {
            Vector3 t = transform.position + new Vector3(0, 1, 0);
            GameObject CanBari = Instantiate(CanBariSablonu, t, transform.rotation, transform);
            canbari = CanBari.GetComponent<CanBari>();

            CanBariVarmi = true;
        }

        if (currentHeath >= 0)
        {
            canbari.CanAta(maxHealth, currentHeath);
        }
    }

    /////////////////////////// hareket

    /// <summary>
    /// karakterin hareket etmesini saðlar
    /// </summary>
    private void PlayerWalk()
    {
        Hareket = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (Hareket.x > 0)
        {
            rb.velocity = Hareket * speed;
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
            rb.velocity = Hareket * speed;
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
            rb.velocity = Hareket * speed;
            if (adimat <= 0)
            {
                Audio.Play();
                adimat = 0.5f;
            }
            else adimat -= Time.deltaTime;
        }
        else
        {
            rb.velocity = Hareket * 0;
        }
        animator.SetInteger("speed", Mathf.Abs((int)rb.velocity.x) + Mathf.Abs((int)rb.velocity.y));
    }

    private void ChangeDirection(int direction)
    {
        Vector3 tempsScale = transform.localScale;
        tempsScale.x = direction;
        transform.localScale = tempsScale;
    }


    ///////////////////  seviye :D

    /// <summary>
    /// seviye atlayýnca gerçekleþecek iþlevler
    /// </summary>
    public void LevelUp()
    {
        level++;
        maxHealth += healthPerLevel;
        currentHeath += healthPerLevel;
    }

    /////// old
    void SeviyeAtla()
    {
        if (oncekiseviye != level)
        {
            oncekiseviye = level;
            Time.timeScale = 0;
            LevelPanel.GetComponent<ButtonNum>().setDegis();

        }
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
        return this.level;
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
