using UnityEngine;

public class PlayerInput : PlayerModule
{
	public Camera mainCamera;

	public virtual Vector2 MoveVector
	{
		get { return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; }
	}

	public virtual Vector2 AimVector
	{
		get
		{
			return (mainCamera == null) ? Vector3.zero : 
				(Input.mousePosition - mainCamera.WorldToScreenPoint(transform.position));
		}
	}

	public virtual bool PrimaryFire
	{
		get { return Input.GetMouseButton(0); }
	}

	public virtual bool SecondaryFire
	{
		get { return Input.GetMouseButton(1); }
	}

	// public virtual bool SuperFire
	// {
	// 	get { return Input.GetKey(KeyCode.Space); }
	// }

	protected override void Awake()
	{
		base.Awake();

		if (mainCamera == null)
			mainCamera = Camera.main;
	}
}