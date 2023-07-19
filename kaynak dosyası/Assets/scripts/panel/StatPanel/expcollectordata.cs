using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expcollectordata : MonoBehaviour
{
    private float speed = 3;
    private float range = 3;

    public void setSpeed()
    {
        this.speed += 1f;
    }
    public void setRange()
    {
        this.range += 1f;
    }

    public float getSpeed()
    {
        return this.speed;
    }

    public float getRange()
    {
        return this.range;
    }
}
