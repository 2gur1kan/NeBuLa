using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNum : MonoBehaviour
{
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    [SerializeField] private GameObject button3;

    private AudioSource Audio;

    [SerializeField] private int MaxSeviye = 6;
    private GameObject player;
    private List<int> skills;
    private List<int> sec;
    private int Num;
    private bool degis = true;

    private int KaranliginZirvesiSeviyesi = 0;
    private int BicCekSeviyesi = 0;
    private int KunaiSeviyesi = 0;
    private int ShurikenSeviyesi = 0;
    private int GizemliAtisSeviyesi = 0;
    private int DroneSeviyesi = 0;

    private void Awake()
    {
        ListeyiDuzenle();
        player = GameObject.FindGameObjectWithTag("Player");
        Audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (degis)
        {
            degis = false;
            RandomNum();
        }

        ListeyiDuzenle();
    }

    void RandomNum()
    {
        Audio.Play();

        sec = new List<int>();
        sec = skills;

        Num = Random.Range(0, sec.Count);
        button1.GetComponent<buttonEdit>().setNum(sec[Num]);
        sec.Remove(sec[Num]);

        Num = Random.Range(0, sec.Count);
        button2.GetComponent<buttonEdit>().setNum(sec[Num]);
        sec.Remove(sec[Num]);

        Num = Random.Range(0, sec.Count);
        button3.GetComponent<buttonEdit>().setNum(sec[Num]); 
        sec.Remove(sec[Num]);
    }

    public void setDegis()
    {
        this.degis = true;
        gameObject.SetActive(true);    
    }

    void ListeyiDuzenle()
    {
        skills = new List<int>();

        //BicCekYeri = 0;
        //skills.Add(0);

        //KunaiYeri = 1;
        skills.Add(1);

        //ShurikenYeri = 2;
        skills.Add(2);

        // KaranliginZirvesiYeri = 3;
        skills.Add(3);

        //DroneYeri = 4;
        skills.Add(4);

        //GizemliAtisYeri = 5;
        skills.Add(5);

        if (BicCekSeviyesi <= MaxSeviye)
        {
            skills.Remove(0);
        }
        else if (KunaiSeviyesi <= MaxSeviye/2)
        {
            skills.Remove(1);
        }
        else if (ShurikenSeviyesi <= MaxSeviye/2)
        {
            skills.Remove(2);
        }
        else if (KaranliginZirvesiSeviyesi <= MaxSeviye)
        {
            skills.Remove(3);
        }
        else if (DroneSeviyesi <= MaxSeviye)
        {
            skills.Remove(4);
        }
        else if (GizemliAtisSeviyesi <= MaxSeviye)
        {
            skills.Remove(5);
        }

        if(ShurikenSeviyesi >= MaxSeviye/2 && KunaiSeviyesi >= MaxSeviye / 2)
        {
            skills.Add(0);
        }
    }
}
