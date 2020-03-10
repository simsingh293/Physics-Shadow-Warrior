using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    [SerializeField] private SO_MoveSettings moveSettings;
    [SerializeField] private Rigidbody _rigidbody = null;

    private IMoveInput moveInput;
    private IGravityInput gravityInput;
    private MoveMotor moveMotor;
    private GravityMotor gravityMotor;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        
        moveInput = new KeyboardInput();
        gravityInput = new EarthGravity();

        moveMotor = moveSettings.UseTransform ?
            new MoveMotor(moveInput, transform, moveSettings) :
            new MoveMotor(moveInput, gravityInput, _rigidbody, moveSettings);

        
    }

    private void Update()
    {
        moveInput.GetInput();
        gravityInput.GetInput(_rigidbody);


        moveMotor.Tick();
    }

    private void FixedUpdate()
    {
        moveMotor.FixedTick();
    }
}


public class GravityMotor
{
    private readonly Rigidbody rigidbody;

    public GravityMotor(Rigidbody _rigidbody)
    {
        rigidbody = _rigidbody;
    }

    public void FixedTick()
    {
        Vector3 currentPosition = rigidbody.position;

        Vector3 offset = rigidbody.transform.up * -0.5f *Time.deltaTime;

        Vector3 newPosition = currentPosition + offset;

        rigidbody.MovePosition(newPosition);
    }
}