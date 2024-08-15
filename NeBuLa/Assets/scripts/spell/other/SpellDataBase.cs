using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellDataBase", menuName = "SpellDataBase")]
public class SpellDataBase : ScriptableObject
{
    public List<SpellDBO> Spells;
}

[Serializable]
public class SpellDBO
{
    public SpellName Name;
    public GameObject GO;
}

public enum SpellName
{
    FireBall,
    IceSharp,
    Lightning,
    Shuriken,
    Kunai,
    ReapCzech,//bi��ek
    DarkPeak,//karanl���n zirvesi
    MysteriousShot,
    Drone
}