using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    [Header("Level Values")]
    [SerializeField] private long desiredExp = 10;
    private long CurrentExp = 0;
    [SerializeField] private float expScale = 2f;

    [Header("Other")]
    [SerializeField] private Slider ExpBari;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// sisteme exp eklemeye yarar
    /// </summary>
    /// <param name="exp"></param>
    public void AddExp(int exp)
    {
        CurrentExp += exp;

        if (CurrentExp >= desiredExp) LevelUp();

        SetExpBar();
    }

    /// <summary>
    /// level atlaýnca gerçekleþecek iþlevleri çalýþtýrýr
    /// </summary>
    private void LevelUp()
    {
        CurrentExp -= desiredExp;
        desiredExp = (long)((double)desiredExp * expScale);



        player.Instance.LevelUp();
        Enemy_Spawn.Instance.LevelUp();
    }

    private void SetExpBar()
    {
        ExpBari.value = CurrentExp;
        ExpBari.maxValue = desiredExp;
    }

    ///////////////// exp collector controller

    [Header("Exp Collector Value")]
    [SerializeField] private float range = 3;
    [SerializeField] private float speed = 3;

    public float Range { get => range; set => range = value; }
    public float Speed { get => speed; set => speed = value; }
}
