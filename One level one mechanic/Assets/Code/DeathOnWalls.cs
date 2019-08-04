using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOnWalls : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<ResetPosition>().ResetPlayerPosition();
    }
}