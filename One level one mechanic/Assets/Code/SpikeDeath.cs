using UnityEngine;

public class SpikeDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            GetComponent<ResetPosition>().ResetPlayerPosition();
        }
    }
}
