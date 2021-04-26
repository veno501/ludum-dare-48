using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : Creature
{
    public LayerMask creatureLayer;

	[Space]
	public float targetingRange;
	public float targetingDelay;
    public float maxVelocity;

	[Space]
	public float chargeSpeed;
	public float chargeDuration;
    Collider2D[] nearby = new Collider2D[3];

    float timer;
	public enum State
	{
		Seeking, Targeting, Charging
	}

	State currentState = State.Seeking;
    Rigidbody2D target;

    void Start()
	{
        rbody = GetComponent<Rigidbody2D>();

		if (target == null)
            target = Player.rb;
	}

    void FixedUpdate()
	{
		switch (currentState)
		{
			case State.Seeking:

				Seek();
				break;

			case State.Targeting:

				ReadyCharge();
				break;

			case State.Charging:

				Charge();
				break;
		}

		UpdateRotation();
	}

    void ReadyCharge()
	{
		rbody.velocity = rbody.velocity.normalized * Mathf.Lerp(maxVelocity * 0.1f, maxVelocity, Mathf.Pow(timer / targetingDelay, 2f));

		timer -= Time.deltaTime;
		if (timer <= 0f)
		{
			currentState = State.Charging;
			timer = chargeDuration;
		}
	}

    void Charge()
	{
		rbody.velocity = rbody.velocity.normalized * Mathf.Lerp(0f, chargeSpeed, Mathf.Pow(timer / chargeDuration, 2f));

		timer -= Time.deltaTime;
		if (timer <= 0f)
		{
			currentState = State.Seeking;
			timer = 1f;
		}
	}

    void Seek()
	{
		Vector2 desired = target.position - rbody.position;
		Vector2 separate = SeparationForce(Random.Range(1.5f, 2.5f), nearby);
		SteerInDirection(desired.normalized + separate, 500f, maxVelocity);

		timer -= Time.deltaTime;
		if (timer <= 0 && desired.sqrMagnitude < targetingRange * targetingRange && 
			Vector2.Angle(rbody.velocity, desired) < 20f)
		{
			currentState = State.Targeting;
			timer = targetingDelay;
			// rbody.velocity = desired.normalized * maxVelocity;
		}
	}

    void SteerInDirection(Vector2 desired, float acceleration, float maxVelocity)
    {
        desired = desired.normalized * maxVelocity;

		if (acceleration == Mathf.Infinity)
		{
			rbody.velocity = desired;
			return;
		}

		Vector2 steer = desired - rbody.velocity;
		steer *= acceleration * Time.fixedDeltaTime;

		rbody.velocity = Vector2.ClampMagnitude(rbody.velocity + steer * Time.fixedDeltaTime, maxVelocity);
    }

    Vector2 SeparationForce(float range, Collider2D[] nearby)
	{
		int nearbyCount = Physics2D.OverlapCircleNonAlloc(rbody.position, 5f, 
            nearby, creatureLayer);

		if (nearbyCount <= 1)
			return Vector2.zero;
			
		Vector2 steer = new Vector2();
		for (int i = 0; i < nearbyCount; i++)
		{
			if (nearby[i].gameObject != gameObject)
			{
				Vector2 difference = rbody.position - nearby[i].attachedRigidbody.position;
				difference /= difference.sqrMagnitude;

				steer += difference;
			}
		}
		steer /= nearbyCount - 1;
		
		return steer;
	}

    void UpdateRotation()
	{
		Debug.DrawRay(transform.position, rbody.velocity.normalized * 3, Color.green);

		if (rbody.velocity == Vector2.zero)
			return;

		Vector2 aimDirection = rbody.velocity.normalized;
		float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg/* - 90f*/;

		rbody.MoveRotation(Mathf.LerpAngle(rbody.rotation, aimAngle, 1f - 35f * Time.fixedDeltaTime));
        // rbody.MoveRotation(aimAngle);
	}
}
