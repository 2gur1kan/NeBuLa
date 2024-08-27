using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage = 2;
    private int level;

    private float timer = 0;
    [SerializeField] private float cast = 0.1f;

    void Start()
    {
        level = gameObject.GetComponent<Enemy_Follow>().Level;
        damage += level * 0.2f;
    }

    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(timer <= 0)
            {
                timer = cast;
                collision.GetComponent<player>().TakeDamage(damage);
            }

        }
    }
}
