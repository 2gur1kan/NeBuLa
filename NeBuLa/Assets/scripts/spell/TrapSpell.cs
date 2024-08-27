using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpell : spell
{
    protected override void SpellStart()
    {
        base.SpellStart();

        if (dropPos == null) dropPos = player;
    }

    public override void Attack()
    {
        if(dropPos != null) transform.position = dropPos.position;

        ActivateSpell();

        restartAttack();
    }
}
