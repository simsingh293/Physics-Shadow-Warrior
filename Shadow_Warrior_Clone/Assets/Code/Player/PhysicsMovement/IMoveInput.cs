using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveInput
{
    float Vertical { get; }
    float Horizontal { get; }
    void GetInput();
}
