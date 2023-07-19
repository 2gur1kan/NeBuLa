using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolun : MonoBehaviour
{
    [SerializeField] private GameObject yaratik;

    [SerializeField] private int yaratiksayisi = 2;
    private bool yaratýkcikti = true;

    public void yaratikCikar()
    {
        if (yaratýkcikti)
        {
            for(int i = 0; i<yaratiksayisi; i++)
            {
                Vector3 Pozisyonu = transform.position;

                float randUzaklikX = Random.RandomRange(-1.5f, 1.5f);
                float randUzaklikY = Random.RandomRange(-1.5f, 1.5f);

                Pozisyonu.x += randUzaklikX;
                Pozisyonu.y += randUzaklikY;

                Instantiate(yaratik, Pozisyonu, transform.rotation);
            }
        }
    }
}
