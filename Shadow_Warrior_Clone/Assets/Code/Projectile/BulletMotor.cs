using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletMotor : MonoBehaviour
{
    Rigidbody m_rigid;

    [SerializeField] private float lifeDuration = 3;
    float lifeTimer = 0;

    [SerializeField] private float bulletSpeed = 0;
    [SerializeField] private Vector3 velocity = Vector3.zero;

    private TrailRenderer tr = null;

    public float Speed 
    { 
        set 
        { 
            bulletSpeed = value;
            velocity = bulletSpeed * transform.forward;
            lifeTimer = lifeDuration;
        } 
    }

    public bool go = false;

    void Awake()
    {
        m_rigid = GetComponent<Rigidbody>();
        velocity = bulletSpeed * transform.forward;
        tr = GetComponentInChildren<TrailRenderer>();
    }

    void FixedUpdate()
    {
        MoveBullet();

        lifeTimer -= Time.deltaTime;

        if (lifeTimer < 0)
        {
            tr.Clear();
            gameObject.SetActive(false);
        }
    }


    void MoveBullet()
    {
        Vector3 point1 = m_rigid.position;
        Quaternion rotation = m_rigid.rotation;
        
        velocity += (Physics.gravity * Time.deltaTime);

        Vector3 point2 = point1 + velocity * Time.deltaTime;

        Vector3 newDirection = point2 - point1;


        point1 = point2;

        m_rigid.MovePosition(point1);
        m_rigid.MoveRotation(Quaternion.LookRotation(newDirection));
    }
}
