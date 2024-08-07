using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpell : spell
{
    [Header("DropSpell")]
    [SerializeField] protected float timer = 0f;
    protected Transform dropPos = null;

    public Transform DropPos {set => dropPos = value; }

    protected override void SpellStart()
    {
        base.SpellAwake();

        if(dropPos == null) dropPos = Player;
    }

    public override void Attack()
    {
        gameObject.SetActive(true);

        transform.position = dropPos.position;

        if (timer > 0) Invoke("Timer", timer);
    }

    /// <summary>
    /// Drop spellerinde varsa belirli bir süre sonra olmasý gereken oalylarý gerçekleþtirir.
    /// </summary>
    protected virtual void Timer()
    {
        gameObject.SetActive(false);
    }
}