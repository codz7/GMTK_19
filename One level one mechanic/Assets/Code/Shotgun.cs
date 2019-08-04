using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField]
    private GameObject shotgun;
    [SerializeField]
    private float rayRange;

    Rigidbody2D rb;
    private Vector3 velocity;
    [SerializeField] private float knockback = 10;
    [SerializeField] public float fallVelocity = 15;
    [SerializeField] public float groundDeceleration = 25;
    public bool grounded = false;

    private RaycastHit2D hitLeft;
    private RaycastHit2D hitUp;
    private RaycastHit2D hitRight;
    private RaycastHit2D hitDown;

    private Vector3 mousepos;
    private Vector2 m_startPoint;
    private Vector2 m_hookTarget;
    private Vector2 direction;
    private float m_startTime;

    public void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos = new Vector3(mousepos.x, mousepos.y, 0);
        direction = (mousepos - shotgun.transform.position).normalized;

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.DrawLine(shotgun.transform.position, mousepos);
            velocity = direction * knockback * -1;
            RaycastHit2D shotgunRay = Physics2D.Raycast(shotgun.transform.position, direction, rayRange);

            if (shotgunRay.collider.gameObject.tag == "Enemies")
            {
                Destroy(shotgunRay.collider.gameObject);
            }            
        }

        if (!grounded)
        {
            velocity.y += Physics2D.gravity.y * Time.deltaTime * fallVelocity;
        }

        velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);
        transform.Translate(velocity * Time.deltaTime);

        float AngleRad = Mathf.Atan2(mousepos.y - shotgun.transform.position.y, mousepos.x - shotgun.transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        shotgun.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        
    }

    public void FixedUpdate()
    {
        hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);

        if (hitDown.collider != null)
        {
            grounded = true;
            velocity.y = 0;
        }
        else
        {
            grounded = false;
        }

        hitUp = Physics2D.Raycast(transform.position, Vector2.up, 0.5f);

        if (hitUp.collider != null)
        {
            velocity.y = 0;
        }

        hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.5f);

        if (hitRight.collider != null)
        {
            velocity.x = 0;
        }

        hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.5f);

        if (hitLeft.collider != null)
        {
            velocity.x = 0;
        }
    }
}
