using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : IMoveInput
{
    public float Vertical { get; private set; }

    public float Horizontal { get; private set; }

    public void GetInput()
    {
        Vertical = Input.GetAxis("Mouse Y");
        Horizontal = Input.GetAxis("Mouse X");
    }
}
