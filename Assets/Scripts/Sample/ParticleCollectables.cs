using UnityEngine;
using System.Collections.Generic;

public class ParticleCollectables : MonoBehaviour
{
	ParticleSystem ps;
	//List<ParticleCollisionEvent> collisionEvents;
	// List<ParticleSystem.Particle> particleCache = new List<ParticleSystem.Particle>(10);
	ParticleSystem.Particle[] particleCache = new ParticleSystem.Particle[particlesSpawned];
	const int particlesSpawned = 10;

	void Start()
	{
		Spawn();
	}

	void Update()
	{
		if (!ps.IsAlive()) {
			Destroy(this.gameObject);
		}
	}

	public void Spawn()
	{
		ps = GetComponent<ParticleSystem>();
		//collisionEvents = new List<ParticleCollisionEvent>();
		var trigger = ps.trigger;
		trigger.SetCollider(0, Player.tr);

		var externalForcesModule = ps.externalForces;
        externalForcesModule.AddInfluence(Player.tr.GetComponentInChildren<ParticleSystemForceField>());
	}

	void OnCollectParticle(ParticleSystem.Particle particle)
	{
		// add minerals
		// StatsManager.Score += scoreOnCollect;
		Player.instance.stats.CollectSample(1.0f / particlesSpawned);
	}

	void OnParticleTrigger()
	{
		List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
		int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
		
		for (int i = 0; i < numEnter; i++)
		{
			OnCollectParticle(enter[i]);
			KillParticle(i);
		}

		ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
	}

	void KillParticle(int _index)
	{
		// var emitter = ps.emission;
		ps.GetParticles(particleCache, ps.particleCount);
		// particleCache = emitter.particles.ToList();
		// particleCache.Remove(_index);
		particleCache[_index] = particleCache[ps.particleCount-1];
		ps.SetParticles(particleCache, ps.particleCount-1);
        // emitter.particles = particleCache.ToArray();
	}

	//void OnParticleCollision (GameObject other)
	//{
	//	int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);

	//	Rigidbody rb = other.GetComponent<Rigidbody>();

	//	for (int i = 0; i < numCollisionEvents; i++)
	//	{
	//		if (rb != null)
	//		{
	//			Vector3 pos = collisionEvents[i].intersection;
	//			Vector3 force = collisionEvents[i].velocity * 10;
	//			rb.AddForce(force);
	//		}
	//	}
	//}
}