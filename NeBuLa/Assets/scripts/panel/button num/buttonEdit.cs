
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class buttonEdit : MonoBehaviour
{
    [SerializeField] private GameObject levelPanel;
    private GameObject player;
    private int Num = -1;

    [SerializeField] public Sprite BicCek;
    [SerializeField] public Sprite Kunai;
    [SerializeField] public Sprite Shuriken;
    [SerializeField] public Sprite KaranliginZirvesi;
    [SerializeField] public Sprite Drone;   
    [SerializeField] public Sprite GizemliAtis;

    // Start is called before the first frame update
    void Start()
    {
        levelPanel = GameObject.FindGameObjectWithTag("LevelPanel");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Num == 0)
        {
            gameObject.GetComponent<Button>().image.sprite = BicCek;
        }
        else if(Num == 1)
        {
            gameObject.GetComponent<Button>().image.sprite = Kunai;
        }
        else if(Num == 2)
        {
            gameObject.GetComponent<Button>().image.sprite = Shuriken;
        }
        else if(Num == 3)
        {
            gameObject.GetComponent<Button>().image.sprite = KaranliginZirvesi;
        }
        else if(Num == 4)
        {
            gameObject.GetComponent<Button>().image.sprite = Drone;
        }
        else if(Num == 5)
        {
            gameObject.GetComponent<Button>().image.sprite = GizemliAtis;
        }
    }

    public void clicked()
    {
        if (Num == 0)
        {
            //player.GetComponent<YetenekAtisi>().BicCekAc();
            //player.GetComponent<YetenekAtisi>().setBicCekSeviyesi();
        }
        else if (Num == 1)
        {
            //player.GetComponent<YetenekAtisi>().setKunaiSeviyesi();
        }
        else if (Num == 2)
        {
            //player.GetComponent<YetenekAtisi>().setShurikenSeviyesi();
        }
        else if (Num == 3)
        {
            //player.GetComponent<YetenekAtisi>().setKaranliginZirvesiSeviyesi();
        }
        else if (Num == 4)
        {
            //player.GetComponent<YetenekAtisi>().setDroneSeviyesi();
        }
        else if (Num == 5)
        {
            // player.GetComponent<YetenekAtisi>().setGizemliAtisSeviyesi();
        }

        Time.timeScale = 1;
        levelPanel.SetActive(false);
    }

    public void setNum (int Num)
    {
        this.Num = Num;
    } 
}
