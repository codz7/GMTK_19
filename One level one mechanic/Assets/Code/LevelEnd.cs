using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField]
    public GameObject levelPrefabToUnload;

    [SerializeField]
    public GameObject nextLevelPrefab;

    [SerializeField]
    public GameObject player;


    void OnCollisionEnter2D(Collision2D obj)
    {
        Destroy(levelPrefabToUnload);
        Instantiate(nextLevelPrefab);
    }
}
