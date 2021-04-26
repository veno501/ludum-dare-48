using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : PlayerModule
{
	public float maxHealth = 1f;
	// public float MaxShield
	// {
	// 	get { return maxHealth * 0.25f; }
	// }
	
	float health;
	public float Health
	{
		get { return health; }
		set
		{
			health = Mathf.Clamp(value, 0, maxHealth);
			if (bar)
			{
				bar.maxValue = maxHealth;
				bar.value = health;
			}
		}
	}

	// float shield;
	// public float Shield
	// {
	// 	get { return shield; }
	// 	set
	// 	{
	// 		shield = Mathf.Clamp(value, 0, Mathf.Min(maxHealth - Health, MaxShield));
	// 	}
	// }
	// public float shieldMultiplier = 1f;

	public float resistanceModifier = 1f;
	public Slider bar;

	// [HideInInspector]
	// public bool isInvulnerable;
	// public bool isGhost;

	protected override void Awake()
	{
		base.Awake();
		Reset();
	}

	// public void OnTriggerEnter2D(Collider2D hit)
	// {
	// 	if (hit.GetComponentInParent<Creature>()) {
	// 		hit.GetComponentInParent<Creature>().TakeDamage(new Damage(100f));
	// 	}
	// 	this.TakeDamage(new Damage(1.0f));
	// }

	public void OnCollisionEnter2D(Collision2D hit)
	{
		if (hit.transform.GetComponentInParent<Creature>()) {
			hit.transform.GetComponentInParent<Creature>().TakeDamage(new Damage(100f));
		}
		this.TakeDamage(new Damage(1.0f));
		
		// Player.rb.velocity = hit.
	}

	public void Reset()
	{
		Health = maxHealth;
		// UIManager.OnUpdateHealth(this);
	}

	public void TakeDamage(Damage _damage)
	{
		if (/*isInvulnerable || */Health == 0)
			return;

		float damageModified = _damage.amount / resistanceModifier;

		// if (damageModified > Shield)
		// {
			Health -= damageModified/* - Shield*/;
		// }
		// Shield -= damageModified;

		// UIManager.OnUpdateHealth(this);

		if (Health == 0)
		{
			OnEliminated(_damage);
		}
	}

	// public void OnUpdateScore(int scoreDifference)
	// {
	// 	AddShield(scoreDifference);
	// }

	// void AddShield (int score)
	// {
	// 	Shield += score * shieldMultiplier * 0.01f;
	// 	// UIManager.OnUpdateHealth(this);
	// }

	void OnEliminated (Damage _damage)
	{
		// MenuManager.InitEndgameMenu();
	}

	// public void SetInvulnerableForSeconds (float _t)
	// {
	// 	if (!isInvulnerable)
	// 	{
	// 		// Start invulnerable effect
	// 	}

	// 	StartCoroutine(SetInvulnerableForSecondsCoroutine(_t));
	// }

	// IEnumerator SetInvulnerableForSecondsCoroutine (float _t)
	// {
	// 	isInvulnerable = true;

	// 	yield return new WaitForSeconds(_t);

	// 	isInvulnerable = false;
	// 	// Stop invulnerable effect
	// }
}