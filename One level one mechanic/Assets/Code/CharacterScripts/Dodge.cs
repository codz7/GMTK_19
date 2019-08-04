using UnityEngine;

public class Dodge : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector3 velocity;
    [SerializeField] public float jumpHeight = 15;
    [SerializeField] public float fallVelocity = 15;
    [SerializeField] public float groundDeceleration = 25;
    [SerializeField] public Animator animator;
    [SerializeField] private float knockback = 15;
    public bool grounded = false;

    private bool enemyCollision;
    private Vector3 enemyPosition;
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

    public void Update()
    {
        if (grounded)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);

            if (Input.GetButtonDown("Fire1"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * Mathf.Abs(Physics2D.gravity.y));
                grounded = false;
            }
        }
        else
        {
            velocity.y += Physics2D.gravity.y * Time.deltaTime * fallVelocity;
        }

        transform.Translate(velocity * Time.deltaTime);
    }

    public void FixedUpdate()
    {
        hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);

        if (hitDown.collider != null)
        {
            if (hitDown.collider.gameObject.tag == "Enemies")
            {
                enemyCollision = true;
                enemyPosition = hitDown.collider.transform.position;
                velocity.y = knockback;
            }
            else
            {
                grounded = true;
                velocity.y = 0;
            }            
        }
        else
        {
            grounded = false;
        }

        hitUp = Physics2D.Raycast(transform.position, Vector2.up, 0.5f);

        if (hitUp.collider != null)
        {
            if (hitUp.collider.gameObject.tag == "Enemies")
            {
                enemyCollision = true;
                enemyPosition = hitUp.collider.transform.position;
                velocity.y = knockback;
                velocity.x = knockback * -1;
            }
            else
            {
                velocity.y = 0;
            }            
        }

        hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.5f);

        if (hitRight.collider != null)
        {
            if (hitRight.collider.gameObject.tag == "Enemies")
            {
                enemyCollision = true;
                enemyPosition = hitRight.collider.transform.position;
                velocity.y = knockback;
                velocity.x = knockback * -1;
            }
            else
            {
                velocity.x = 0;
            }            
        }

        hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.5f);

        if (hitLeft.collider != null)
        {
            if (hitLeft.collider.gameObject.tag == "Enemies")
            {
                enemyCollision = true;
                enemyPosition = hitLeft.collider.transform.position;
                velocity.y = knockback;
                velocity.x = knockback;
            }
            else
            {
                velocity.x = 0;
            }
        }

        if (enemyCollision)
        {
            enemyCollision = false;

            velocity = (transform.position - enemyPosition).normalized * knockback;
            velocity = new Vector2(velocity.x, velocity.y);

        }
    }
}
