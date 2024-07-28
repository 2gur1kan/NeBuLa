using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spell : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] protected int baseDamage;
    [SerializeField] protected int damageScale;
    protected float damage;
    private int level = 1;
    protected Transform target;

    [Header("Move")]
    [SerializeField] protected float speed = 8f;
    [SerializeField] protected bool isTrow = true;
    protected Vector2 MoveForRB;

    [Header("second phase")]
    [SerializeField] protected GameObject destroyObject = null;
    protected GameObject CreateObject = null;
    protected spell CreateObjectSC = null;

    protected Rigidbody2D rb;
    protected Transform Player;

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

    protected virtual void SpellAwake()
    {
        if (GetComponent<Rigidbody2D>()) rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Attack();
    }

    protected virtual void SpellStart()
    {

    }

    protected virtual void SpellUpdate()
    {
        rb.velocity = MoveForRB;
    }

    protected virtual void SpellOnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);

            DestroySpell();
        }
    }

    protected virtual void SpellOnTriggerExit2D(Collider2D collision)
    {

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
        gameObject.SetActive(true);

        if (isTrow)
        {
            transform.position = Player.position;
            SelectDirection();
            Vector3 distance = target.position - transform.position;
            float CalculateSpeed = speed * Time.deltaTime;
            MoveForRB = new Vector2(distance.normalized.x * CalculateSpeed, distance.normalized.y * CalculateSpeed);
        }

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

    ////////////////////// Y�n bulma i�levleri
    protected void SelectDirection()
    {
        Vector3 h = target.position;
        Vector3 m = transform.position;
        Vector3 g = h - m;

        if (h.x > m.x && h.y > m.y)
        {
            DirectionAssign(DirectionFinder(g, 0f));
        }
        else if (h.x < m.x && h.y > m.y)
        {
            g = new Vector3(m.y - h.y, h.x - m.x, m.z);
            DirectionAssign(DirectionFinder(g, 90f));
        }
        else if (h.x < m.x && h.y < m.y)
        {
            g = m - h;
            DirectionAssign(DirectionFinder(g, 180f));
        }
        else
        {
            g = new Vector3(h.x - m.x, m.y - h.y, m.z);
            DirectionAssign(DirectionFinder(g, -90f));
        }
    }

    protected void DirectionAssign(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle); // A��ya g�re rotasyon ayar�
    }

    protected float DirectionFinder(Vector3 yon, float add)
    {
        float angle = Mathf.Atan2(yon.y, yon.x) * Mathf.Rad2Deg; // A�� hesaplamas�
        angle += add;
        return angle;
    }
}
