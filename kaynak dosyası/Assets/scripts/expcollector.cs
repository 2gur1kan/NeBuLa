using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expcollector : MonoBehaviour
{
    [SerializeField] int VercegiExp = 1;

    private float ToplamaMesafesi = 3;
    private float speed = 2;

    private GameObject Camera;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");

        ToplamaMesafesi = Camera.GetComponent<expcollectordata>().getRange();
        speed = Camera.GetComponent<expcollectordata>().getSpeed();

        StartCoroutine(bekle(15f));
    }

    // Update is called once per frame
    void Update()
    {
        Toplan();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            player.GetComponent<player>().addExp(VercegiExp);
            Destroy(gameObject);
        }
    }

    private void Toplan()
    {
        float Uzaklik = Vector3.Distance(transform.position, player.transform.position);

        if(Uzaklik <= ToplamaMesafesi)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    public void setToplamaMesafsi(float ToplamaMesafesi)
    {
        this.ToplamaMesafesi += ToplamaMesafesi;
    }

    public void setSpeed(float speed)
    {
        this.speed += speed;
    }

    IEnumerator bekle(float sure)
    {
        yield return new WaitForSeconds(sure);
        Destroy(gameObject);
    }
}
