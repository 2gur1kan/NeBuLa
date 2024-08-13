using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spell : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] protected float spellCastTime = 10f;
    [SerializeField] protected float spellExistenceTime = 6f;

    [Header("Damage")]
    [SerializeField] protected int baseDamage;
    [SerializeField] protected int damageScale;
    [SerializeField] protected float radius = 30f;
    protected float damage;
    protected int level = 1;
    protected Vector3 target;
    protected bool isActive = true;

    [Header("second phase")]
    [SerializeField] protected GameObject destroyObject = null;
    protected GameObject CreateObject = null;
    protected spell CreateObjectSC = null;

    protected Rigidbody2D rb;
    protected Collider2D clldr;
    protected SpriteRenderer sr;
    protected AudioSource aus = null;
    protected Transform player;
    protected Enemy_Array EnemysObject = null;

    [Header("Drop and Trap spell used")]
    protected Transform dropPos = null;

    public int Level { get => level; set => level = value; }
    public Transform Player { get => player; set => player = value; }
    public Transform DropPos { set => dropPos = value; }

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
        if(player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        clldr = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        if(GetComponent<AudioSource>()) aus = GetComponent<AudioSource>();

        target = Vector3.negativeInfinity;

        EnemyObjectFinder();
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
        if (collision.transform.tag == "Enemy" && isActive)
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
    /// tekrardan attack yapmas� i�in i�lemleri kapat�r
    /// </summary>
    protected virtual void RestartAttackTimer()
    {
        setActivateFalse();
        CancelInvoke("DestroySpell");
        //DestroySpell();
        CancelInvoke("Attack");
        Invoke("Attack", .1f);
    }

    protected virtual void ActivateSpell()
    {
        setActiveTrue();
        Invoke("DestroySpell", spellExistenceTime);
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
        target = EnemysObject.SelectNearestEnemy(player.transform.position, radius);

        if (target == null || target.x == Mathf.NegativeInfinity)
        {
            RestartAttackTimer();
            return;
        }

        transform.position = target;

        ActivateSpell();

        restartAttack();
    }

    protected virtual void DestroySpell()
    {
        if (!isActive) return;

        if (destroyObject != null) CreateOrUseDestroyObject();
        setActivateFalse();
    }

    protected virtual void CreateOrUseDestroyObject()
    {
        if (CreateObject == null)
        {
            CreateObject = Instantiate(destroyObject, transform.position, Quaternion.identity);
            CreateObjectSC = CreateObject.GetComponent<spell>();

            AddSpellCreateValue();

            CreateObjectSC.Attack();
        }
        else
        {
            CreateObjectSC.Attack();
        }
    }

    protected virtual void AddSpellCreateValue()
    {
        CreateObjectSC.Level = level;
        CreateObjectSC.DropPos = transform;//d��ece�i nokta

        // obje ilk olu�tu�unda konumunda olu�mas�n� sa�lar
        CreateObject.transform.position = transform.position;
    }

    ////////////////////////////////// Enemy Finder ////////////////////////////////////////////

    /// <summary>
    /// Enemy object nesnesin bulmaya u�ra��r bulursa atamas�n� yapar
    /// </summary>
    protected void EnemyObjectFinder()
    {
        if(GameObject.FindGameObjectWithTag("EnemysObject"))
        {
            EnemysObject = GameObject.FindGameObjectWithTag("EnemysObject").GetComponent<Enemy_Array>();

            //d��man objesi olu�turuldu�und atack yapmas�n� sa�lar
            Attack();
            return;
        }

        StartCoroutine(SearchEnemyObject());
    }

    protected IEnumerator SearchEnemyObject()
    {
        while (EnemysObject == null)
        {
            yield return new WaitForSeconds(.5f);

            if (GameObject.FindGameObjectWithTag("EnemysObject"))
            {
                EnemysObject = GameObject.FindGameObjectWithTag("EnemysObject").GetComponent<Enemy_Array>();

                //d��man objesi olu�turuldu�und atack yapmas�n� sa�lar
                Attack();
            }
        }

        yield return null;
    }

    //////////////////////////////// Spell Acrivate ///////////////////////////////////////
    
    /// <summary>
    /// objenin g�r�n�r evrende olu�mas�n� sa�lar
    /// </summary>
    protected void setActiveTrue()
    {
        isActive = true;
        clldr.enabled = true;
        sr.enabled = true;
        if(aus != null) aus.enabled = true;
    }

    /// <summary>
    /// objenin g�n�r d�zlemde kaybolmas�n� sa�lar
    /// </summary>
    protected void setActivateFalse()
    {
        isActive = false;
        clldr.enabled = false;
        sr.enabled = false;
        if (aus != null) aus.enabled = false;
    }
}
