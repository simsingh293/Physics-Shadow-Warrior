using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveInput
{
    void GetInput();
    float Vertical { get; }
    float Horizontal { get; }
}
