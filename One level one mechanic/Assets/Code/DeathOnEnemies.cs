using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOnEnemies : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.gameObject.tag == "Enemies")
        {
            GetComponent<ResetPosition>().ResetPlayerPosition();
        }
    }
}
