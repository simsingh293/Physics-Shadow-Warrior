using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private MoveSettings moveSettings;

    private IMoveInput moveInput;
    private IMoveInput rotationInput;

    public float playerHeight = 0;
    public float originalY = 0;
    public float jumpTime = 0;
    public float jumpHeight = 10;
    public float riseSpeed = 5;
    public float fallSpeed = 5;

    public bool isJumping = false;
    public bool rising = false;
    public bool falling = false;
    public bool isGrounded = false;
    public bool jumpPressed = false;





    void Start()
    {
        if(rigid == null)
        {
            rigid = gameObject.GetComponent<Rigidbody>();
        }

        moveInput = new KeyboardInput();
        rotationInput = new MouseInput();

        playerHeight = gameObject.GetComponentInChildren<Collider>().bounds.extents.y;
        //Debug.Log(playerHeight);
    }

    void Update()
    {
        moveInput.GetInput();
        rotationInput.GetInput();

        if (Input.GetKey(KeyCode.Space))
        {
            jumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
            
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            //jumpPressed = false;
        }

        isGrounded = isOnGround();
    }


    private void FixedUpdate()
    {
        Vector3 movement = rigid.transform.right * moveInput.Horizontal;
        movement += (rigid.transform.forward * moveInput.Vertical);
        movement.y = 0;

        movement = movement.normalized * moveSettings.MoveSpeed * Time.fixedDeltaTime;

        Vector3 targetPos = rigid.position + movement;

        // if player initiates a jump while on the ground
        if(jumpPressed && isGrounded)
        {
            rising = true;
            falling = false;
            isJumping = true;
            jumpPressed = false;
            originalY = rigid.position.y;
        }

        // While the player is in the falling state..
        if (falling)
        {
            // If the grounded check returns true
            if (isGrounded)
            {
                rising = false;
                falling = false;
                isJumping = false;
                jumpTime = 0;
                jumpHeight = 10;
                originalY = rigid.position.y;
            }
        }

        if (!isGrounded)
        {


            float newYPos = targetPos.y;

            // if the player is rising or falling
            if (rising || falling)
            {
                // add to time counter
                jumpTime += Mathf.Abs(Time.deltaTime);

                if (rising)
                {
                    newYPos = originalY + (riseSpeed * jumpTime) + (0.5f * -9.8f * jumpTime * jumpTime);
                }
                else if (falling)
                {
                    newYPos = originalY + (fallSpeed * jumpTime) + (0.5f * -9.8f * jumpTime * jumpTime);
                }

                //targetPos.y = newYPos;
            }

            if ((rising && !falling && newYPos >= jumpHeight) || (!jumpPressed))
            {
                falling = true;
                rising = false;
            }

            if (rising || falling)
            {
                targetPos.y = newYPos;
            }

            //else if (!jumpPressed)
            //{

            //}
        }


        if (targetPos.sqrMagnitude > Mathf.Epsilon)
        {
            rigid.MovePosition(targetPos);
        }

    }
















    private bool isOnGround()
    {
        float lengthToSearch = 1.1f;

        Vector3 lineStart = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        Vector3 vectorToSearch = new Vector3(this.transform.position.x, lineStart.y - lengthToSearch, this.transform.position.z);

        Color color = new Color(0.0f, 0.0f, 1.0f);
        Debug.DrawLine(lineStart, vectorToSearch, color);

        RaycastHit hitInfo;
        if (Physics.Linecast(this.transform.position, vectorToSearch, out hitInfo))
        {
            if (hitInfo.distance < playerHeight)
            {
                return true;
            }
        }
        return false;
    }
}
