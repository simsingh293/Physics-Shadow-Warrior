using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthGravity : IGravityInput
{
    public Vector3 GravityForce { get; private set; }

    public void GetInput(Rigidbody _rigid)
    {
        Vector3 currentPosition = _rigid.position;

        Vector3 offset = _rigid.transform.up * 0.5f * Time.deltaTime;

        Vector3 newPosition = currentPosition + offset;

        GravityForce = newPosition;
    }

}
