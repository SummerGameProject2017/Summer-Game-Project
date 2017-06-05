using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour {

    public ParticleSystem particleLauncher;
    public ParticleSystem splatterParticles;
    public Gradient particleColorGradient;
    public ParticleDecalPool splatDecalpool;

    List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();

    }

    void OnParticleCollision(GameObject other)
    {
        if (particleColorGradient != null && splatDecalpool != null)
        {
            ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);
            for (int i = 0; i < collisionEvents.Count; i++)
            {
                splatDecalpool.ParticleHit(collisionEvents[i], particleColorGradient);
                EmitAtLocation(collisionEvents[i]);
            }
        }
    }

    void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        splatterParticles.transform.position = particleCollisionEvent.intersection;
        splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        splatterParticles.Emit(1);
    }


}
