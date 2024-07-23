using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausePanel : MonoBehaviour
{
    [SerializeField] private GameObject pause;
    private bool durdu = false;

    void Start()
    {
        pause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (durdu)
            {
                pause.SetActive(false);
                Time.timeScale = 1;
                durdu = false;
            }
            else
            {
                pause.SetActive(true);
                Time.timeScale = 0;
                durdu = true;
            }
        }
    }

    public void aktifEt(string durum)
    {
        if (durum == "pause")
        {
            pause.SetActive(true);
            Time.timeScale = 0;
        }
        else if(durum == "devam")
        {
            pause.SetActive(false);
            Time.timeScale = 1;
        }
        else if(durum == "ayarlar")
        {
            pause.SetActive(false);
        }
        else if (durum == "cikis")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
