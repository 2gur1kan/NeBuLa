using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zamanlayici : MonoBehaviour
{
    private Text sayac;
    private Enemy_Spawn spawn;
    private float saniyeSayac = 0;
    private int dakikaSayac = 0;

    private void Awake()
    {
        spawn = Enemy_Spawn.Instance;
        sayac = GameObject.FindGameObjectWithTag("zamanlayici").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        saniyeSayac += Time.deltaTime;

        if(saniyeSayac>= 60)
        {
            saniyeSayac = 0;
            dakikaSayac++;
            spawn.suruSpawn(dakikaSayac);
        }

        if (saniyeSayac < 10)
        {
            if(dakikaSayac < 10)
            {
                sayac.text = "0"+dakikaSayac.ToString() + ":" + "0" +((int)saniyeSayac).ToString();
            }
            else
            {
                sayac.text = dakikaSayac.ToString() + ":" + "0" +((int)saniyeSayac).ToString();
            }
        }
        else
        {
            if (dakikaSayac < 10)
            {
                sayac.text = "0" + dakikaSayac.ToString() + ":" + ((int)saniyeSayac).ToString();
            }
            else
            {
                sayac.text = dakikaSayac.ToString() + ":" + ((int)saniyeSayac).ToString();
            }
        }

       
    }

    public int getDakika()
    {
        return this.dakikaSayac;
    }
}
