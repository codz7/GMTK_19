using UnityEngine;

public class Falling : MonoBehaviour
{
    [SerializeField]
    private float fallSpeed = 10;

    private bool falling = false;
    private Vector2 fallDirection;
    private Vector3 mousepos;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos = new Vector3(mousepos.x, mousepos.y, 0);
            fallDirection = (mousepos - transform.position).normalized;
            falling = true;
        }
    }

    private void FixedUpdate()
    {
        if (falling)
        {
            Vector2 velocity = fallDirection * fallSpeed;
            transform.Translate(velocity * Time.deltaTime);
        }
    }
}
