using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] private float predictionStepsPerFrame = 6;
    [SerializeField] private Vector3 bulletVelocity = Vector3.zero;
    [SerializeField] private GameObject impactFlash = null;
    [SerializeField] private Transform objTransform = null;

    bool hittingObj = false;






    public Vector3 SetVelocity { set { bulletVelocity = value; } }







    private void Update()
    {
        if (!hittingObj)
        {
            // store current position in a variable
            Vector3 point1 = transform.position;

            // store current direction of travel in a variable, usually the forward of the projectile's transform
            Vector3 travelDirection = transform.forward;

            // not sure
            float stepSize = 1.0f / predictionStepsPerFrame;

            for (float step = 0; step < 1; step += stepSize)
            {
                // add outside forces that affect the bullet velocity
                bulletVelocity += Physics.gravity * stepSize * Time.deltaTime;

                // store the new position of the projectile with velocity added
                Vector3 point2 = point1 + bulletVelocity * stepSize * Time.deltaTime;

                // store the new forward direction of the projectile by subtracting the old position from the new position
                Vector3 newDirection = point2 - point1;

                Ray ray = new Ray(point1, newDirection);


                // Check for collisions
                if (Physics.Raycast(ray, (point2 - point1).magnitude))
                {
                    Debug.Log("Hit");
                    //bulletVelocity = Vector3.zero;
                }

                // store the new position in the variable of the old position, update the value
                point1 = point2;

                // store the new direction in the variable of the old direction, update the value
                travelDirection = newDirection;
            }

            // move bullet to the new position
            transform.position = point1;

            // update the projectile's forward direction to the new direction
            transform.forward = travelDirection; 
        }
        else
        {
            bulletVelocity = Vector3.zero;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        hittingObj = true;

        bulletVelocity = Vector3.zero;

        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 position = contact.point;

        if (impactFlash != null)
        {
            var hitVFX = GameObject.Instantiate(impactFlash, position, rotation);
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 point1 = this.transform.position;
        float stepSize = 0.01f;
        Vector3 predictedBulletVelocity = bulletVelocity;
        for (float step = 0; step < 1; step += stepSize)
        {
            predictedBulletVelocity += Physics.gravity * stepSize;
            Vector3 point2 = point1 + predictedBulletVelocity * stepSize;
            Gizmos.DrawLine(point1, point2);
            point1 = point2;
        }
    }
}
