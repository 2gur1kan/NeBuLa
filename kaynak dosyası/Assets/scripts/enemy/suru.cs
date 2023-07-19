using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suru : MonoBehaviour
{
    [SerializeField] private int suruSayisi = 10;
    void Start()
    {
        if (suruSayisi > 0)
        {

            float y = Random.RandomRange(-2f, 2f);
            float x = Random.RandomRange(-2f, 2f);

            Vector3 n = transform.position;

            n.x += x;
            n.y += y;

            GameObject z = Instantiate(gameObject, n, transform.rotation);
            suru Z = z.GetComponent<suru>();
            Z.setSuruSayisi(suruSayisi - 1);
        }

    }

    public void setSuruSayisi(int suruSayisi)
    {
        this.suruSayisi = suruSayisi;
    }
}
