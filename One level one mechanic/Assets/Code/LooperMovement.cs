using UnityEngine;

public class LooperMovement : MonoBehaviour
{
    [SerializeField]
    private Transform m_loopPointObject;
    [SerializeField]
    private float m_speed;

    private Vector3 m_startPoint;
    private bool m_returning;
    private Vector3 m_currentTarget;
    private float m_startTime;
    private float m_journeyLength;
    private Vector3 m_loopPoint;

    private void Awake()
    {
        m_loopPoint = m_loopPointObject.transform.position;
        m_startPoint = transform.position;
        m_startTime = Time.time;
    }

    private void Update()
    {
        LerpToTarget();


        if (transform.position == m_currentTarget)
        {
            TargetReached();
        }
    }

    private void LerpToTarget()
    {
        float distCovered = (Time.time - m_startTime) * m_speed;
        float fracJourney = distCovered / m_journeyLength;
        transform.position = Vector3.Lerp(m_startPoint, m_currentTarget, fracJourney);
    }

    private void TargetReached()
    {
        if (m_returning)
        {
            m_currentTarget = m_loopPoint;
        }
        else
        {
            m_currentTarget = m_startPoint;
        }

        m_returning = !m_returning;
        m_startTime = Time.time;
        m_journeyLength = Vector3.Distance(transform.position, m_currentTarget);
        m_startPoint = transform.position;
    }
}
