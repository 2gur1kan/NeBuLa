using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaranliginZirvesi : MonoBehaviour
{
    [SerializeField] private int size = 6;
    [SerializeField] private float hasar = 0;
    private int seviye = 1;
    private int oncekiseviye = 0;

    private float bekle;

    private Transform merkez;

    // Update is called once per frame
    void Update()
    {
        if(oncekiseviye != seviye)
        {
            SetSize(size + seviye);
            hasar += seviye * 0.5f;
            oncekiseviye = seviye;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            if (bekle <= 0)
            {
                bekle = 0.5f;
                collision.GetComponent<Enemy_Follow>().TakeDamage(hasar);
            }
            else bekle -= Time.deltaTime;
        }
    }

    public void SetMerkez(Transform merkez)
    {
        this.merkez = merkez;
    }

    void SetSize(float size_i)
    {
        Vector3 size = transform.localScale;
        size.x = size_i;
        size.y = size_i;
        transform.localScale = size;
    }

    public void setSeviye(int seviye)
    {
        this.seviye = seviye;
    }
}
