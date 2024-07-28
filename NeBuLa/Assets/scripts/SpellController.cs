using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    [Header("element")]
    [SerializeField] private slot element;

    [Header("Slots")]
    [SerializeField] private List<slot> slots = new List<slot>(5);
    private int nextEmptySlotNum = 0;

    [Header("OtherValue")]
    [SerializeField] private float castScale = 1f;
    private SpellDataBaseController SDBC;

    private void Start()
    {
        SDBC = SpellDataBaseController.Instance;
    }

    /////////////////////////////// D��ar�dan eri�im i�in i�levler ////////////////////////

    /// <summary>
    /// elementin leveline g�re ��kt��� say�y� ayarlar
    /// </summary>
    public void SelectElement(SpellName SN)
    {
        if(element.spell.Count < element.spellLevel)
        {
           element.spell.Add(Instantiate(SDBC.SpellFinder(SN), transform.position, Quaternion.identity));
           element.name = SN;
        }

        UpgradeSpellLevel(element.spell, element.spellLevel);
    }

    /// <summary>
    /// se�ilen spellin kullan�ma ge�mesini veya geli�mesini sa�lar
    /// </summary>
    /// <param name="SN"></param>
    /// <returns></returns>
    public bool SelectSpell(SpellName SN)
    {
        foreach(slot gg in slots)
        {
            if (gg.name == SN)
            {
                gg.spellLevel++;
                UpgradeSpellLevel(gg.spell, gg.spellLevel);
                if(gg.spellLevel > 4) CheckUpgrade();
                return true;
            }
        }

        AddSpell(SN);

        if (nextEmptySlotNum > 5) return false;
        return true;
    }

    /// <summary>
    /// haz�rda olan bir spellin say�s�n� artt�rmak i�in kullan�l�r;
    /// bunun amac� level atlay�nca 2-3 tane olup ilerleyen yeteneklerin say�sn�n artmas�n� sa�lamak 
    /// </summary>
    /// <param name="SN"></param>
    public void AddSlotASpell(SpellName SN)
    {
        foreach (slot gg in slots)
        {
            if (gg.name == SN)
            {
                gg.spell.Add(Instantiate(SDBC.SpellFinder(SN), transform.position, Quaternion.identity));
                return;
            }
        }
    }

    /////////////////////////// alt i�levler ///////////////////////////////////

    /// <summary>
    /// levelleri default olarak 1 �st�ne ��kar�r
    /// </summary>
    private void UpgradeSpellLevel(List<GameObject> spells)
    {
        foreach (GameObject gg in spells)
        {
            gg.GetComponent<spell>().LevelUp();
        }
    }

    /// <summary>
    /// levelleri default olarak istenilen level e ��kar�r
    /// </summary>
    private void UpgradeSpellLevel(List<GameObject> spells, int level)
    {
        foreach (GameObject gg in spells)
        {
            gg.GetComponent<spell>().LevelUp(level);
        }
    }

    /// <summary>
    /// spelli kullan�ma ekleme k�sm� buradan ger�ekle�iyor
    /// </summary>
    private void AddSpell(SpellName SN)
    {
        slots[nextEmptySlotNum].spell.Add(Instantiate(SDBC.SpellFinder(SN),transform.position, Quaternion.identity));
        slots[nextEmptySlotNum].name = SN;

        nextEmptySlotNum++;
    }

    /// <summary>
    /// spellin upragede olma durumu kontrol edilecek. 
    /// Bu durum genelde 2 spellin 5 seviye olmas� ile yeni bir spell a��lmas� olay�d�r.
    /// bu spellerin geli�mesi durumuda ikisi 5 olduktan sonra yeni gelen eklentilerde bu ikisini birle�tiren eklentinin ��kmas� durumudur.
    /// </summary>
    private void CheckUpgrade()
    {
        //a��klamay� oku
    }
}

[Serializable]
public class slot
{
    public SpellName name;
    public List<GameObject> spell;
    public int spellLevel = 1;

    public float castTime;
}
