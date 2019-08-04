using UnityEngine;

public class Hookshot : MonoBehaviour
{
    [SerializeField] public float speed = 7;
    [SerializeField] public float range = 150;
    [SerializeField] private GameObject magnet;

    Rigidbody2D rb;
    private Vector3 velocity;
    public bool hooking = false;
    private RaycastHit2D hitLeft;
    private RaycastHit2D hitUp;
    private RaycastHit2D hitRight;
    private RaycastHit2D hitDown;
    private RaycastHit2D hitUpleft;
    private RaycastHit2D hitDownleft;
    private RaycastHit2D hitUpRight;
    private RaycastHit2D hitDownRight;
    private Vector3 mousepos;
    private Vector2 m_startPoint;
    private Vector2 m_hookTarget;
    private float m_startTime;
    private float m_journeyLength;
    private GameObject activeMagnet;

    public void Start()
    {
        range = 150;

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
            Debug.DrawLine(transform.position, mousepos, Color.cyan, 3);
            Vector2 direction = (mousepos - transform.position).normalized;
            RaycastHit2D hitHook = Physics2D.Raycast(transform.position, direction, range);

            if (hitHook.collider != null && hitHook.distance > 2.0f)
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
                activeMagnet.transform.position = new Vector2(transform.position.x, transform.position.y) + ((hitHook.point - m_startPoint).normalized * 2f );
                activeMagnet.GetComponent<Magnet>().direction = (hitHook.point - m_startPoint).normalized;
                
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

        hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);
        if (hitDown.collider != null)
        {
            if (velocity.y < 0)
                velocity.y = 0;
        }
        hitUp = Physics2D.Raycast(transform.position, Vector2.up, 1.6f);
        if (hitUp.collider != null)
        {
            if (velocity.y > 0)
                velocity.y = 0;
        }
        hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.6f);
        if (hitRight.collider != null)
        {
            if (velocity.x > 0)
                velocity.x = 0;
        }
        hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.6f);
        if (hitLeft.collider != null)
        {
            if (velocity.x < 0)
                velocity.x = 0;
        }


        hitUpleft = Physics2D.Raycast(transform.position, Vector2.left + Vector2.up, 0.6f);
        if (hitUpleft.collider != null)
        {
            velocity = Vector2.zero;
        }
        hitDownleft = Physics2D.Raycast(transform.position, Vector2.left + Vector2.down, 0.6f);
        if (hitDownleft.collider != null)
        {
            velocity = Vector2.zero;
        }
        hitUpRight = Physics2D.Raycast(transform.position, Vector2.right + Vector2.up, 0.6f);
        if (hitUpRight.collider != null)
        {
            velocity = Vector2.zero;
        }
        hitDownRight = Physics2D.Raycast(transform.position, Vector2.right + Vector2.down, 0.6f);
        if (hitDownRight.collider != null)
        {
            velocity = Vector2.zero;
        }
    }
}
