using UnityEngine;

public class Dash : MonoBehaviour
{
    public Vector3 dashVelocity;
    Rigidbody2D rb;
    private Vector3 velocity;
    [SerializeField] public float dashStrength;
    [SerializeField] public float fallVelocity = 15;
    [SerializeField] public float deceleration = 25;
    [SerializeField] public float dashDecrease = 25;
    public float dashTimer = 0.15f;
    [SerializeField] public Animator animator;
    public bool grounded = false;

    private RaycastHit2D hitLeft;
    private RaycastHit2D hitUp;
    private RaycastHit2D hitRight;
    private RaycastHit2D hitDown;
    public bool canDash = true;
    public float dashCooldown = 0;

    public void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    public virtual void Update()
    {
        if(Input.GetButtonDown("Fire1") && canDash && dashCooldown <= 0.0f)
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos = new Vector3(mousepos.x, mousepos.y, 0);
            dashVelocity = (mousepos - transform.position).normalized * dashStrength;
            dashVelocity.z = 0;
            canDash = false;
            velocity = Vector3.zero;
            dashCooldown = dashTimer;
        }

        if (dashCooldown > 0.0f)
        {
            dashCooldown -= Time.deltaTime;
        }

        FixedUpdate();

        if (dashVelocity.sqrMagnitude > 0)
        {
            dashVelocity.x = Mathf.MoveTowards(dashVelocity.x, 0, dashDecrease * Time.deltaTime);
            dashVelocity.y = Mathf.MoveTowards(dashVelocity.y, 0, dashDecrease * Time.deltaTime);
            dashVelocity.z = 0;
            transform.Translate(dashVelocity * Time.deltaTime);
        }
        else
        {
            if (!grounded)
            {
                velocity.y += Physics2D.gravity.y * Time.deltaTime;
            }

            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
            transform.Translate(velocity * Time.deltaTime);
        }
    }

    public void FixedUpdate()
    {
        hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);

        if (hitDown.collider != null)
        {
            if (dashCooldown < dashTimer * 0.9f)
            {
                canDash = true;
            }

            grounded = true;

            if (velocity.y < 0)
                velocity.y = 0;

            if (dashVelocity.y < 0)
                dashVelocity.y = 0;
        }
        else
        {
            grounded = false;
        }

        hitUp = Physics2D.Raycast(transform.position, Vector2.up, 1.6f);

        if (hitUp.collider != null)
        {
            if (velocity.y > 0)
                velocity.y = 0;

            if (dashVelocity.y > 0)
                dashVelocity.y = 0;

            canDash = true;
        }

        hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.6f);

        if (hitRight.collider != null)
        {
            //velocity.x = 0;
            //dashVelocity = Vector3.zero;

            if (velocity.x > 0)
                velocity.x = 0;

            if (dashVelocity.x > 0)
                dashVelocity.x = 0;

            canDash = true;
        }

        hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.6f);

        if (hitLeft.collider != null)
        {
            //velocity.x = 0;
            //dashVelocity = Vector3.zero;
            if (velocity.x < 0)
                velocity.x = 0;

            if (dashVelocity.x < 0)
                dashVelocity.x = 0;

            canDash = true;
        }
    }
}
