using UnityEngine;

public class PlayerMovement : PlayerModule
{
	//public float MaxSpeed
	//{
	//	get { return acceleration * speedModifier / (rbody.mass * rbody.drag); }
	//}

	public float acceleration;
	public float speedModifier = 1f;

	public Thrusters thrusters;

	public static bool active = true;

	Rigidbody2D rbody;

	protected override void Awake()
	{
		base.Awake();
		rbody = GetComponentInParent<Rigidbody2D>();
		thrusters = GetComponentInChildren<Thrusters>();
	}

	protected void FixedUpdate()
	{
		// if (ArenaManager.isOutOfBounds(rbody.position))
		// {
		// 	Vector2 arenaBoundsNormal = (ArenaManager.arenaCenter - rbody.position).normalized;

		// 	rbody.position = -arenaBoundsNormal * ArenaManager.arenaRadius;
		// 	rbody.velocity = arenaBoundsNormal * 30f;
		// }
		// else
		// {
			UpdateVelocity();
		// }
		
		UpdateRotation();
		thrusters.UpdateThrusters(controller.input.MoveVector);
	}

	void UpdateVelocity()
	{
		if (controller.input.MoveVector == Vector2.zero)
		{
			// rbody.velocity = rbody.velocity.normalized * Mathf.Max(rbody.velocity.magnitude, 3f);
			return;
		}

		Vector2 force = controller.input.MoveVector * (acceleration * speedModifier);
		Vector2 rotation = (controller.input.AimVector == Vector2.zero) ? 
			(Vector2)transform.up : controller.input.AimVector;

		// for going backwards
		if (Vector2.Dot(force.normalized, rotation) < -0.25f)
			force *= 0.75f;

		rbody.AddForce(force, ForceMode2D.Force);
	}

	void UpdateRotation()
	{
		if (controller.input.AimVector == Vector2.zero)
		{
			Vector2 inputMoveVector = controller.input.MoveVector;
			if (inputMoveVector != Vector2.zero)
			{
				float moveAngle = Mathf.Atan2(inputMoveVector.y, inputMoveVector.x) * Mathf.Rad2Deg - 90f;
				float rotateAngle = Mathf.LerpAngle(moveAngle, rbody.rotation, Time.fixedDeltaTime * 45f);

				rbody.rotation = rotateAngle;
			}
			return;
		}

		float aimAngle = Mathf.Atan2(controller.input.AimVector.y, controller.input.AimVector.x) * Mathf.Rad2Deg - 90f;

		// Causes interpolation to move the player slightly
		//rbody.MoveRotation(aimAngle);
		rbody.rotation = aimAngle;
	}

	public void AddRecoil(float velocity)
	{
		Vector2 recoil = new Vector2(Random.Range(-velocity, velocity) * 0.2f, -velocity);
		rbody.AddForce(transform.TransformDirection(recoil), ForceMode2D.Impulse);
	}
}