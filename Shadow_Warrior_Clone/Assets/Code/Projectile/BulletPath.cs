using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPath : MonoBehaviour
{
    [SerializeField] private Transform muzzle = null;
    [SerializeField] private float bulletSpeed = 3;
    [SerializeField] private Vector3 bulletVelocity = Vector3.zero;


    void Start()
    {
        muzzle = transform;
    }

    void Update()
    {
        bulletVelocity = bulletSpeed * muzzle.forward;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 point1 = this.transform.position;
        float stepSize = 0.01f;
        Vector3 predictedBulletVelocity = bulletVelocity;
        for(float step = 0; step < 1; step += stepSize)
        {
            predictedBulletVelocity += Physics.gravity * stepSize;
            Vector3 point2 = point1 + predictedBulletVelocity * stepSize;
            Gizmos.DrawLine(point1, point2);
            point1 = point2;
        }
    }
}
