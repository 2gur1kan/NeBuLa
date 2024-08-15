using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expcollector : MonoBehaviour
{
    [SerializeField] private int exp = 1;
    [SerializeField] private float destroyTime = 15f;

    private float range = 3;
    private float speed = 2;

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        range = LevelController.Instance.Range;
        speed = LevelController.Instance.Speed;
    }

    void Start()
    {
        Invoke("Destroy", destroyTime);
    }

    void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            LevelController.Instance.AddExp(exp);
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance <= range)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
