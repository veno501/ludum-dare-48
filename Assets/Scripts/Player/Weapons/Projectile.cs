using UnityEngine;

public class Projectile : MonoBehaviour
{
	protected float lifetime;
	protected float lifetimeTimer;
	// protected bool isInPool;

	// Use playerProjectile, nonPlayerProjectile, Player and Enemy layers for collision in the future!

	protected void Spawn(float _lifetime/*, bool _isInPool*/)
	{
		lifetimeTimer = lifetime = _lifetime;
		// isInPool = _isInPool;

		gameObject.SetActive(true);
	}

	protected void Despawn()
	{
		// if (isInPool)
		// 	gameObject.SetActive(false);
		// else
			Destroy(gameObject);
	}
	
	protected virtual void Update()
	{
		lifetimeTimer -= Time.deltaTime;
		if (lifetimeTimer <= 0)
		{
			Despawn();
		}
	}
}