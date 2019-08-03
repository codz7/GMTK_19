﻿using UnityEngine;

public class OutOfBoundsDeath : MonoBehaviour
{
    [SerializeField]
    private float m_levelWidth;
    [SerializeField]
    private float m_levelHeight;

    void Update()
    {
        if (Mathf.Abs(transform.position.x) > m_levelWidth || Mathf.Abs(transform.position.y) > m_levelHeight)
        {
            GetComponent<ResetPosition>().ResetPlayerPosition();
        }        
    }
}