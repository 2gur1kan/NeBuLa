using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2Mermi : MonoBehaviour
{
    [SerializeField] GameObject mermiPatla;

    [SerializeField] private float hiz = 15;
    [SerializeField] private float hasar = 1;
    [SerializeField] private float kaybolmaSuresi = 10;

    private Vector3 fareKonumu;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        fareKonumu = fareKonumu - transform.position;

        StartCoroutine(sil(kaybolmaSuresi));
    }

    // Update is called once per frame
    void Update()
    {
        if(fareKonumu != transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, fareKonumu, hiz * 3 * Time.deltaTime);
        }
        else
        {
            StartCoroutine(patla(1f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (collision.GetComponent<player>() != null)
            {
                collision.GetComponent<player>().TakeDamage(hasar);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator patla(float sure)
    {
        yield return new WaitForSeconds(sure);
        Instantiate(mermiPatla, transform.position, transform.rotation);
        Destroy(gameObject);
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

    public void setHiz(float hiz)
    {
        this.hiz = hiz;
    }

}
