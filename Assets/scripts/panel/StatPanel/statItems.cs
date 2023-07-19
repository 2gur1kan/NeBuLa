using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statItems : MonoBehaviour
{
    [SerializeField] private Sprite[] resim;

    [SerializeField] private GameObject Camera;

    private int num;

    // Start is called before the first frame update
    void Start()
    {
        //if(StatPanel == null) 
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        num = Random.RandomRange(0,resim.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = resim[num];

        StartCoroutine(sil(20f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Camera.GetComponent<statpanelerisim>().eris();
            Destroy(gameObject);
        }
    }

    IEnumerator sil(float sure)
    {
        yield return new WaitForSeconds(sure);
        Destroy(gameObject);
    }
}
