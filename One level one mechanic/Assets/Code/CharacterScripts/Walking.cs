using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 startPos;
    private Vector3 velocity;
    [SerializeField] public float fallVelocity = 15;
    [SerializeField] public float walkAcceleration = 25;
    [SerializeField] public float speed = 7;
    [SerializeField] public float groundDeceleration = 25;
    [SerializeField] public float airMovementAdjuster = 0.7f;
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

        startPos = transform.position;
    }

    public virtual void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (grounded)
            {
                velocity.x = Mathf.MoveTowards(velocity.x, speed, walkAcceleration * Time.deltaTime);
            }
            else
            {
                velocity.x = Mathf.MoveTowards(velocity.x, speed, airMovementAdjuster * walkAcceleration * Time.deltaTime);
            }
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);
        }

        if (!grounded)
        {
            velocity.y += Physics2D.gravity.y * Time.deltaTime;
        }

        if (velocity != Vector3.zero)
        {
            animator.SetTrigger("Run");
            animator.ResetTrigger("Idle");
        }
        else
        {
            animator.SetTrigger("Idle");
            animator.ResetTrigger("Run");
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
