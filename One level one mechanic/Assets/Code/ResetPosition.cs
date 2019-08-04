using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    private Vector3 m_startPosition;

    public void ResetPlayerPosition()
    {
        FindObjectOfType<Main>().ResetLevel();
    }
}
