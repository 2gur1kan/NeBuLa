using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ayarlar : MonoBehaviour
{
    [SerializeField] private GameObject Ayarlar;

    public void eris()
    {
        Ayarlar.SetActive(true);
        Time.timeScale = 0;
    }

    public void kapa()
    {
        Ayarlar.SetActive(false);
        Time.timeScale = 0;
    }
}
