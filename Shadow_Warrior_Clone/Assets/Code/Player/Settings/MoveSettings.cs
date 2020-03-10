using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move Settings.asset", menuName = "Settings/Movement")]
public class MoveSettings : ScriptableObject
{
    [SerializeField] private float turnSpeed = 10;
    [SerializeField] private float moveSpeed = 10;

    public float TurnSpeed { get { return turnSpeed; } }
    public float MoveSpeed { get { return moveSpeed; } }
}
