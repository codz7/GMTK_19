using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookshot : MonoBehaviour
{
    [SerializeField] public float speed = 7;
    [SerializeField] public float range = 15;
    [SerializeField] private GameObject magnet;

    Rigidbody2D rb;
    private Vector3 velocity;
    private bool hooking = false;
    private RaycastHit2D hitLeft;
    private RaycastHit2D hitUp;
    private RaycastHit2D hitRight;
    private RaycastHit2D hitDown;
    private Vector3 mousepos;
    private Vector2 m_startPoint;
    private Vector2 m_hookTarget;
    private float m_startTime;
    private float m_journeyLength;
    private GameObject activeMagnet;

    public void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    public virtual void Update()
    {
        if (!hooking && Input.GetButtonDown("Fire1"))
        {
            mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos = new Vector3(mousepos.x, mousepos.y, 0);
            Debug.DrawLine(transform.position, mousepos);
            Vector2 direction = (mousepos - transform.position).normalized;
            RaycastHit2D hitHook = Physics2D.Raycast(transform.position, direction, range);

            if (hitHook.collider != null)
            {
                if (activeMagnet != null)
                {
                    Destroy(activeMagnet);
                }

                hooking = true;
                m_startPoint = transform.position;
                m_hookTarget = hitHook.point;
                m_journeyLength = Vector2.Distance(transform.position, hitHook.point);
                m_startTime = Time.time;

                activeMagnet = Instantiate(magnet);
                activeMagnet.transform.position = m_hookTarget;
            }
        }
    }

    public void FixedUpdate()
    {
        if (hooking)
        {
            Debug.DrawLine(transform.position, m_hookTarget);
            float distCovered = (Time.time - m_startTime) * speed;
            float fracJourney = distCovered / m_journeyLength;
            transform.position = Vector2.Lerp(m_startPoint, m_hookTarget, fracJourney);
        }

        /*
        hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);

        if (hitDown.collider != null && hitDown.distance < 1.0f)
        {
            velocity.y = 0;
            hooking = false;
        }
        else
        {
            grounded = false;
        }
        */

        hitUp = Physics2D.Raycast(transform.position, Vector2.up, 0.5f);

        if (hitUp.collider != null && hitUp.distance < 1.0f)
        {
            velocity.y = 0;
            hooking = false;
        }

        hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.5f);

        if (hitRight.collider != null && hitRight.distance < 1.0f)
        {
            velocity.x = 0;
            hooking = false;
        }

        hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.5f);

        if (hitLeft.collider != null && hitLeft.distance < 1.0f)
        {
            velocity.x = 0;
            hooking = false;
        }
    }
}
