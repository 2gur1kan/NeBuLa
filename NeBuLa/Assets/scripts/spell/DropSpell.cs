using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpell : spell
{
    [Header("DropSpell")]
    protected Transform dropPos = null;

    public Transform DropPos {set => dropPos = value; }

    protected override void SpellStart()
    {
        base.SpellStart();

        if (dropPos == null) dropPos = player;
    }

    protected override void SpellOnTriggerEnter2D(Collider2D collision)
    {
        base.SpellOnTriggerEnter2D(collision);
    }

    protected override void DestroySpell()
    {
        // buras� bo� ��nk� yok olma olay� attack da ger�ekle�iyor
    }

    public override void Attack()
    {
        setActiveTrue();

        if (dropPos == null) dropPos = player;
        transform.position = dropPos.position;

        Invoke("setActivateFalse", spellExistenceTime);
    }
}