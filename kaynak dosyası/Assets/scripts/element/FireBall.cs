using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private float hasar;
    private int seviye = 1;

    private Transform hedef ;
    [SerializeField] private float speed = 8f;
    [SerializeField] private GameObject FireBallAreaSablonu;

    private int FireBallAreaDamage = 1;
    private Vector3 mesafe;
    private float mesafeKare;
    private Vector2 mp;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        if (hedef == null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            mesafe = hedef.position - transform.position;
            mesafeKare = speed * Time.deltaTime;
            mp = new Vector2(mesafe.normalized.x * speed, mesafe.normalized.y * speed);
            yonlen();
        }

        hasar += seviye * 3;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = mp;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.GetComponent<Enemy_Follow>().TakeDamage(hasar);

            GameObject FireBallArea = Instantiate(FireBallAreaSablonu, transform.position,new Quaternion(0,0,0,0));
            FireBallArea fireballarea = GetComponent<FireBallArea>();

            if (fireballarea != null)
            {
                fireballarea.SetDamage(FireBallAreaDamage);
            }

            Destroy(gameObject);
        }
    }

    public void HedefAta(Transform hedef)
    {
        this.hedef = hedef;
    }

    private void yon_ata(float z)
    {
        Vector3 yon = new Vector3(0, 0, z);
        transform.localRotation = Quaternion.Euler(yon);
    }

    private float yon_bul(Vector3 yon, float ek)
    {
        float aci = yon.y / (yon.x + yon.y);
        aci = aci * 90f;
        aci += ek;

        return aci;
    }

    private float yon_bul_x(Vector3 yon, float ek)
    {
        float aci = yon.x / (yon.x + yon.y);
        aci = aci * 90f;
        aci += ek;

        return aci;
    }

    private void yonlen()
    {
        Vector3 h = hedef.position;
        Vector3 m = transform.position;

        if (h.x > m.x && h.y > m.y)
        {
            Vector3 g = h - m;
            yon_ata(yon_bul(g, 0f));

            float a = transform.rotation.z;
        }
        else if (h.x < m.x && h.y > m.y)
        {
            Vector3 g = new(m.y - h.y, h.x - m.x, m.z);
            yon_ata(yon_bul(g, 90f));
        }
        else if (h.x < m.x && h.y < m.y)
        {
            Vector3 g = m - h;
            yon_ata(yon_bul(g, 180f));
        }
        else
        {
            Vector3 g = new(h.x - m.x, m.y - h.y, m.z);
            yon_ata(yon_bul_x(g, -90f));
        }
    }

    public void setSeviye(int seviye)
    {
        this.seviye = seviye;
    }
}
