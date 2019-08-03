using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D m_boxCollider;
    [SerializeField]
    private SpriteRenderer m_spriteRenderer;
    [SerializeField]
    private Sprite m_activatedSprite;

    private Main m_main;
    private bool m_activated;

    public void SetMain(Main main)
    {
        m_main = main;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!m_activated && collider.gameObject.tag == "Player")
        {
            m_main.RemoveCheckpoint();
            m_activated = true;
            m_spriteRenderer.sprite = m_activatedSprite;
        }
    }
}
