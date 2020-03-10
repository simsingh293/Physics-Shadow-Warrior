using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGravityInput
{
    void GetInput(Rigidbody _rigid);
    Vector3 GravityForce { get; }
}
