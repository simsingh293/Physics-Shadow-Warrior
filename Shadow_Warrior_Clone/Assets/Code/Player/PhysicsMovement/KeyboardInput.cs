using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IMoveInput
{
    public float Vertical { get; private set; }

    public float Horizontal { get; private set; }

    public void GetInput()
    {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
    }
}
