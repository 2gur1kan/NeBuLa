using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ok : MonoBehaviour
{
    [SerializeField] private float hiz = 15;
    [SerializeField] private float hasar = 2;
    private int seviye = 1;
    [SerializeField] private float kaybolmaSuresi = 1f;

    private Vector3 ms;
    private Vector3 hedef;
    private Rigidbody2D rb;
    private bool ilkOk = true;
    private bool sagaGidenMermi = true;

    void Start()
    {
        hasar += seviye;
        if (ilkOk)
        {
            hedef = transform.position + new Vector3(1, 0, 0);
        }

        okSac();
        rb = GetComponent<Rigidbody2D>();

        ms = hedef.normalized * hiz;

        StartCoroutine(sil(kaybolmaSuresi));
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = ms;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            /*if (collision.GetComponent<bossHealth>() != null)
            {
                collision.GetComponent<bossHealth>();
                Destroy(gameObject);
            }*/
            if (collision.GetComponent<Enemy_Follow>() != null)
            {
                collision.GetComponent<Enemy_Follow>().TakeDamage(hasar);
                Destroy(gameObject);
            }
        }
    }

    void okSac()
    {
        Vector3 hedef = this.hedef;

        if (ilkOk)
        {
            GameObject ok1 = Instantiate(gameObject, transform.position, transform.rotation);
            ok1.GetComponent<ok>().set›lkOk();
            ok1.GetComponent<ok>().setHedef(hedef);
            ok1.GetComponent<ok>().setSeviye(seviye - 1);

            GameObject ok2 = Instantiate(gameObject, transform.position, transform.rotation);
            ok2.GetComponent<ok>().set›lkOk();
            ok2.GetComponent<ok>().setSagaGidenOk();
            ok2.GetComponent<ok>().setHedef(hedef);
            ok2.GetComponent<ok>().setSeviye(seviye - 1);

            this.hedef = hedef - transform.position;
        }
        else if (sagaGidenMermi)
        {
            okYonu(0.5f);

            this.hedef = hedef - transform.position;

            if (seviye > 0)
            {
                GameObject ok3 = Instantiate(gameObject, transform.position, transform.rotation);
                ok3.GetComponent<ok>().set›lkOk();
                ok3.GetComponent<ok>().setHedef(hedef);
                ok3.GetComponent<ok>().setSeviye(seviye - 1);
            }
        }
        else
        {
            okYonu(-0.5f);

            if (seviye > 0)
            {
                GameObject ok3 = Instantiate(gameObject, transform.position, transform.rotation);
                ok3.GetComponent<ok>().set›lkOk();
                ok3.GetComponent<ok>().setSagaGidenOk();
                ok3.GetComponent<ok>().setHedef(hedef);
                ok3.GetComponent<ok>().setSeviye(seviye - 1);
            }
        }
    }

    void okYonu(float aci)
    {
        float x = konumAyarlaX(aci);
        float y = konumAyarlaY(aci);

        hedef = hedef - transform.position;

        hedef.x += x;
        hedef.y += y;
    }

    float konumAyarlaX(float aci)
    {
        if (sagaGidenMermi)
        {
            if (hedef.y == transform.position.y)
            {
                return 0;
            }
            else if (hedef.y > transform.position.y)
            {
                return aci;
            }
            else if (hedef.y < transform.position.y)
            {
                return -aci;
            }
        }
        else
        {
            if (hedef.y == transform.position.y)
            {
                return 0;
            }
            else if (hedef.y > transform.position.y)
            {
                return -aci;
            }
            else if (hedef.y < transform.position.y)
            {
                return aci;
            }
        }

        return 0;
    }

    float konumAyarlaY(float aci)
    {
        if (hedef.x == transform.position.x)
        {
            return 0;
        }
        else if (hedef.x > transform.position.x)
        {
            return aci;
        }
        else if (hedef.x < transform.position.x)
        {
            return -aci;
        }

        return 0;
    }

    IEnumerator sil(float sure)
    {
        yield return new WaitForSeconds(sure);
        Destroy(gameObject);
    }

    public void setSeviye(int seviye)
    {
        this.seviye = seviye;
    }

    public void setHiz(float hiz)
    {
        this.hiz = hiz;
    }

    public void set›lkOk()
    {
        ilkOk = false;
    }

    public void setSagaGidenOk()
    {
        this.sagaGidenMermi = false;
    }

    public void setHedef(Vector3 hedef)
    {
        this.hedef = hedef;
    }
}
