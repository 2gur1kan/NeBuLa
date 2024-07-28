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

    /////////////////////////////// Dýþarýdan eriþim için iþlevler ////////////////////////

    /// <summary>
    /// elementin leveline göre çýktýðý sayýyý ayarlar
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
    /// seçilen spellin kullanýma geçmesini veya geliþmesini saðlar
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
    /// hazýrda olan bir spellin sayýsýný arttýrmak için kullanýlýr;
    /// bunun amacý level atlayýnca 2-3 tane olup ilerleyen yeteneklerin sayýsnýn artmasýný saðlamak 
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

    /////////////////////////// alt iþlevler ///////////////////////////////////

    /// <summary>
    /// levelleri default olarak 1 üstüne çýkarýr
    /// </summary>
    private void UpgradeSpellLevel(List<GameObject> spells)
    {
        foreach (GameObject gg in spells)
        {
            gg.GetComponent<spell>().LevelUp();
        }
    }

    /// <summary>
    /// levelleri default olarak istenilen level e çýkarýr
    /// </summary>
    private void UpgradeSpellLevel(List<GameObject> spells, int level)
    {
        foreach (GameObject gg in spells)
        {
            gg.GetComponent<spell>().LevelUp(level);
        }
    }

    /// <summary>
    /// spelli kullanýma ekleme kýsmý buradan gerçekleþiyor
    /// </summary>
    private void AddSpell(SpellName SN)
    {
        slots[nextEmptySlotNum].spell.Add(Instantiate(SDBC.SpellFinder(SN),transform.position, Quaternion.identity));
        slots[nextEmptySlotNum].name = SN;

        nextEmptySlotNum++;
    }

    /// <summary>
    /// spellin upragede olma durumu kontrol edilecek. 
    /// Bu durum genelde 2 spellin 5 seviye olmasý ile yeni bir spell açýlmasý olayýdýr.
    /// bu spellerin geliþmesi durumuda ikisi 5 olduktan sonra yeni gelen eklentilerde bu ikisini birleþtiren eklentinin çýkmasý durumudur.
    /// </summary>
    private void CheckUpgrade()
    {
        //açýklamayý oku
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
