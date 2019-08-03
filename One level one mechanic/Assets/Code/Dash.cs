using UnityEngine;

public class Dash : PlayerMovement
{
    public Vector3 dashVelocity;

    public override void Update()
    {
        base.Update();

        if(Input.GetButtonDown("fire1"))
        {
            //dashVelocity = 
        }
    }
}
