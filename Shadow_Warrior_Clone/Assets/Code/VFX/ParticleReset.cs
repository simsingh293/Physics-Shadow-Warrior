using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleReset : MonoBehaviour
{
    public List<ParticleSystem> particles = new List<ParticleSystem>();

    private void Awake()
    {
        var _particles = GetComponentsInChildren<ParticleSystem>();

        foreach(var par in _particles)
        {
            particles.Add(par);
        }
    }

    public void ShootParticles()
    {
        foreach (var particle in particles)
        {
            particle.time = 0;
            particle.Play();
        }
    }
}
