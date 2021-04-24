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
	// public PlayerCanvas canvas;

	[Space]

	[SerializeField]
	SpriteRenderer spriteGraphic;

	// bool isVisible = true;
	// public static bool IsVisible
	// {
	// 	get { return instance.isVisible; }
	// 	set
	// 	{
	// 		instance.isVisible = value;
			
	// 		instance.spriteGraphic.gameObject.SetActive(value);
	// 		if (value == false)
	// 			instance.movement.thrusters.DisableThrusters();
	// 		else
	// 			instance.movement.thrusters.EnableThrusters();
	// 	}
	// }

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
		// canvas = GetComponentInChildren<PlayerCanvas>();
	}
}