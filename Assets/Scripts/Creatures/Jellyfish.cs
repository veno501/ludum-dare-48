using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : Creature
{
    public LayerMask creatureLayer;
    public float dashDelay;
    public float dashVelocity;
    Collider2D[] nearby = new Collider2D[3];

    float dashTimer;
    Rigidbody2D target;
    Animator anim;

    void Start()
	{
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        dashTimer = dashDelay;

		if (target == null)
            target = Player.rb;
	}

    void FixedUpdate()
    {
        dashTimer -= Time.deltaTime;
        if (dashTimer <= 0f)
        {
            dashTimer = dashDelay;
            CheckCanSeePlayer();
            anim.SetTrigger("swim");
            Dash();
        }
        UpdateRotation();
    }

    void Dash()
    {
        // if (ArenaManager.isOutOfBounds(rbody.position))
        // {
        //     rbody.AddForce((ArenaManager.arenaCenter - rbody.position).normalized * dashVelocity, ForceMode2D.Impulse);
        //     return;
        // }
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        Vector2 playerDir = (target.position - rbody.position).normalized;
        Vector2 avoidDir = SeparationForce(3f, nearby);

        if (!canSeePlayer)
        {
            playerDir = Vector2.zero;
        }

        rbody.AddForce((randomDir * 0.8f + playerDir * 1.2f + avoidDir).normalized * dashVelocity, ForceMode2D.Impulse);
    }

    // void SteerInDirection(Vector2 desired, float acceleration, float maxVelocity)
    // {
    //     desired = desired.normalized * maxVelocity;

	// 	if (acceleration == Mathf.Infinity)
	// 	{
	// 		rbody.velocity = desired;
	// 		return;
	// 	}

	// 	Vector2 steer = desired - rbody.velocity;
	// 	steer *= acceleration * Time.fixedDeltaTime;

	// 	rbody.velocity = Vector2.ClampMagnitude(rbody.velocity + steer * Time.fixedDeltaTime, maxVelocity);
    // }

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
		float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;

		// rbody.MoveRotation(Mathf.LerpAngle(rbody.rotation, aimAngle, 1f - 35f * Time.fixedDeltaTime));
        rbody.MoveRotation(aimAngle);
	}
}
