using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMotor
{
    private readonly IMoveInput moveInput;
    private readonly IGravityInput gravityInput;
    private readonly Transform transformToMove;
    private readonly Rigidbody rigidbodyToMove;
    private readonly SO_MoveSettings moveSettings;
    private readonly bool useTransform;

    public MoveMotor(IMoveInput _moveInput, Transform _transform, SO_MoveSettings _moveSettings)
    {
        this.moveInput = _moveInput;
        this.transformToMove = _transform;
        this.moveSettings = _moveSettings;
        this.useTransform = true;
    }

    public MoveMotor(IMoveInput _moveInput, IGravityInput _gravityInput, Rigidbody _rigidbody, SO_MoveSettings _moveSettings)
    {
        this.moveInput = _moveInput;
        this.gravityInput = _gravityInput;
        this.rigidbodyToMove = _rigidbody;
        this.moveSettings = _moveSettings;
        this.useTransform = false;
        
    }

    public void Tick()
    {
        if (useTransform)
        {
            TransformMovement();
        }
        
    }

    public void FixedTick()
    {
        if (!useTransform)
        {
            RigidbodyMovement();
        }
    }


    void TransformMovement()
    {
        transformToMove.Rotate(Vector3.up * moveInput.Horizontal * Time.deltaTime * moveSettings.TurnSpeed);
        transformToMove.position += transformToMove.forward * moveInput.Vertical * Time.deltaTime * moveSettings.MoveSpeed;
    }

    void RigidbodyMovement()
    {
        Debug.Log("Using Rigidbody Movement");

        RotateRigidbody();

        MoveRigidbody();
    }

    void RotateRigidbody()
    {
        Quaternion currRot = rigidbodyToMove.rotation;
        Vector3 newRot = currRot.eulerAngles;

        newRot += Vector3.up * moveInput.Horizontal * Time.deltaTime * moveSettings.TurnSpeed;

        Quaternion target = Quaternion.Euler(newRot);

        rigidbodyToMove.MoveRotation(target);
    }

    void MoveRigidbody()
    {
        Vector3 currentPosition = rigidbodyToMove.position;

        Vector3 offset = rigidbodyToMove.transform.forward * moveInput.Vertical * Time.deltaTime * moveSettings.MoveSpeed;

        offset += (gravityInput.GravityForce * Time.deltaTime);

        Vector3 newPosition = currentPosition + offset;

        rigidbodyToMove.MovePosition(newPosition);
    }
}
