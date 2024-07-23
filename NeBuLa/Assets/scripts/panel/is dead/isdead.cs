using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class isdead : MonoBehaviour
{
    [SerializeField] private GameObject deadMenu;

    public void dead()
    {
        Time.timeScale = 0;
        deadMenu.SetActive(true);  
    }

    public void sec(string durum)
    {
        if(durum == "basla")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(durum == "cikis")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
