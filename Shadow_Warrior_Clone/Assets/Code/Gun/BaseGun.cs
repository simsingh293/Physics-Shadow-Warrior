using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    public float fireRate = 2;
    public ParticleReset muzzleFlash = null;
    public Transform firePoint = null;
    public float VelocityMultiplier = 10;

    private Vector3 predictedVelocity = Vector3.zero;


    ObjectPooler pooler = null;

    GameObject test = null;

    private void Awake()
    {
        muzzleFlash = GetComponentInChildren<ParticleReset>();
        pooler = ObjectPooler.instance;
    }

    void Start()
    {
        pooler = ObjectPooler.instance;

        
    }

    void Update()
    {
        predictedVelocity = firePoint.forward * VelocityMultiplier;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            test = pooler.SpawnFromPool("Bullet", firePoint.position, firePoint.rotation);

            if(test != null &&  test.TryGetComponent(out BulletMotor bullet))
            {
                bullet.Speed = VelocityMultiplier;
                muzzleFlash.ShootParticles();
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 point1 = firePoint.position;
        float stepSize = 0.01f;
        Vector3 predictedBulletVelocity = predictedVelocity;
        for (float step = 0; step < 1; step += stepSize)
        {
            //predictedBulletVelocity += Physics.gravity * stepSize;
            Vector3 point2 = point1 + predictedBulletVelocity * stepSize;
            Gizmos.DrawLine(point1, point2);
            point1 = point2;
        }
    }
}
