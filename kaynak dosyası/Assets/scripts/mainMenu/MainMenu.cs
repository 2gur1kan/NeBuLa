using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject ayarlar;
    [SerializeField] private GameObject hikaye;

    public void clicked(string deger)
    {
        if(deger == "basla")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (deger == "ayarlar")
        {
            mainMenu.SetActive(false);
            ayarlar.SetActive(true);
        }
        else if (deger == "geri")
        {
            hikaye.SetActive(false);
            ayarlar.SetActive(false);
            mainMenu.SetActive(true);
        }
        else if(deger == "hikaye")
        {
            hikaye.SetActive(true);
        }
        else if (deger == "cikis")
        {
            Application.Quit();
        }
    }
}
