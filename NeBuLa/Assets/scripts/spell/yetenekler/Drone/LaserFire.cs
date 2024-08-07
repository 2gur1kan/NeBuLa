using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFire : MonoBehaviour
{
    [SerializeField] private float hasar;
    private int seviye = 1;

    private Animator anim;
    private Transform hedef;

    // Start is called before the first frame update
    void Start()
    {
        hasar += seviye*2;
        anim = GetComponent<Animator>();

        yonlen();
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("Fire"))
        {
            StartCoroutine(bekle(1f));
        }
        else StartCoroutine(YokOl(1f));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.GetComponent<Enemy_Follow>().TakeDamage(hasar);
        }
    }

    IEnumerator bekle(float sure)
    {
        yield return new WaitForSeconds(sure);
        anim.SetBool("Fire", true);        
    }

    IEnumerator YokOl(float sure)
    {
        yield return new WaitForSeconds(sure);
        Destroy(gameObject);
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
            Vector3 g = m - h;
            yon_ata(yon_bul(g, 180f));
        }
        else if (h.x < m.x && h.y > m.y)
        {
            Vector3 g = new(h.x - m.x, m.y - h.y, m.z);
            yon_ata(yon_bul_x(g, -90f));
        }
        else if (h.x < m.x && h.y < m.y)
        {
            Vector3 g = h - m;
            yon_ata(yon_bul(g, 0f));
        }
        else
        {
            Vector3 g = new(m.y - h.y, h.x - m.x, m.z);
            yon_ata(yon_bul(g, 90f));
        }
    }

    public void HedefAta(Transform hedef)
    {
        this.hedef = hedef;
    }

    public void setSeviye(int seviye)
    {
        this.seviye = seviye;
    }
}
