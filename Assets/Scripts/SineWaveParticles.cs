using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//from gpt asking how to make particles move in a sine wave
public class SineWaveParticles : MonoBehaviour
{
    public ParticleSystem particleSys; // Reference to the particle system
    public float amplitude = 1f; // Amplitude of the sine wave
    public float frequency = 1f; // Frequency of the sine wave
    public float speed = 1f; // Speed of particle movement

    private ParticleSystem.Particle[] particles; // Array to store particle data

    void Start()
    {
        // Ensure the particle system is not null
        if (!particleSys)
        {
            particleSys = GetComponent<ParticleSystem>();
        }
    }

    void LateUpdate()
    {
        if (particleSys == null)
            return;

        // Allocate particle array if not already allocated
        if (particles == null || particles.Length < particleSys.main.maxParticles)
        {
            particles = new ParticleSystem.Particle[particleSys.main.maxParticles];
        }

        // Get particles
        int particleCount = particleSys.GetParticles(particles);

        // Modify particle positions to follow a sine wave
        for (int i = 0; i < particleCount; i++)
        {
            Vector3 position = particles[i].position;
            float timeOffset = position.z * frequency; // Adjust by z-position for wave effect
            position.y = Mathf.Sin((Time.time * speed) + timeOffset) * amplitude;
            particles[i].position = position;
        }

        // Apply the modified particle data back to the system
        particleSys.SetParticles(particles, particleCount);
    }
}

