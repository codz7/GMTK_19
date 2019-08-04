using UnityEngine;

public class HelicopterRotor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Player")
        {
            GetComponentInParent<ResetPosition>().ResetPlayerPosition();
        }
    }
}
