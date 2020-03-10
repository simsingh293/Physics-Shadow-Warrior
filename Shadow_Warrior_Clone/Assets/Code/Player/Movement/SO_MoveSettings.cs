using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveSetting.asset", menuName = "Move/Settings")]
public class SO_MoveSettings : ScriptableObject
{
    [SerializeField] private float turnSpeed = 25f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private bool useTransformToMove = false;


    public float TurnSpeed { get { return turnSpeed; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public bool UseTransform { get { return useTransformToMove; } }
}
