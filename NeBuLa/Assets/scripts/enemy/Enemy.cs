using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Current Values")]
    [SerializeField] protected float HealthScale = 1f;
    [SerializeField] protected float MaxHealth;
    [SerializeField] protected float CurrentHealth;
    [SerializeField] protected float speed = 5f;
    protected int level = 1;
    
    [Header("Control Value")]
    protected bool stun = false;
    protected float size = 7;
    protected float fireTime = 0f;
    protected GameObject player;
    protected Rigidbody2D rb;
    protected Animator animator;

    [Header("Drop : ")]
    [SerializeField] protected GameObject Exp;
    [SerializeField] protected float ExpDropChance = .5f;
    [SerializeField] protected GameObject Stat;
    [SerializeField] protected float StatDropChance = .05f;

    // getter methods
    public float Health { get => CurrentHealth; } 
    public float Speed { get => speed; }
    public float Level { get => level; }

    //////////////////// ana fonksiyonlar ////////////////////////////
    
    private void Awake()
    {
        EnemyAwake();
    }
    private void Start()
    {
        EnemyStart();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyTriggerEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyTriggerExit(collision);
    }

    private void OnDestroy()
    {
        EnemyOnDestroy();
    }

    //////////////////// erisim fonksiyonlar� ////////////////////////////

    /// <summary>
    /// Awake fonksiyonunu alt s�n�flardada kullanmak i�in bir ara i�lev
    /// </summary>
    protected virtual void EnemyAwake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        size = transform.localScale.x;
        level = player.GetComponent<player>().getSeviye();
        MaxHealth += level * HealthScale - HealthScale;
        CurrentHealth = MaxHealth;
    }

    /// <summary>
    /// Start fonksiyonunu alt s�n�flardada kullanmak i�in bir ara i�lev
    /// </summary>
    protected virtual void EnemyStart()
    {

    }

    /// <summary>
    /// Ektikle�ime giren obje ile ya�anacak olaylar
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void EnemyTriggerEnter(Collider2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Lightning":
                StartCoroutine(UseLightning());
                break;
            case "Ice":
                speed /= 2;
                break;
            case "Fire":
                SetUseFire(4f);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Etkile�imden ��kacak obje ile ya�anacak olaylar
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void EnemyTriggerExit(Collider2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Ice":
                speed *= 2;
                break;
            case "Fire":
                SetUseFire();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// OnDestroy fonksiyonunu alt s�n�flardada kullanmak i�in bir ara i�lev
    /// </summary>
    protected virtual void EnemyOnDestroy()
    {
        if (gameObject.GetComponent<bolun>() != null)
        {
            gameObject.GetComponent<bolun>().yaratikCikar();
        }
    }

    //////////////////// di�er fonksiyonlar ////////////////////////////

    /// <summary>
    /// yok oldu�u zaman yerine getirmesi gereken ana i�lemler
    /// </summary>
    protected virtual void Destroy()
    {
        Drop();
        Destroy(gameObject);
    }

    /// <summary>
    /// Enemy genel olarak yok oldu�u zamanlarda ortaya ��kacak olan objeleri olu�turur
    /// </summary>
    protected virtual void Drop()
    {
        float rand = Random.Range(0f, 1f);

        if (rand <= ExpDropChance)
        {
            Instantiate(Exp, transform.position, transform.rotation);
        }

        rand = Random.Range(0f,1f);

        if (rand > StatDropChance)
        {
            Instantiate(Stat, transform.position, transform.rotation);
        }
    }

    /// <summary>
    /// karakterin stun yemesini sa�lar
    /// </summary>
    protected IEnumerator UseLightning(float time = .5f)
    {
        stun = true;
        yield return new WaitForSeconds(time);
        stun = false;
    }

    /// <summary>
    /// Use fire methoduna eri�imi kontol eden bir ara method
    /// </summary>
    /// <param name="time"></param>
    protected void SetUseFire(float time = 2f)
    {
        fireTime += time;

        if (fireTime <= 0) InvokeRepeating("UseFire", 0f, .5f);
    }

    /// <summary>
    /// D��man�n yanmas�n� sa�layan bir method
    /// </summary>
    protected void UseFire()
    {
        fireTime -= Time.deltaTime;

        if(fireTime <= 0) CancelInvoke("UseFire");
        else CurrentHealth -= MaxHealth / 100;
    }

    /// <summary>
    /// D��manin hasar alamas�n� sa�layan fonksiyon
    /// </summary>
    /// <param name="hasar"></param>
    public void TakeDamage(float damage)
    {
        this.CurrentHealth -= damage;
    }
}
