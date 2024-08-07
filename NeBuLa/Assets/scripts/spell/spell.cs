using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spell : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] protected float spellCastTime = 1f;

    [Header("Damage")]
    [SerializeField] protected int baseDamage;
    [SerializeField] protected int damageScale;
    protected float damage;
    private int level = 1;
    protected Transform target;

    [Header("Move")]
    [SerializeField] protected float speed = 8f;
    protected Vector2 MoveForRB;

    [Header("second phase")]
    [SerializeField] protected GameObject destroyObject = null;
    protected GameObject CreateObject = null;
    protected spell CreateObjectSC = null;

    protected Rigidbody2D rb;
    protected Transform Player;
    protected GameObject EnemysObject = null;

    protected int Level { get => level; set => level = value; }

    ////////////////////// baz i�levler ///////////////////////////////

    private void Awake()
    {
        SpellAwake();
    }

    private void Start()
    {
        SpellStart();
    }

    private void Update()
    {
        SpellUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpellOnTriggerEnter2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SpellOnTriggerExit2D(collision);
    }

    ////////////////////// eri�im i�levleri ///////////////////////////////

    /// <summary>
    /// Awake i�levi i�in bir ilete�
    /// </summary>
    protected virtual void SpellAwake()
    {
        if (GetComponent<Rigidbody2D>()) rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        EnemyObjectFinder();

        Attack();
    }

    /// <summary>
    /// start i�levi i�in bir ilete�
    /// </summary>
    protected virtual void SpellStart()
    {

    }

    /// <summary>
    /// Update i�levi i�in bir ilete�
    /// </summary>
    protected virtual void SpellUpdate()
    {
        
    }

    /// <summary>
    /// triggerEnter2d i�levi i�in bir ilete�
    /// </summary>
    protected virtual void SpellOnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);

            DestroySpell();
        }
    }

    /// <summary>
    /// triggerexir2d i�levi i�in bir ilete�
    /// </summary>
    protected virtual void SpellOnTriggerExit2D(Collider2D collision)
    {

    }

    ////////////////////// timer i�levleri ///////////////////////////////
    
    /// <summary>
    /// attak i�levini kendi i�erisinde oyun bitene kadar belirli aral�klarla tekrarlamas�n� sa�lar
    /// </summary>
    protected virtual void restartAttack()
    {
        Invoke("Attack", spellCastTime);
    }

    /// <summary>
    /// s�re sayac�n� es ge�erek tekrar sald�r�n�n yap�lmas�n� sa�lar
    /// </summary>
    protected virtual void cancelAttackTimer()
    {
        CancelInvoke("Attack");
        Attack();
    }

    ////////////////////// di�er i�levler ///////////////////////////////

    /// <summary>
    /// spell in levelini 1 att�rmaya yarar
    /// </summary>
    public int LevelUp()
    {
        level++;

        damage = baseDamage + (level * damageScale);

        return level;
    } 

    /// <summary>
    /// spell in levelini isteilen seviyeye y�kseltir
    /// </summary>
    public int LevelUp(int level)
    {
        this.level = level;

        damage = baseDamage + (level * damageScale);

        return level;
    }

    public virtual void Attack()
    {
        gameObject.SetActive(false);

        restartAttack();
    }

    protected virtual void DestroySpell()
    {
        if (destroyObject != null) CreateOrUseDestroyObject();
        gameObject.SetActive(false);
    }

    protected virtual void CreateOrUseDestroyObject()
    {
        if(CreateObject == null)
        {
            CreateObject = Instantiate(destroyObject, transform.position, Quaternion.identity);
            CreateObjectSC = CreateObject.GetComponent<spell>();

            CreateObject.transform.position = transform.position;
            CreateObjectSC.Level = level;

            CreateObjectSC.Attack();
        }
        else
        {
            CreateObject.transform.position = transform.position;
            CreateObjectSC.Attack();
        }
    }

    ////////////////////////////////// Enemy Finder ////////////////////////////////////////////

    /// <summary>
    /// Enemy object nesnesin bulmaya u�ra��r bulursa atamas�n� yapar
    /// </summary>
    private void EnemyObjectFinder()
    {
        if(GameObject.FindGameObjectWithTag("EnemysObject"))
        {
            EnemysObject = GameObject.FindGameObjectWithTag("EnemysObject");
            return;
        }

        StartCoroutine(SearchEnemyObject());
    }

    private IEnumerator SearchEnemyObject()
    {
        while (EnemysObject == null)
        {
            yield return new WaitForSeconds(.5f);

            if (GameObject.FindGameObjectWithTag("EnemysObject"))
            {
                EnemysObject = GameObject.FindGameObjectWithTag("EnemysObject");
            }
        }

        yield return null;
    }
}
