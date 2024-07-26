using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    [Header("element")]
    [SerializeField] private GameObject element;

    [Header("Slots")]
    [SerializeField] private slot slot_1;
    [SerializeField] private slot slot_2;
    [SerializeField] private slot slot_3;
    [SerializeField] private slot slot_4;
    [SerializeField] private slot slot_5;

    [Header("OtherValue")]
    [SerializeField] private float castScale = 1f;



}

[Serializable]
public class slot
{
    public List<GameObject> spell;
    public int spellLevel;

    public float castTime;
}
