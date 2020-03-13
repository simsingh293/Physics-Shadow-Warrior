using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTester : MonoBehaviour
{
    ObjectPooler pooler;
    VFXPooler FXpooler;
    public Transform muzzle;
    public GameObject muzzleFlash = null;
    public float bulletSpeed = 0;

    private GameObject testBulletObj = null;
    private ProjectileMovement testProjectile = null;

    void Start()
    {
        pooler = ObjectPooler.instance;
        FXpooler = VFXPooler.Instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            testBulletObj = pooler.SpawnFromPool("Bullet01", muzzle.position, muzzle.rotation);

            

            if (testBulletObj.TryGetComponent(out ProjectileMovement projectile))
            {
                testProjectile = projectile;

                testProjectile.SetVelocity(bulletSpeed, muzzle.forward);

                testProjectile = null;
                testBulletObj = null;
            }
        }
    }
}
