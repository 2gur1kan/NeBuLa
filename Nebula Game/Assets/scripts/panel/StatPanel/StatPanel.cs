using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPanel : MonoBehaviour
{
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    [SerializeField] private GameObject button3;

    [SerializeField] private int MaxSeviye = 6;
    private GameObject player;
    private List<int> stat;
    private List<int> sec;
    private int Num;
    private bool degis = true;

    private int playerspeedsayac = 0;
    private int expcollectorrange = 0;
    private int expcollectorspeed = 0;

    private void Awake()
    {
        ListeyiDuzenle();
        player = GameObject.FindGameObjectWithTag("Player");
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
        sec = new List<int>();
        sec = stat;

        Num = Random.Range(0, sec.Count);
        button1.GetComponent<statButton>().setNum(sec[Num]);
        sec.Remove(sec[Num]);

        Num = Random.Range(0, sec.Count);
        button2.GetComponent<statButton>().setNum(sec[Num]);
        sec.Remove(sec[Num]);

        Num = Random.Range(0, sec.Count);
        button3.GetComponent<statButton>().setNum(sec[Num]);
        sec.Remove(sec[Num]);
    }

    public void setDegis()
    {
        this.degis = true;
        gameObject.SetActive(true);
    }

    void ListeyiDuzenle()
    {
        stat = new List<int>();

        //can
        stat.Add(0);

        //maxcan
        stat.Add(1);

        //playerspeed
        stat.Add(2);

        //collector range
        stat.Add(3);

        //collector speed
        stat.Add(4);

        if (expcollectorspeed > 3)
        {
            stat.Remove(4);
        }
        
        if(expcollectorrange > 6)
        {
            stat.Remove(3);
            stat.Add(0);
        }
        
        if(playerspeedsayac > 5)
        {
            stat.Remove(2);
        }    
    }

    public void setPlayerSpeedSayac()
    {
        this.playerspeedsayac++;
    }
    public void setExpCollectorRange()
    {
        this.expcollectorrange++;
    }
    public void setExpCollectorSpeed()
    {
        this.expcollectorspeed++;
    }

}
