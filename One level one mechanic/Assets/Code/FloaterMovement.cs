using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Transform playerPos;
    private Vector3 playerVector;
    private Vector2 direction;
    private Vector2 velocity;

    private void Start()
    {
        playerPos = FindObjectOfType<ResetPosition>().transform;
    }

    private void Update()
    {
        playerVector = playerPos.position;

        direction = (playerVector - transform.position).normalized;
        direction = new Vector3(direction.x, direction.y, 0);

        velocity.x = Mathf.MoveTowards(velocity.x, direction.x * moveSpeed, acceleration * Time.deltaTime);
        velocity.y = Mathf.MoveTowards(velocity.y, direction.y * moveSpeed, acceleration * Time.deltaTime);

        transform.Translate(velocity * Time.deltaTime);

        if (velocity.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (velocity.y > 0)
        {
            spriteRenderer.flipY = true;
        }
        else
        {
            spriteRenderer.flipY = false;
        }
    }
}
