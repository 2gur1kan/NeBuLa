using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Array : MonoBehaviour
{
    private List<Transform> Enemys = new List<Transform>();

    private void Start()
    {
        InvokeRepeating("AddChildTransforms", 0f, .1f);
    }

    /// <summary>
    /// Child transformlar� listeye ekleyen metod 
    /// </summary>
    private void AddChildTransforms()
    {
        Enemys = new List<Transform>();

        foreach (Transform child in gameObject.transform)
        {
            Enemys.Add(child);
        }
    }

    /// <summary>
    /// En yak�ndaki d��man� bulup konumunu d�nderir
    /// </summary>
    /// <param name="CurrentTransform"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public Vector3 SelectNearestEnemy(Vector3 CurrentTransform, float radius = 30f)
    {
        Vector3 gg = Vector3.negativeInfinity;
        Vector3 result = Vector3.negativeInfinity;
        float distance = 333;

        foreach (Transform Enemy in Enemys)
        {
            gg = Enemy.position;
            distance = Vector3.Distance(CurrentTransform, gg);

            if(distance < radius)
            {
                radius = distance;
                result = gg;
            }
        }

        return result;
    }

    /// <summary>
    /// Bu i�levin bulunmama �ans� var ��nk� 100 denemede de yak�nda olmama ihtimali var
    /// </summary>
    /// <param name="CurrentTransform"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public Vector3 SelectRandomEnemy(Vector3 CurrentTransform, float radius = 30f)
    {
        int gg = 100;
        int rand = Random.Range(0, Enemys.Count);
        float distance = 333;

        while (gg > 0)
        {
            distance = Vector3.Distance(CurrentTransform, Enemys[rand].position);

            if (distance <= radius)
            {
                return Enemys[rand].position;
            }

            rand = Random.Range(0, Enemys.Count);
            gg--;
        }

        return Vector3.negativeInfinity;
    }
}
