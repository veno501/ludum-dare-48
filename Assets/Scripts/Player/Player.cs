using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player instance;
	public static Transform tr;
	public static Rigidbody2D rb;

	[Header("Modules")]

	public PlayerInput input;
	public PlayerMovement movement;
	public PlayerHealth health;
	public PlayerWeapons weapons;
	public PlayerStats stats;
	// public PlayerCanvas canvas;

	[Space]
	[SerializeField]
	SpriteRenderer spriteGraphic;

	void Awake ()
	{
		instance = this;

		tr = instance.transform;
		rb = instance.GetComponent<Rigidbody2D>();

		spriteGraphic = GetComponentInChildren<SpriteRenderer>();

		input = GetComponentInChildren<PlayerInput>();
		movement = GetComponentInChildren<PlayerMovement>();
		health = GetComponentInChildren<PlayerHealth>();
		weapons = GetComponentInChildren<PlayerWeapons>();
		stats = GetComponentInChildren<PlayerStats>();
		// canvas = GetComponentInChildren<PlayerCanvas>();
	}

	// void OnTriggerEnter2D(Collider2D hit)
	// {
	// 	health.OnTriggerEnter2D(hit);
	// }
	void OnCollisionEnter2D(Collision2D hit)
	{
		health.OnCollisionEnter2D(hit);
	}
}