using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector3 velocity;
    [SerializeField] public float jumpHeight = 5;
    [SerializeField] public float fallVelocity = 10;
    [SerializeField] public float walkAcceleration = 5;
    [SerializeField] public float speed = 10;
    [SerializeField] public float groundDeceleration = 15;
    public bool grounded = false;

    private RaycastHit2D hitLeft;
    private RaycastHit2D hitUp;
    private RaycastHit2D hitRight;
    private RaycastHit2D hitDown;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, walkAcceleration * Time.deltaTime);
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
            velocity.y += Physics2D.gravity.y * Time.deltaTime;
        }

        transform.Translate(velocity * Time.deltaTime);
    }

    void FixedUpdate()
    {
        hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);

        if(hitDown.collider != null && hitDown.distance < 1.0f)
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

    void Death()
    {
        transform.position = Vector3.zero;
    }

}
