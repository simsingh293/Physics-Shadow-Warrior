using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTester : MonoBehaviour
{
    ObjectPooler pooler;
    public Transform muzzle;
    public float bulletSpeed = 0;

    private GameObject testBulletObj = null;
    private ProjectileMovement testProjectile = null;

    void Start()
    {
        pooler = ObjectPooler.instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            testBulletObj = pooler.SpawnFromPool("Bullet01", muzzle.position, muzzle.rotation);

            if(testBulletObj.TryGetComponent(out ProjectileMovement projectile))
            {
                testProjectile = projectile;

                testProjectile.SetVelocity(bulletSpeed, muzzle.forward);

                testProjectile = null;
                testBulletObj = null;
            }
        }
    }
}
