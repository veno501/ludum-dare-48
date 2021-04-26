using UnityEngine;

public class Flocker : Creature
{
    public LayerMask creatureLayer;

	[Header("Movement and Range")]

	public float maxSpeed = 10f;
	public float maxForce = 0.15f;
	public float fleetingRange = 8f;
	public Vector2 nearbyRange = new Vector2(1.5f, 4f);

	[Space]

	[Range(0, 100)]
	public int maxNearbyIterations = 100;

	[Header("Behaviour Weights")]

	[Range(0, 10)]
	public float separationWeight = 1f;
	[Range(0, 10)]
	public float alignmentWeight = 1f;
	[Range(0, 10)]
	public float cohesionWeight = 1f;

	[Space]

	[Range(-4, 10)]
	public float fleetingWeight = 4f;
	
	Collider2D[] nearby;
    Rigidbody2D target;

	// Renderer rend;
	int frameCounter;

	void Start()
	{
		rbody = GetComponent<Rigidbody2D>();
		rbody.velocity = Random.insideUnitCircle * maxSpeed;
        if (target == null)
            target = Player.rb;

		nearby = new Collider2D[maxNearbyIterations + 1];
		// rend = GetComponentInChildren<Renderer>();
	}

	void FixedUpdate()
	{
		if (Mathf.Repeat(frameCounter++, 5) != 0)
		{
			return;
		}

		Vector2 force = Vector2.zero;

		// if (rend.isVisible)
		// {
			force = Separation() + Alignment() + Cohesion();
		// }

		// Approximation
		// force = Vector2.ClampMagnitude(Bound(force - Fleeting(target)), maxForce);
		
		rbody.velocity = Vector2.ClampMagnitude(rbody.velocity + force, maxSpeed);

		// if (rend.isVisible)
		// {
			UpdateRotation();
		// }
	}

	// Vector2 Bound(Vector2 _velocity)
	// {
	// 	Vector2 border = ArenaManager.arenaBorder;
	// 	Vector2 desired = _velocity;

	// 	if (rbody.position.x < -border.x)
	// 		desired.x = maxSpeed;
	// 	else if (rbody.position.x > border.x)
	// 		desired.x = -maxSpeed;

	// 	if (rbody.position.y < -border.y)
	// 		desired.y = maxSpeed;
	// 	else if (rbody.position.y > border.y)
	// 		desired.y = -maxSpeed;

	// 	if (desired == rbody.velocity)
	// 		return Vector2.zero;

	// 	return Steer(desired, true) * 5f;
	// }

	Vector2 Steer(Vector2 _desired, bool setMagnitudeToMax)
	{
		if (setMagnitudeToMax)
		{
			_desired = _desired.normalized * maxSpeed;
		}

		Vector2 steer = _desired - rbody.velocity;
		//steer = Vector2.ClampMagnitude(steer, maxForce);

		return steer;
	}

	Vector2 Arrival(Rigidbody2D _target)
	{
		Vector2 desired = _target.position - rbody.position;

		// desired is first distance from target
		if (desired.magnitude < fleetingRange)
		{
			desired = desired.normalized * Mathf.Lerp(0, maxSpeed, desired.magnitude / fleetingRange);

			return Steer(desired, false) * -fleetingWeight;
		}

		return Steer(desired, true) * -fleetingWeight;
	}

	Vector2 Fleeting(Rigidbody2D _target)
	{
		Vector2 desired = rbody.position - (_target.position + _target.velocity / 3);

		if (desired.magnitude > fleetingRange)
			return Vector2.zero;
			
		//desired /= desired.sqrMagnitude;
		return Steer(desired, true) * fleetingWeight;
	}

	Vector2 Separation()
	{
		int nearbyCount = Physics2D.OverlapCircleNonAlloc(rbody.position, nearbyRange.x, nearby, 
			creatureLayer);

		if (nearbyCount <= 1)
			return Vector2.zero;
			
		Vector2 separation = new Vector2();
		for (int i = 0; i < nearbyCount; i++)
		{
			if (nearby[i].gameObject != gameObject)
			{
				Vector2 difference = rbody.position - nearby[i].attachedRigidbody.position;
				difference /= difference.sqrMagnitude;

				separation += difference;
			}
		}

		separation /= nearbyCount - 1;

		return Steer(separation, true) * separationWeight;
	}

	Vector2 Alignment()
	{
		int nearbyCount = Physics2D.OverlapCircleNonAlloc(rbody.position, nearbyRange.y, nearby,
			creatureLayer);
			
		if (nearbyCount <= 1)
			return Vector2.zero;

		Vector2 alignment = new Vector2();
		for (int i = 0; i < nearbyCount; i++)
		{
			if (nearby[i].gameObject != gameObject)
				alignment += nearby[i].attachedRigidbody.velocity;
		}

		return Steer(alignment, true) * alignmentWeight;
	}

	Vector2 Cohesion()
	{
		int nearbyCount = Physics2D.OverlapCircleNonAlloc(rbody.position, nearbyRange.y, nearby, 
			creatureLayer);

		if (nearbyCount <= 1)
			return Vector2.zero;

		Vector2 centerOfNearby = new Vector2();
		for (int i = 0; i < nearbyCount; i++)
		{
			if (nearby[i].gameObject != gameObject)
				centerOfNearby += nearby[i].attachedRigidbody.position;
		}

		centerOfNearby /= nearbyCount - 1;

		Vector2 desired = centerOfNearby - rbody.position;

		return Steer(desired, true) * cohesionWeight;
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