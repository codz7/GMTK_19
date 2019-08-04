using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private List<Checkpoint> m_checkpoints = new List<Checkpoint>(0);

    private Main m_main;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Debug.Log("Goal!");
            m_main.NextLevel();
        }
    }

    public void SetMain(Main main)
    {
        m_main = main;

        foreach (var checkpoint in m_checkpoints)
        {
            checkpoint.SetMain(main);
            m_main.AddCheckpoint();
        }
    }
}
