using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDataBaseController : MonoBehaviour
{
    public static SpellDataBaseController Instance;

    [SerializeField] private SpellDataBase SDB;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// data baseden istenilen saldýrýya eriþimi saðlar
    /// </summary>
    /// <returns></returns>
    public GameObject SpellFinder(SpellName Sn)
    {
        foreach (SpellDBO spell in SDB.Spells)
        {
            if(spell.Name == Sn)
            {
                return spell.GO;
            }
        }

        return null;
    }
}
