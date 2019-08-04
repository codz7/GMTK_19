using UnityEngine;

public class Magnet : MonoBehaviour
{
    public Vector2 direction;
    bool hasColided;

    private void Update()
    {
        if (!hasColided)
        {
            transform.Translate(20f * direction * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "Player")
        {
            collision.gameObject.GetComponent<Hookshot>().hooking = false;
        }
        else if (tag == "Spikes")
        {
            Destroy(gameObject);
        }
        else
        {
            hasColided = true;
        }
    }
}
