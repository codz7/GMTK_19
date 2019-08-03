using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector3 velocity;
    [SerializeField] public float fallVelocity = 15;
    [SerializeField] public float acceleration = 25;
    [SerializeField] public float speed = 7;
    [SerializeField] public float deceleration = 25;
    [SerializeField] public Animator animator;
    public bool grounded = false;

    private RaycastHit2D hitLeft;
    private RaycastHit2D hitUp;
    private RaycastHit2D hitRight;
    private RaycastHit2D hitDown;

    public void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    public virtual void Update()
    {
        

        if (Input.GetButton("Fire1"))
        {
            rb.gravityScale = 0;

            velocity.y = Mathf.MoveTowards(velocity.y, speed, acceleration * Time.deltaTime);

            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos = new Vector3(mousepos.x, mousepos.y, 0);

            if (mousepos.x > transform.position.x)
            {
                velocity.x = Mathf.MoveTowards(velocity.x, speed, acceleration * Time.deltaTime);
            }
            else
            {
                velocity.x = Mathf.MoveTowards(velocity.x, speed * -1, acceleration * Time.deltaTime);
            }
        }
        else
        {
            if (!grounded)
            {
                rb.gravityScale = 1;
                velocity.y += Physics2D.gravity.y * Time.deltaTime;
            }

            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
        }

        transform.Translate(velocity * Time.deltaTime);
    }

    public void FixedUpdate()
    {
        hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);

        if (hitDown.collider != null && hitDown.distance < 1.0f)
        {
            grounded = true;
            velocity.y = 0;
        }
        else
        {
            grounded = false;
        }

        hitUp = Physics2D.Raycast(transform.position, Vector2.up, 0.5f);

        if (hitUp.collider != null && hitUp.distance < 1.0f)
        {
            velocity.y = 0;
        }

        hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.5f);

        if (hitRight.collider != null && hitRight.distance < 1.0f)
        {
            velocity.x = 0;
        }

        hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.5f);

        if (hitLeft.collider != null && hitLeft.distance < 1.0f)
        {
            velocity.x = 0;
        }
    }
}
