using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrowSpell : spell
{
    protected Transform targetPos;
    protected Transform thisPos;

    protected override void SpellStart()
    {
        base.SpellStart();

        targetPos = target.transform;
        thisPos = Player;
    }

    protected override void SpellUpdate()
    {
        rb.velocity = MoveForRB;
    }

    public override void Attack()
    {
        gameObject.SetActive(true);

        transform.position = thisPos.position;
        SelectDirection();
        Vector3 distance = targetPos.position - thisPos.position;
        float CalculateSpeed = speed * Time.deltaTime;
        MoveForRB = new Vector2(distance.normalized.x * CalculateSpeed, distance.normalized.y * CalculateSpeed);

        restartAttack();
    }

    ////////////////////// Y�n bulma i�levleri
    protected void SelectDirection()
    {
        Vector3 h = target.position;
        Vector3 m = transform.position;
        Vector3 g = h - m;

        if (h.x > m.x && h.y > m.y)
        {
            DirectionAssign(DirectionFinder(g, 0f));
        }
        else if (h.x < m.x && h.y > m.y)
        {
            g = new Vector3(m.y - h.y, h.x - m.x, m.z);
            DirectionAssign(DirectionFinder(g, 90f));
        }
        else if (h.x < m.x && h.y < m.y)
        {
            g = m - h;
            DirectionAssign(DirectionFinder(g, 180f));
        }
        else
        {
            g = new Vector3(h.x - m.x, m.y - h.y, m.z);
            DirectionAssign(DirectionFinder(g, -90f));
        }
    }

    protected void DirectionAssign(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle); // A��ya g�re rotasyon ayar�
    }

    protected float DirectionFinder(Vector3 yon, float add)
    {
        float angle = Mathf.Atan2(yon.y, yon.x) * Mathf.Rad2Deg; // A�� hesaplamas�
        angle += add;
        return angle;
    }

    /////////////////////// gerekli i�levler
    
    /// <summary>
    /// at�lan cismin geri d�nmeis gerekti�inde kullan�lacak temel fonksiyon
    /// </summary>
    protected void SwitchPos()
    {
        Transform gg = thisPos;
        thisPos = targetPos;
        targetPos = gg;
    }
}
