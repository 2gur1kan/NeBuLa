using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Sharp : MonoBehaviour
{
    [SerializeField] private float hasar;
    private int seviye = 1;

    [SerializeField] private GameObject IceAreaSablonu;
    private int IceAreaDamage = 1;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        hasar += seviye * 4;
        anim = GetComponent<Animator>();
        StartCoroutine(bekle(15f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Enemy")
        {
            anim.SetBool("patla", true);
            StartCoroutine(patla(2f));
            other.GetComponent<Enemy_Follow>().TakeDamage(hasar);

            GameObject IceArea = Instantiate(IceAreaSablonu, transform.position, transform.rotation);
            IceArea icearea = GetComponent<IceArea>();

            if (icearea != null)
            {
                icearea.SetDamage(IceAreaDamage);
            }
        }
    }

    IEnumerator patla(float sure)
    {
        yield return new WaitForSeconds(sure);

        Destroy(gameObject);
    }
    IEnumerator bekle(float sure)
    {
        yield return new WaitForSeconds(sure);
        anim.SetBool("patla", true);        
        StartCoroutine(patla(2f));
    }

    public void setSeviye(int seviye)
    {
        this.seviye = seviye;
    }
}
