using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// panelinden element seçeye yarar
    /// </summary>
    public void SelectElement(int sn)
    {
        SpellController sc = GameObject.FindGameObjectWithTag("Player").GetComponent<SpellController>();

        sc.SelectElement((SpellName)sn);

        Time.timeScale = 1;

        Destroy(gameObject);
    }
}
