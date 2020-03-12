using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YComponent : MonoBehaviour
{
    public float yInitial = 0;
    public float yFinal = 0;

    public float VyInitial = 0;
    public float VyFinal = 0;

    public float Acceleration = 0;
    public float gravity = -9.8f;

    public float time = 0;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CalculateFinalYDisplacement();
        }
    }


    void CalculateFinalYDisplacement()
    {
        Acceleration += gravity;


        yFinal = yInitial + (VyInitial * time) + (0.5f * Acceleration * Mathf.Pow(time, 2));

        Acceleration -= gravity;
    }
}
