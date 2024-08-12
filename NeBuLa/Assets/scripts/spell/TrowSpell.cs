using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrowSpell : spell
{
    [Header("Move")]
    [SerializeField] protected float speed = 8f;
    protected Vector2 MoveForRB;

    protected Vector3 targetPos;
    protected Vector3 thisPos;

    ////////////////////////////// var olan i�levler

    protected override void SpellAwake()
    {
        base.SpellAwake();

        MoveForRB = new Vector2();
        MoveForRB = Vector2.zero;
    }

    protected override void SpellUpdate()
    {
        if(MoveForRB != Vector2.zero) rb.velocity = MoveForRB;
    }

    public override void Attack()
    {
        MoveForRB = Vector2.zero;
        target = EnemysObject.SelectNearestEnemy(player.transform.position, radius);

        if (target == null || target.x == Mathf.NegativeInfinity)
        {
            RestartAttackTimer();
            return;
        }

        targetPos = target;

        thisPos = player.position;
        transform.position = thisPos;

        SelectDirection(targetPos, thisPos);

        MoveForRB = CalculateMove();

        ActivateSpell();

        restartAttack();
    }

    /////////////////////// hareket etme

    /// <summary>
    /// hareket edece�i y�n� ve b�y�kl���n� hesaplar
    /// </summary>
    /// <returns></returns>
    protected Vector2 CalculateMove()
    {
        if (targetPos == Vector3.negativeInfinity) return Vector2.zero;

        Vector3 distance = targetPos - thisPos;
        float CalculateSpeed = speed;
        return new Vector2(distance.normalized.x * CalculateSpeed, distance.normalized.y * CalculateSpeed);
    }

    ////////////////////// Y�n bulma i�levleri
    
    /// <summary>
    /// at���n bakaca�� y�n� belirler
    /// </summary>
    protected void SelectDirection(Vector3 h, Vector3 m)
    {
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
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // A��ya g�re rotasyon ayar�
    }

    protected float DirectionFinder(Vector3 direciton, float add)
    {
        float angle = direciton.y / (direciton.x + direciton.y);// A�� hesaplamas�
        angle *= 90;
        angle += add;
        return angle;
    }

    /////////////////////// gerekli i�levler
    
    /// <summary>
    /// at�lan cismin geri d�nmeis gerekti�inde kullan�lacak temel fonksiyon
    /// </summary>
    protected void SwitchPos()
    {
        Vector3 gg = thisPos;
        thisPos = targetPos;
        targetPos = gg;
    }
}
