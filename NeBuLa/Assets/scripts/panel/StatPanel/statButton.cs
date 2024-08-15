using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statButton : MonoBehaviour
{
    [SerializeField] private GameObject statPanel;
    private GameObject camera;
    private GameObject player;
    private int Num = 0;

    [SerializeField] private Sprite[] resim; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Button>().image.sprite = resim[Num];
    }

    public void clicked()
    {
        if (Num == 0)
        {
            player.GetComponent<player>().AddHealth();
        }
        else if (Num == 1)
        {
            player.GetComponent<player>().heal();
        }
        else if (Num == 2)
        {
            player.GetComponent<player>().playerSpeedAdd();
            statPanel.GetComponent<StatPanel>().setPlayerSpeedSayac();
        }
        else if (Num == 3)
        {
            LevelController.Instance.Range++;
            statPanel.GetComponent<StatPanel>().setExpCollectorRange();
        }
        else if (Num == 4)
        {
            LevelController.Instance.Speed++;
            statPanel.GetComponent<StatPanel>().setExpCollectorSpeed();
        }

        Time.timeScale = 1;
        statPanel.SetActive(false);
    }

    public void setNum(int Num)
    {
        this.Num = Num;
    }
}
