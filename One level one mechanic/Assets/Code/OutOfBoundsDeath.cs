using UnityEngine;

public class OutOfBoundsDeath : MonoBehaviour
{
    [SerializeField]
    private float m_levelWidth;
    [SerializeField]
    private float m_levelHeight;

    void Update()
    {
        if (Mathf.Abs(transform.position.x) > m_levelWidth || transform.position.y < m_levelHeight * -1)
        {
            GetComponent<ResetPosition>().ResetPlayerPosition();
        }        
    }
}
