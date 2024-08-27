using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2DusecekKonum : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(sil());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator sil() 
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
