using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugiPlayer : MonoBehaviour
{
    bool isJumping = false, jumpUp = false, fallDown = false, isGrounded = false, isKeySpaceDown = false;
    public int jumpUpSpeed, fallSpeed, moveSpeed, spacePress = 0;
    private float halfPlayerHeight = 1f, jumpTime = 0, originalYPos, OGYP, move, side, jumpHeight;
    public GameObject destination, plane, platform, platform2;
    private Rigidbody rigidBody;
    //private Scrollbar health;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        //health = GameObject.Find("Health").GetComponent<Scrollbar>();
        jumpHeight = 10;
    }

    // Update is called once per frame
    void Update()
    {

        move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        side = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        transform.Translate(new Vector3(side, 0, move), Space.Self);

        if (Input.GetKey(KeyCode.Space))
        {
            isJumping = true;
            ++spacePress;

            if (spacePress == 2)
            {
                jumpHeight *= 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isKeySpaceDown = true;
            OGYP = rigidBody.position.y;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isKeySpaceDown = false;
        }

        float rotateHorizontal = Input.GetAxis("Mouse X"), rotateVertical = Input.GetAxis("Mouse Y");

        transform.Rotate(new Vector3(0, rotateHorizontal, 0));
        Camera.main.transform.Rotate(new Vector3(-rotateVertical, 0, 0));

        isGrounded = isOnGround();
    }

    private void FixedUpdate()
    {
        Vector3 movement = Vector3.right * side * Time.fixedDeltaTime;
        movement += Vector3.forward * move * moveSpeed * Time.fixedDeltaTime;
        movement.y = 0.0f;
        Vector3 targetPosition = rigidBody.position + movement;

        if (isJumping && isGrounded)
        {
            jumpUp = true;
            fallDown = false;
            isJumping = false;
        }

        if (fallDown)
        {
            if (isGrounded)
            {
                jumpUp = false;
                fallDown = false;
                isJumping = false;
                jumpTime = 0;
                jumpHeight = 10;
                spacePress = 0;
                originalYPos = rigidBody.position.y;
            }
        }

        float newYposition = targetPosition.y;

        if (jumpUp || fallDown)
        {
            jumpTime += Mathf.Abs(Time.deltaTime);

            if (jumpUp)
            {
                newYposition = originalYPos + (jumpUpSpeed * jumpTime) + (0.5f * -9.8f * jumpTime * jumpTime);
            }

            else if (fallDown)
            {
                newYposition = originalYPos + (fallSpeed * jumpTime) + (0.5f * -9.8f * jumpTime * jumpTime);
            }
        }

        if ((jumpUp && !fallDown && newYposition >= jumpHeight) || !isKeySpaceDown)
        {
            fallDown = true;
            jumpUp = false;
        }

        /*if (jumpUp && !fallDown && newYposition > jumpHeight)
        {
            fallDown = true;
            jumpUp = false;
        }*/

        if (jumpUp || fallDown)
        {
            targetPosition.y = newYposition;
        }

        else if (!isKeySpaceDown)
        {
            targetPosition.y = OGYP;
        }

        if (movement.sqrMagnitude > Mathf.Epsilon || jumpUp || fallDown)
        {
            rigidBody.MovePosition(targetPosition);
        }
    }

    private bool isOnGround()
    {
        float lengthToSearch = 0.8f;

        Vector3 lineStart = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        Vector3 vectorToSearch = new Vector3(this.transform.position.x, lineStart.y - lengthToSearch, this.transform.position.z);

        Color color = new Color(0.0f, 0.0f, 1.0f);
        Debug.DrawLine(lineStart, vectorToSearch, color);

        RaycastHit hitInfo;
        if (Physics.Linecast(this.transform.position, vectorToSearch, out hitInfo))
        {
            if (hitInfo.distance < halfPlayerHeight)
            {
                return true;
            }
        }
        return false;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    GameObject obj = collision.gameObject;

    //    if (obj.tag.Equals("Enemy"))
    //    {
    //        if ((obj.name.Equals("Enemy1") || obj.name.Equals("Enemy3")) && transform.position.y > 1)
    //        {
    //            Destroy(obj);
    //        }

    //        else
    //        {
    //            health.size -= 0.1f;

    //            if (obj.name.Contains("1"))
    //            {
    //                obj.GetComponent<Enemy1>().playerAttaked = true;
    //            }

    //            else
    //            {
    //                obj.GetComponent<Enemy2>().playerAttaked = true;
    //                transform.Translate(Vector3.back);
    //            }
    //        }
    //    }

    //    else if (obj.name.Equals("Platform2") && transform.position.y - obj.transform.position.y < 1)
    //    {
    //        transform.Translate(Vector3.down);
    //    }

    //    else if (obj.name.Equals("DestinationLine"))
    //    {
    //        Debug.Log("You win");
    //    }
    //}
}
