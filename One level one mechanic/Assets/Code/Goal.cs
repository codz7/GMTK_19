using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private Main m_main;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            m_main.NextLevel();
        }
    }

    public void SetMain(Main main)
    {
        Debug.Log("Main Set");
        m_main = main;
    }
}
