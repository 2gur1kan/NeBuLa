using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    [SerializeField] GameObject[] Light;
    [SerializeField] GameObject[] Medium;
    [SerializeField] GameObject[] heavy;

    [SerializeField] private List<GameObject> Enemys;
    [SerializeField] private Transform[] SpawnPoints;

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject[] suru;

    private GameObject player;
    private GameObject EnemyObject;
    private int seviye = 1;
    private int oncekiSeviye = 1;

    private float baseBeklmeSuresi = 0.5f;
    [SerializeField] private float beklemesuresiSifirla = 0.5f;
    private float beklemesuresi = 0f;
    private bool boss = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        setEnemys();

        EnemyObject = new GameObject("EnemysObject");
        EnemyObject.tag = "EnemysObject";
        EnemyObject.AddComponent<Enemy_Array>();
    }

    private void Update()
    {
        seviye  = player.GetComponent<player>().getSeviye();

        if(oncekiSeviye < seviye)
        {
            oncekiSeviye++;
            setEnemys();
            setBeklemeSuresiSifirla(0.06f);
        }

        spawn();
    }

    /// <summary>
    /// Ouþacak düþmanlarý düzenler
    /// </summary>
    void setEnemys()
    {
        Enemys = new List<GameObject>();

        if (seviye < 3)
        {
            for (int i = 0; i < Light.Length; i++)
            {
                Enemys.Add(Light[i]);
            }
        }
        else if(seviye < 5)
        {
            for (int i = 0; i < Light.Length; i++)
            {
                Enemys.Add(Light[i]);
            }
            for (int i = 0; i < Medium.Length; i++)
            {
                Enemys.Add(Medium[i]);
            }
        }
        else if(seviye < 8)
        {
            for (int i = 0; i < heavy.Length; i++)
            {
                Enemys.Add(heavy[i]);
            }
            for (int i = 0; i < Medium.Length; i++)
            {
                Enemys.Add(Medium[i]);
            }
        }
        else
        {
            for (int i = 0; i < heavy.Length; i++)
            {
                Enemys.Add(heavy[i]);
            }
        }

    }

    void spawn()
    {
        if (beklemesuresi <= 0)
        {
            beklemesuresi = beklemesuresiSifirla;

            Instantiate(Enemys[Random.Range(0, Enemys.Count)], SpawnPoints[Random.Range(0, SpawnPoints.Length)].position, Quaternion.identity).transform.parent = EnemyObject.transform;

        }
        else beklemesuresi -= Time.deltaTime;
    }

    public void suruSpawn(int dakika)
    {
        GameObject Suru = Instantiate(suru[Random.Range(0, suru.Length)], SpawnPoints[Random.Range(0, SpawnPoints.Length)].position, Quaternion.identity);
        Suru.GetComponent<suru>().setSuruSayisi(10 + (dakika * 5));
    }

    void setBeklemeSuresiSifirla(float azalt)
    {
        if(this.beklemesuresiSifirla > 0.01f)
        {
            if (!boss) this.beklemesuresiSifirla -= azalt;
            else this.baseBeklmeSuresi -= azalt;
        }
    }

    public void resetBeklemeSuresi(bool boss)
    {
        this.boss = boss;
        float a = beklemesuresiSifirla;
        beklemesuresiSifirla = baseBeklmeSuresi;
        baseBeklmeSuresi = a;
    }
}
