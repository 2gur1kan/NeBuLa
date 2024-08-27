using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAreaSpell : spell
{
    protected override void SpellStart()
    {
        base.SpellStart();

        if (dropPos == null) dropPos = player;
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
