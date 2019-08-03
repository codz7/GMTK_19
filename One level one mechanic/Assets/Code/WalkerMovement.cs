using UnityEngine;

public class WalkerMovement : MonoBehaviour
{
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private Rigidbody2D m_rigidbody;
    [SerializeField]
    private BoxCollider2D m_groundCheck;

    private int m_currentCollisions;

    private void Update()
    {
        transform.Translate(new Vector3(m_speed, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemies" || collider.gameObject.tag == "Spikes")
        {
            FlipWalker();
            return;
        }

        m_currentCollisions++;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemies" || collider.gameObject.tag == "Spikes")
        {
            return;
        }

        m_currentCollisions--;

        if (m_currentCollisions < 1)
        {
            FlipWalker();
        }
    }

    private void FlipWalker()
    {
        transform.Rotate(new Vector3(0,180,0));
    }
}