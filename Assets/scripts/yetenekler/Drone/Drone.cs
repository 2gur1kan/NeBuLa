using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    private int seviye = 1;

    private Animator animator;
    private Rigidbody2D rb;

    public Transform merkez;
    public float speed = 4.5f;
    public int ebad = 2;

    //lazer atma
    [SerializeField] private GameObject LaserSablonu;
    private Transform hedef;
    private float AtisSuresi = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        takip();
        DusmaniSec();
        AtesEt();
    }

    void takip()
    {
        Vector3 mesafe = merkez.position - transform.position;
        float mesafeKare = speed * Time.deltaTime;

        if(mesafe.magnitude >= 20)
        {
            transform.Translate(mesafe.normalized * mesafeKare * 2);
        }
        else if (mesafe.magnitude >= 2)
        {
            transform.Translate(mesafe.normalized * mesafeKare);
        }
        else if (mesafe.magnitude >= 0.5)
        {
            transform.Translate(mesafe.normalized * mesafeKare / 2);
        }       

        if (mesafe.x > 0)
        {
            ChangeDirection(ebad);
        }
        else if (mesafe.x < 0)
        {
            ChangeDirection(-ebad);
        }

        animator.SetInteger("speed", Mathf.Abs((int)rb.velocity.x) + Mathf.Abs((int)rb.velocity.y));
    }

    void DusmaniSec()
    {
        GameObject[] dusmanlarArray = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> dusmanlar = new List<GameObject>();

        for (int i = 0; i < dusmanlarArray.Length; i++)
        {
            dusmanlar.Add(dusmanlarArray[i]);
        }

        float enYakinDusmanUzaklik = 5;
        GameObject enYakinDusman = null;
        hedef = null;

        foreach (var dusman in dusmanlar)
        {
            float dusmanaUzaklik = Vector3.Distance(transform.position, dusman.transform.position);

            if (dusmanaUzaklik < enYakinDusmanUzaklik)
            {
                enYakinDusmanUzaklik = dusmanaUzaklik;
                enYakinDusman = dusman;
            }
        }

        if (enYakinDusman != null)
        {
            hedef = enYakinDusman.transform;
        }
        else hedef = null;

    }

    void AtesEt()
    {
        if (AtisSuresi <= 0)
        {
            AtisSuresi = 5f;

            GameObject Laser = Instantiate(LaserSablonu,transform.position,transform.rotation);
            LaserFire laser = Laser.GetComponent<LaserFire>();

            if(laser != null)
            {
                laser.HedefAta(hedef);
                laser.setSeviye(seviye);
            }

        }
        else AtisSuresi -= Time.deltaTime;
    }

    void ChangeDirection(int direction)
    {
        Vector3 tempsScale = transform.localScale;
        tempsScale.x = direction;
        transform.localScale = tempsScale;
    }

    public void SetMerkez(Transform merkez)
    {
        this.merkez = merkez;
    }

    public void setSeviye(int seviye)
    {
        this.seviye = seviye;
    }
}
