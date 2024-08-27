using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [SerializeField] private float hasar;
    private int seviye = 1;

    private Transform hedef;
    [SerializeField] private float speed = 10f;
    private Vector3 m;

    // Start is called before the first frame update
    void Start()
    {
        if(hedef == null)
        {
            Destroy(gameObject);
        }
        hasar += seviye;

        Vector3 mesafe = hedef.position - transform.position;
        m = mesafe;

        StartCoroutine(sil(3f));
    }

    // Update is called once per frame
    void Update()
    {
        float mesafeKare = speed * Time.deltaTime;

        transform.Translate(m.normalized * mesafeKare);
    }

    public void HedefAta(Transform hedef)
    {
        this.hedef = hedef;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Enemy")
        {
            other.GetComponent<Enemy_Follow>().TakeDamage(hasar);
        }
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
}
