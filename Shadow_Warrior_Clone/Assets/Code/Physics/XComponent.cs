using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XComponent : MonoBehaviour
{
    public float xInitial = 0;
    public float xFinal = 0;

    public float VxInitial = 0;
    public float VxFinal = 0;

    public float Acceleration = 0;

    public float time = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CalculateFinalXDisplacement();
        }
    }

    void CalculateFinalXDisplacement()
    {
        xFinal = xInitial + (VxInitial * time) + (0.5f * Acceleration * Mathf.Pow(time, 2));
    }
}
