using UnityEngine;
using System.Collections;

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

	// [HideInInspector]
	// public bool isInvulnerable;
	// public bool isGhost;

	protected override void Awake()
	{
		base.Awake();
		Reset();
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