using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] private float hasar;
    private int seviye = 1;

    public Transform hedef;
    private Animator animator;

    private float sure = 0f;

    void Awake()
    {
        hasar += seviye;
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(hedef.localPosition - transform.localPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (hedef == null)
        {
            Destroy(gameObject);
            return;
        }

        sure += 1*Time.deltaTime;

        if (sure > 0.2f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.GetComponent<Enemy_Follow>().TakeDamage(hasar);
        }
    }

    public void HedefAta(Transform hedef1)
    {
        hedef = hedef1;
    }

    public void setSeviye(int seviye)
    {
        this.seviye = seviye;
    }
}
