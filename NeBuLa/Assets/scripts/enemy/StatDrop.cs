using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatDrop : MonoBehaviour
{
    [SerializeField] private GameObject drop;

    public void Drop()
    {
        int num = Random.RandomRange(0, 100);

        if (num > 95)
        {
            Instantiate(drop, transform.position, transform.rotation);
        }  
    }
}
