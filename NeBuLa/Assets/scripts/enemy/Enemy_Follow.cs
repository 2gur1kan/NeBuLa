using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Follow : Enemy
{
    protected override void EnemyStart()
    {
        base.EnemyStart();

        InvokeRepeating("Follow", .1f, .2f);
    }

    void Follow()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > 30)
        {
            Destroy();
        }
        if (CurrentHealth <= 0)
        {
            StartCoroutine(dead());
        }
        else if (!stun)
        {
            Vector2 y�n = player.transform.position - transform.position;
            y�n.Normalize();

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            if (y�n.x > 0)
            {
                ChangeDirection(size);
            }
            else if (y�n.x < 0)
            {
                ChangeDirection(-size);
            }
        }       
    }

    IEnumerator dead()
    {
        animator.SetBool("DEAD", true);
        yield return new WaitForSeconds(0.4f);
        Destroy();
    }

    protected override void Destroy() ///////////////////////////////////////////////////////// buras� belki s�k�nt� ��karabilir
    {
        Drop();
        CancelInvoke("Follow");
        Destroy(gameObject);
    }

    void ChangeDirection(float direction)
    {
        Vector3 tempsScale = transform.localScale;
        tempsScale.x = direction;
        transform.localScale = tempsScale;
    }

}
