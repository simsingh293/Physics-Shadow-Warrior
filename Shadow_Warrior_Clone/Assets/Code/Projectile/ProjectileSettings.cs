using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Settings.asset", menuName = "Projectiles/Settings")]
public class ProjectileSettings : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float hitRadius;
    [SerializeField] private float forceMultiplier;


    
    public float Damage { get { return damage; } }
    public float HitRadius { get { return hitRadius; } }
    public float ForceMult { get { return forceMultiplier; } }
}
