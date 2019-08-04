using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField]
    private GameObject sword;

    Rigidbody2D rb;
    private Vector3 velocity;
    [SerializeField] private float knockback = 10;
    [SerializeField] public float fallVelocity = 15;
    [SerializeField] public float groundDeceleration = 25;
    [SerializeField] private SpriteRenderer spriteRenderer;
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

    public void LaunchPlayer()
    {
        velocity = direction * knockback * -1;
    }

    private void Update()
    {
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos = new Vector3(mousepos.x, mousepos.y, 0);
        direction = (mousepos - sword.transform.position).normalized;

        if (!grounded)
        {
            velocity.y += Physics2D.gravity.y * Time.deltaTime * fallVelocity;
        }

        velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);
        transform.Translate(velocity * Time.deltaTime);

        float AngleRad = Mathf.Atan2(sword.transform.position.y - mousepos.y, sword.transform.position.x - mousepos.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        sword.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

        if (mousepos.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (mousepos.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
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
