using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceArea : MonoBehaviour
{
    private int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(bekle(2f));
    }

    IEnumerator bekle(float sure)
    {
        yield return new WaitForSeconds(sure);
        Destroy(gameObject);
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
