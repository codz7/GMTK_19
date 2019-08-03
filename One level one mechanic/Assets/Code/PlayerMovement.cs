using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector3 movementVector;
    [SerializeField] public float jumpHeight = 5;
    [SerializeField] public float fallVelocity = -10;
    public bool inAir = true;

    public bool Move_DEBUG;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.collider.transform.position.y < transform.position.y)
        {
            inAir = false;
        }
    }

    void Update()
    {
        movementVector = Vector2.zero;
        
        if (Mathf.Abs(Input.GetAxis("Horizontal")) != 0)
        {
            movementVector.x += Input.GetAxis("Horizontal") * Time.deltaTime;
            Move_DEBUG = true;
        }
        else
        {
            Move_DEBUG = false;
        }

        if (!inAir && Input.GetButtonDown("Jump"))
        {
            inAir = true;
            movementVector.y = jumpHeight;
        }

        if(inAir)
        {
            movementVector.y += fallVelocity * Time.deltaTime;
        }

        transform.position += movementVector;
    }
}
