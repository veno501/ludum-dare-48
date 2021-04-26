using UnityEngine;
using Weapons;

public class Collectable : MonoBehaviour
{
	public float pickupRange = 7f;
	Rigidbody2D rbody;

	void Awake()
	{
		rbody = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		float d = Vector2.Distance(Player.rb.position, rbody.position);
		if (d <= pickupRange && d > 0.01f)
		{
			rbody.velocity = 50f * (Player.rb.position - rbody.position) / (d*d);
		}
	}

	void OnTriggerEnter2D(Collider2D hit)
	{
		if (hit.GetComponentInParent<Player>())
		{
			OnCollected();
		}
	}

	void OnCollected()
	{
		// add mineral
		Player.instance.stats.CollectSample(0.34f);
		Destroy(gameObject);
	}
}