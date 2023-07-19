using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPanel : MonoBehaviour
{
    [SerializeField] private GameObject elementPanel;

    // Start is called before the first frame update
    void Start()
    {
        elementPanel.SetActive(true);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void donus(string donus)
    {

        if(donus == "ates")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<YetenekAtisi>().setFireBallSeviyesi();

            elementPanel.SetActive(false);
            Time.timeScale = 1;
        }
        if(donus == "buz")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<YetenekAtisi>().setIce_SharpSeviyesi();

            elementPanel.SetActive(false);
            Time.timeScale = 1;
        }
        if(donus == "yildirim")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<YetenekAtisi>().setLightningSeviyesi();

            elementPanel.SetActive(false);
            Time.timeScale = 1;
        }

    }
}
