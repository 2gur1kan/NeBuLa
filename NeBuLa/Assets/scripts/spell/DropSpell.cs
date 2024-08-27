using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpell : spell
{
    protected override void SpellStart()
    {
        base.SpellStart();

        if (dropPos == null) dropPos = player;
    }
}