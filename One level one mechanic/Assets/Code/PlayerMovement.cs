using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector3 velocity;
    [SerializeField] public float jumpHeight = 15;
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
    }

    public virtual void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput != 0)
        {
            if (grounded)
            {
                velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, walkAcceleration * Time.deltaTime);
            }
            else
            {
                velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, airMovementAdjuster * walkAcceleration * Time.deltaTime);
            }
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);
        }

        if (grounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            grounded = false;
        }

        if(!grounded)
        {
            velocity.y += Physics2D.gravity.y * Time.deltaTime * fallVelocity;
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

        if(hitDown.collider != null)
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
