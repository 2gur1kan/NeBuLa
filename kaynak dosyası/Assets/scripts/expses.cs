using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expses : MonoBehaviour
{
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio= GetComponent<AudioSource>();
    }

    public void expSes()
    {
        audio.Play();
    }
}
