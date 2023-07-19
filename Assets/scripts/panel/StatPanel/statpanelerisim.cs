using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statpanelerisim : MonoBehaviour
{
    [SerializeField] private GameObject StatPanel;

    public void eris()
    {
        StatPanel.GetComponent<StatPanel>().setDegis();
        Time.timeScale = 0;
    }

}
