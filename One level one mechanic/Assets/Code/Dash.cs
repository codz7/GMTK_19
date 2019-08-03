using UnityEngine;

public class Dash : MonoBehaviour
{
    public Vector3 dashVelocity;
    Rigidbody2D rb;
    private Vector3 velocity;
    [SerializeField] public float dashStrength;
    [SerializeField] public float fallVelocity = 15;
    [SerializeField] public float deceleration = 25;
    [SerializeField] public Animator animator;
    public bool grounded = false;

    private RaycastHit2D hitLeft;
    private RaycastHit2D hitUp;
    private RaycastHit2D hitRight;
    private RaycastHit2D hitDown;
    private bool canDash = true;

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
            dashVelocity -= Vector3.one * Time.deltaTime;
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

        if (hitDown.collider != null && hitDown.distance < 1.0f)
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

        if (hitUp.collider != null && hitUp.distance < 1.0f)
        {
            velocity.y = 0;
            dashVelocity.y = 0;
            canDash = true;
        }

        hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.5f);

        if (hitRight.collider != null && hitRight.distance < 1.0f)
        {
            velocity.x = 0;
            dashVelocity.x = 0;
            canDash = true;
        }

        hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.5f);

        if (hitLeft.collider != null && hitLeft.distance < 1.0f)
        {
            velocity.x = 0;
            dashVelocity.x = 0;
            canDash = true;
        }
    }
}
