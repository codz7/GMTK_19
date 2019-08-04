using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector3 velocity;
    [SerializeField] public float jumpHeight = 15;
    [SerializeField] public float fallVelocity = 10;
    [SerializeField] public float deceleration = 3;
    [SerializeField] public Animator animator;
    public bool grounded = false;

    private RaycastHit2D hitLeft;
    private RaycastHit2D hitUp;
    private RaycastHit2D hitRight;
    private RaycastHit2D hitDown;
    private bool charging;

    public void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    public virtual void Update()
    {
        if (grounded)
        {
            animator.SetTrigger("Idle");
            animator.ResetTrigger("Jump");

            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);

            if (Input.GetButtonDown("Fire1"))
            {
                charging = true;
            }
        }
        else
        {
            animator.SetTrigger("Jump");
            animator.ResetTrigger("Idle");

            velocity.y += Physics2D.gravity.y * Time.deltaTime * fallVelocity;
            charging = false;
        }

        if (charging)
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos = new Vector3(mousepos.x, mousepos.y, 0);

            Debug.DrawLine(transform.position, mousepos);

            if (Input.GetButtonUp("Fire1"))
            {
                velocity = (mousepos - transform.position).normalized * jumpHeight;
                grounded = false;
                charging = false;
            }
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
