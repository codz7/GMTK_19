﻿using UnityEngine;

public class SpikeDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.gameObject.tag == "Spike")
        {
            Destroy(gameObject);
        }
    }
}