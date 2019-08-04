using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_levels = new List<GameObject>(17);
    [SerializeField]
    private int m_currentLevel = 0;

    private GameObject m_activeLevel;
    private int m_remainingCheckpoints;

    public void Awake()
    {
        m_activeLevel = Instantiate(m_levels[m_currentLevel]);
        m_activeLevel.GetComponentInChildren<Goal>().SetMain(this);
    }

    public void NextLevel()
    {
        if (m_remainingCheckpoints > 0)
        {
            return;
        }

        Destroy(m_activeLevel);
        m_currentLevel++;

        if (m_currentLevel == m_levels.Count)
        {
            GameEnd();
        }
        else
        {
            m_activeLevel = Instantiate(m_levels[m_currentLevel]);
            Goal goal = m_activeLevel.GetComponentInChildren<Goal>();

            if (goal != null)
            {
                goal.SetMain(this);
            }            
        }
    }

    public void AddCheckpoint()
    {
       m_remainingCheckpoints++;
    }

    public void RemoveCheckpoint()
    {
        m_remainingCheckpoints--;
    }

    public void ResetLevel()
    {
        m_remainingCheckpoints = 0;
        Destroy(m_activeLevel);
        m_activeLevel = Instantiate(m_levels[m_currentLevel]);
        Goal goal = m_activeLevel.GetComponentInChildren<Goal>();

        if (goal != null)
        {
            goal.SetMain(this);
        }
    }

    private void GameEnd()
    {

    }
}
