using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float hasar = 2;
    private int seviye;

    private float beklemesuresi = 0;
    [SerializeField] private float beklemesuresisifirla = 0.1f;

    void Start()
    {
        seviye = gameObject.GetComponent<Enemy_Follow>().getSeviye();
        hasar += seviye * 0.2f;
    }

    private void Update()
    {
        if (beklemesuresi > 0) beklemesuresi -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(beklemesuresi <= 0)
            {
                beklemesuresi = beklemesuresisifirla;
                collision.GetComponent<player>().HasarAl(hasar);
            }

        }
    }
}
