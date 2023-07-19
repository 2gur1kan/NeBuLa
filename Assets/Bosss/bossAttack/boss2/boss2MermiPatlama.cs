using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2MermiPatlama : MonoBehaviour
{
    private bool kucul = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(bekle(3f));

        StartCoroutine(sil(6.5f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), 0.5f);

        if (kucul)
        {
            transform.localScale -= transform.localScale * 0.002f;
        }
        else
        {
            transform.localScale += transform.localScale * 0.002f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {

        }
    }

    IEnumerator bekle(float sure)
    {
        yield return new WaitForSeconds(sure);
        kucul = true;
    }

    IEnumerator sil(float sure)
    {
        yield return new WaitForSeconds(sure);
        Destroy(gameObject);
    }
}
