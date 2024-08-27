using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    public static Enemy_Spawn Instance;

    [Header("Wawe Value")]
    [SerializeField] List<EnemyWaweValue> EnemyWaweList;
    [SerializeField] private List<GameObject> Enemys;
    private int CurrentWawe = 0;
    private int level = 1;
    private GameObject EnemyObject;

    [Header("Spawn")]
    [SerializeField] private float distance = 20;
    private Transform player;
    [SerializeField] private float timerResetValue = 0.5f;
    private float timer = 0f;

    [Header("Old System")]
    [SerializeField] private GameObject[] suru;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        setEnemys();

        EnemyObject = new GameObject("EnemysObject");
        EnemyObject.tag = "EnemysObject";
        EnemyObject.AddComponent<Enemy_Array>();
    }

    private void Update()
    {
        Spawner();
    }

    /////////////////////////////////// spawn fonc

    /// <summary>
    /// düþmanlarý süreye dayalý oluþturur
    /// </summary>
    private void Spawner()
    {
        if (timer <= 0) spawn();
        else timer -= Time.deltaTime;
    }

    /// <summary>
    /// listeden ratgele bir düþmaný seviye kadar rastgele bir yere çýkarýr
    /// </summary>
    private void spawn()
    {
        timer = timerResetValue;

        Vector3 point = GetRandomPointAroundCharacter();
        GameObject enemy = Enemys[UnityEngine.Random.Range(0, Enemys.Count)];
        int gg = level;

        while (gg > 0)
        {
            Instantiate(enemy, point, Quaternion.identity).transform.parent = EnemyObject.transform;

            gg--;
        }
    }

    /// <summary>
    /// düþman oluþturulacak rastgele bir nokta belirler
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRandomPointAroundCharacter()
    {
        float angle = UnityEngine.Random.Range(0f, 360f);
        angle *= Mathf.Deg2Rad;

        float x = Mathf.Cos(angle) * distance;
        float y = Mathf.Sin(angle) * distance;

        Vector3 randomPoint = new Vector3(x, y, 0) + player.position;
        return randomPoint;
    }

    /// <summary>
    /// sürü oluþturur
    /// </summary>
    /// <param name="dakika"></param>
    public void suruSpawn(int dakika)
    {
        GameObject Suru = Instantiate(suru[UnityEngine.Random.Range(0, suru.Length)], GetRandomPointAroundCharacter(), Quaternion.identity);
        Suru.GetComponent<suru>().setSuruSayisi(10 + (dakika * 5));
    }

    /////////////////////////////////////////////// level designer

    /// <summary>
    /// level atladýðýnda olmasý gereken iþlevleri çaðýrýr
    /// </summary>
    public void LevelUp()
    {
        level++;

        setEnemys();
        setResetTimer(0.05f);
    }

    /// <summary>
    /// Oluþacak düþmanlarý düzenler
    /// </summary>
    private void setEnemys()
    {
        if (CurrentWawe + 1 > EnemyWaweList.Count) return;

        if (EnemyWaweList[CurrentWawe + 1].startLevel == level)
        {
            CurrentWawe++;
            Enemys = EnemyWaweList[CurrentWawe].WaweEnemyList;
        }
    }

    /// <summary>
    /// bekleme süresini azaltýr
    /// </summary>
    /// <param name="azalt"></param>
    private void setResetTimer(float azalt)
    {
        if(timerResetValue > 0.1f)
        {
            timerResetValue -= azalt;
        }
    }
}

[Serializable]
public class EnemyWaweValue
{
    public List<GameObject> WaweEnemyList;
    public int startLevel = 0;
}
