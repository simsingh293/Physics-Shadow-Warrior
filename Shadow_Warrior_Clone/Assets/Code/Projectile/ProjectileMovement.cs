using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float predictionStepsPerFrame = 6;
    //public float bulletSpeed = 3;
    public Vector3 bulletVelocity = Vector3.zero;
    public Transform firePoint = null;

    Coroutine currentCo = null;

    public float activeTime = 2;
    public float timeActive = 0;


    public void SetVelocity(float _bulletSpeed, Vector3 _forward)
    {
        bulletVelocity = _forward * _bulletSpeed;
        currentCo = StartCoroutine(ActiveBullet());
    }

    void BulletMovement()
    {
        Vector3 point1 = transform.position;
        Vector3 travelDirection = transform.forward;
        float stepSize = 1.0f / predictionStepsPerFrame;
        
        for (float step = 0; step < 1; step += stepSize)
        {
            bulletVelocity += Physics.gravity * stepSize * Time.deltaTime;
            Vector3 point2 = point1 + bulletVelocity * stepSize * Time.deltaTime;
            Vector3 newDirection = point2 - point1;

            Ray ray = new Ray(point1, newDirection);

            if(Physics.Raycast(ray, (point2 - point1).magnitude))
            {
                Debug.Log("Hit");

            }


            
            point1 = point2;
            travelDirection = newDirection;
        }

        transform.position = point1;
        transform.forward = travelDirection;
    }


    IEnumerator ActiveBullet()
    {
        while (timeActive < activeTime)
        {
            timeActive += Time.deltaTime;
            BulletMovement();
            yield return null;
        }

        yield return new WaitForEndOfFrame();

        //StopCoroutine(currentCo);
        //currentCo = null;
        currentCo = StartCoroutine(DeactivateBullet());

    }

    IEnumerator DeactivateBullet()
    {
        yield return null;

        timeActive = 0;
        StopCoroutine(currentCo);
        currentCo = null;
        gameObject.SetActive(false);
    }
}
