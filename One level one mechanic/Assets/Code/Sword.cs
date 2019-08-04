using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private Melee melee;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Player")
        {
            melee.LaunchPlayer();

            if (collider.gameObject.tag == "Enemies")
            {
                Destroy(collider.gameObject);
            }
        }
    }
}
