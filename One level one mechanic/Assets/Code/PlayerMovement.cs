using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") < 0)
        {
            rb.velocity = -Vector2.right;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            rb.velocity = Vector2.right;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (Input.GetAxis("Jump") > 0)
        {
            rb.velocity = (Vector2.up);
        }
    }
}
