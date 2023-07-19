using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBari : MonoBehaviour
{
    private float CanHavuzu;
    private float Can;

    // Update is called once per frame
    void Update()
    {
        CanBariAyari(Can/CanHavuzu);
    }

    public void CanAta(float CanHavuzu,float Can)
    {
        this.CanHavuzu = CanHavuzu;
        this.Can = Can;
    }

    void CanBariAyari(float canyuzdesi)
    {
        Vector3 c = transform.localScale;
        c.x = canyuzdesi/4;
        transform.localScale = c;
    }

}
