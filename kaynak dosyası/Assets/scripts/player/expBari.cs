using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expBari : MonoBehaviour
{
    [SerializeField] private Slider ExpBari;

    // Update is called once per frame
    void Update()
    {
        ExpBari.value = gameObject.GetComponent<player>().getExp();
        ExpBari.maxValue = gameObject.GetComponent<player>().getMaxExp();
    }
}
