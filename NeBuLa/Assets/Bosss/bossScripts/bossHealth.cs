using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHealth : MonoBehaviour
{
    [SerializeField] private float maxCan = 100f;
    private float can;

    private void Start()
    {
        can = maxCan;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CanAzalt(float hasar)
    {
        can -= hasar;
    }

    public void setCan(float can)
    {
        this.can = can;
    }

    public float getCan()
    {
        return can;
    }
}
