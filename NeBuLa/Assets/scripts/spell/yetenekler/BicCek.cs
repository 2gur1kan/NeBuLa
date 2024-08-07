using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BicCek : MonoBehaviour
{
    [SerializeField] private float hasar;
    private int seviye = 1;

    private Transform hedef;
    private Transform merkez;
    [SerializeField] private float speed = 10f;

    private Rigidbody2D rb;
    private Animator animator;

    private bool back = false;
    private bool vardi = false;

    // Start is called before the first frame update
    void Start()
    {
        hasar += seviye * 5;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        yonlen();
        //rb.MoveRotation
    }

    // Update is called once per frame
    void Update()
    {
        if (back)
        {
            Vector3 mesafe = merkez.position - transform.position;
            float mesafeKare = speed * Time.deltaTime;

            if (mesafe.magnitude <= mesafeKare)
            {
                Destroy(gameObject);
                return;
            }

            transform.Translate(mesafe.normalized * mesafeKare);
        }
        else
        {
            if (hedef == null)
            {
                back = true;
                return;
            }

            Vector3 mesafe = hedef.position - transform.position;
            float mesafeKare = speed * Time.deltaTime;
            Vector2 mp = new Vector2(mesafe.normalized.x * speed * 2, mesafe.normalized.y * speed);


            if (mesafe.magnitude <= mesafeKare*4)
            {
                rb.velocity = new Vector2(0f, 0f);
                yon_ata(0f);
                vardi = true;
                animator.SetBool("cek", true);
                StartCoroutine(bekle(1));
                return;
            }

            if (!vardi)
            {
                rb.velocity = mp;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.GetComponent<Enemy_Follow>().TakeDamage(hasar);
        }
    }

    public void HedefAta(Transform hedef)
    {
        this.hedef = hedef;
    }
    public void SetMerkez(Transform merkez)
    {
        this.merkez = merkez;
    }

    IEnumerator bekle(float sure)
    {
        yield return new WaitForSeconds(sure);
        back = true;
    }

    private void yon_ata(float z)
    {
        Vector3 yon = new Vector3(0,0,z);
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
