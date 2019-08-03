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

    public void Awake()
    {
        m_activeLevel = Instantiate(m_levels[m_currentLevel]);
        m_activeLevel.GetComponentInChildren<Goal>().SetMain(this);
    }

    public void NextLevel()
    {
        Destroy(m_activeLevel);
        m_currentLevel++;

        if (m_currentLevel == m_levels.Count)
        {
            GameEnd();
        }
        else
        {
            m_activeLevel = Instantiate(m_levels[m_currentLevel]);
            m_activeLevel.GetComponentInChildren<Goal>().SetMain(this);
        }
    }

    private void GameEnd()
    {

    }
}
