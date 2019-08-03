using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    private Vector3 m_startPosition;
    
    void Start()
    {
        m_startPosition = transform.position;
    }

    public void ResetPlayerPosition()
    {
        transform.position = m_startPosition;
        GetComponent<Rigidbody2D>().velocity.Set(0, 0);
    }
}
