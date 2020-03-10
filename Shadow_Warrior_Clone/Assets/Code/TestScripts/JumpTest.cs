using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTest : MonoBehaviour
{
    [SerializeField] private Rigidbody Rigidbody = null;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float moveSpeed = 4;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float mass = 10;

    [SerializeField] private LayerMask groundLayer;

    float vertical = 0;
    float horizontal = 0;
    bool jump = false;

    Vector3 net = Vector3.zero;

    void Start()
    {
        
    }

    void Update()
    {
        GetInput();

        net = Vector3.zero;

        //if (!GroundCheck())
        //{
        //    net += CalculateGravity();
        //}

        //if (jump)
        //{
        //    net += PerformJump();
        //    jump = false;
        //}

        //net += CalculateMovement();

        

        //MoveRigidbody(net);
    }

    private void FixedUpdate()
    {
        if (!GroundCheck())
        {
            net += CalculateGravity();
        }

        if (jump)
        {
            net += PerformJump();
            jump = false;
        }

        net += CalculateMovement();



        MoveRigidbody(net);
    }


    Vector3 CalculateGravity()
    {
        Vector3 accel = Vector3.zero;

        accel = mass * (Vector3.down * gravity) * Time.deltaTime;

        return accel;
    }

    bool GroundCheck()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1.1f, groundLayer))
        {
            return true;
        }

        return false;
    }

    Vector3 CalculateMovement()
    {
        Vector3 accel = Vector3.zero;

        accel = Rigidbody.transform.forward * vertical * Time.deltaTime * moveSpeed;

        return accel;
    }


    void GetInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        jump = Input.GetKeyDown(KeyCode.Space);
    }

    void MoveRigidbody(Vector3 netForce)
    {
        Vector3 current = Rigidbody.position;
        Vector3 offset = netForce;

        Vector3 newPos = current + offset;

        Rigidbody.MovePosition(newPos);
    }

    Vector3 PerformJump()
    {
        
        Vector3 final, initial, acceleration, vjump = Vector3.zero;

        initial = Rigidbody.velocity;
        acceleration = mass * (Vector3.down * gravity) * Time.deltaTime;
        vjump = -acceleration;
        vjump += (Vector3.up * jumpForce);

        final = initial + vjump * Time.deltaTime;

        return final;
        
    }
}
