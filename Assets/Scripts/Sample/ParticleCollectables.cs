using UnityEngine;
using System.Collections.Generic;

public class ParticleCollectables : MonoBehaviour
{
	ParticleSystem ps;
	//List<ParticleCollisionEvent> collisionEvents;

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
		Player.instance.stats.CollectSample(0.1f);
	}

	void OnParticleTrigger()
	{
		List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
		int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
		
		for (int i = 0; i < numEnter; i++)
		{
			OnCollectParticle(enter[i]);
		}

		ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
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