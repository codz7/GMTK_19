using UnityEngine;

public class Dash : MonoBehaviour
{
    public Vector3 dashVelocity;
    [SerializeField] public float dashStrength;

    public void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            dashVelocity = (Input.mousePosition - transform.position).normalized * dashStrength;
            dashVelocity.z = 0;
        }

        if (dashVelocity.sqrMagnitude > 0)
        {
            dashVelocity -= Vector3.one * Time.deltaTime;
            transform.Translate(dashVelocity * Time.deltaTime);
        }
    }
}
