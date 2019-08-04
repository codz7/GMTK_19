using UnityEngine;

public class DeathOnEnemies : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            GetComponent<ResetPosition>().ResetPlayerPosition();
        }
    }
}
