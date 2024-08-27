using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2Laser : MonoBehaviour
{
    [SerializeField] private float hiz = 15;
    [SerializeField] private float hasar = 1;
    [SerializeField] private int seviye = 1;
    [SerializeField] private float kaybolmaSuresi = 4;

    private Vector3 fareKonumu;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        mermiYonu();

        StartCoroutine(sil(kaybolmaSuresi));
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = fareKonumu.normalized * hiz;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (collision.GetComponent<player>() != null)
            {
                collision.GetComponent<player>().HasarAl(hasar);
                Destroy(gameObject);
            }
        }
    }
    void mermiYonu()
    {
        float x = Random.RandomRange(0, 5);
        float y = Random.RandomRange(0, 5);

        x = konumAyarlaX(x);
        y = konumAyarlaY(y);

        fareKonumu = fareKonumu - transform.position;

        fareKonumu.x += x;
        fareKonumu.y += y;
    }

    float konumAyarlaX(float aci)
    {
        if (fareKonumu.y == transform.position.y)
        {
            return 0;
        }
        else if (fareKonumu.y > transform.position.y)
        {
            return aci;
        }
        else if (fareKonumu.y < transform.position.y)
        {
            return -aci;
        }

        return 0;
    }

    float konumAyarlaY(float aci)
    {
        if (fareKonumu.x == transform.position.x)
        {
            return 0;
        }
        else if (fareKonumu.x > transform.position.x)
        {
            return aci;
        }
        else if (fareKonumu.x < transform.position.x)
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
    public void setFareKonumu(Vector3 fareKonumu)
    {
        this.fareKonumu = fareKonumu;
    }

    public void setSeviye(int seviye)
    {
        this.seviye = seviye;
    }

    public void setHiz(float hiz)
    {
        this.hiz = hiz;
    }
}
