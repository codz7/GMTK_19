using UnityEngine;

public class Dash : MonoBehaviour
{
    public Vector3 dashVelocity;
    Rigidbody2D rb;
    private Vector3 velocity;
    [SerializeField] public float dashStrength;
    [SerializeField] public float fallVelocity = 15;
    [SerializeField] public float deceleration = 25;
    [SerializeField] public float dashDecreace = 25;
    [SerializeField] public Animator animator;
    public bool grounded = false;

    private RaycastHit2D hitLeft;
    private RaycastHit2D hitUp;
    private RaycastHit2D hitRight;
    private RaycastHit2D hitDown;
    public bool canDash = true;

    public void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    public virtual void Update()
    {
        if(Input.GetButtonDown("Fire1") && canDash)
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos = new Vector3(mousepos.x, mousepos.y, 0);
            dashVelocity = (mousepos - transform.position).normalized * dashStrength;
            dashVelocity.z = 0;
            canDash = false;
            velocity = Vector3.zero;
        }

        if (dashVelocity.sqrMagnitude > 0)
        {
            dashVelocity.x = Mathf.MoveTowards(dashVelocity.x, 0, dashDecreace * Time.deltaTime);
            dashVelocity.y = Mathf.MoveTowards(dashVelocity.y, 0, dashDecreace * Time.deltaTime);
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
            grounded = true;
            velocity.y = 0;
            canDash = true;
        }
        else
        {
            grounded = false;
        }

        hitUp = Physics2D.Raycast(transform.position, Vector2.up, 0.5f);

        if (hitUp.collider != null)
        {
            velocity.y = 0;
            dashVelocity = Vector3.zero;
            canDash = true;
        }

        hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.5f);

        if (hitRight.collider != null)
        {
            velocity.x = 0;
            dashVelocity = Vector3.zero;
            canDash = true;
        }

        hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.5f);

        if (hitLeft.collider != null)
        {
            velocity.x = 0;
            dashVelocity = Vector3.zero;
            canDash = true;
        }
    }
}
