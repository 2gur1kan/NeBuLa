using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpell : spell
{
    [Header("Area Spell")]
    [SerializeField] protected float tickForSec = 2;
    protected float timer = 0;

    [Header("-scale-")]
    [SerializeField] protected float StartSize = 1f;
    [SerializeField] protected bool UpdateScale = true;
    [SerializeField] protected float UpdateScaleRate = .1f;

    /////////////////////////////// gerekli þlev düzenlemeleri

    protected override void SpellStart()
    {
        base.SpellStart();

        // player a yapýþarak hareket eder
        transform.parent = player;

        SetSize();
    }

    protected override void SpellOnTriggerStay2D(Collider2D collision)
    {
        base.SpellOnTriggerStay2D(collision);

        if (collision.transform.tag == "Enemy" && isActive)
        {
            if (timer <= 0)
            {
                timer = 1/tickForSec;
                collision.GetComponent<Enemy>().TakeDamage(damage);
            }
            else timer -= Time.deltaTime;
        }
    }

    public override int LevelUp(int level)
    {
        if (UpdateScale) SetSize();

        return base.LevelUp(level);
    }

    //////////////////////////////////// class a özel iþlevler

    /// <summary>
    /// leveline göre boyuunu ayarlar
    /// </summary>
    protected void SetSize()
    {
        float s = StartSize + 1 * Mathf.Pow(UpdateScaleRate, level);

        transform.localScale = new Vector3(s,s,1);
    }

    /////////////////////// düzgün çalýþmasý için kapatýlmasý gerekn iþlevler

    public override void Attack()
    {
        // sürekli açýk olduðu için attack boþ olmalýdýr.
    }

    protected override void DestroySpell()
    {
        // ne olur ne olmaz bunuda kapattým :D
    }
}
