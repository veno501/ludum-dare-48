using UnityEngine;
using Weapons;

public class PlayerWeapons : PlayerModule
{
	public MainWeapon mainWeapon;
	// public SideWeapon sideWeapon;

	public Transform mainMount;
	// public Transform sideMount;

	// public float chargeModifier = 1f;
	// float charge;
	// public float Charge
	// {
	// 	get { return charge; }
	// 	set
	// 	{
	// 		charge = Mathf.Clamp01(value);
	// 	}
	// }

	// public float ammoLossRate = 0.1f;

	public static bool active = true;

	protected override void Awake()
	{
		base.Awake();

		if (mainWeapon == null)
			mainWeapon = GetComponent<MainWeapon>();

		// if (sideWeapon == null)
		// 	sideWeapon = GetComponent<SideWeapon>();
	}

	public void OnUpdateScore(int scoreDifference)
	{
		// if (sideWeapon.Ammo > 0f)
		// 	return;
			
		// Charge += scoreDifference * chargeModifier * 0.001f;

		// if (Charge == 1f)
		// {
		// 	sideWeapon.ActivateCharge();
		// 	Charge = 0f;
		// }
	}

	// void Update()
	// {
	// 	sideWeapon.Ammo -= sideWeapon.maxAmmo * ammoLossRate * Time.deltaTime;

	// 	if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button0))
	// 	{
	// 		sideWeapon.ActivateCharge();
	// 		Charge = 0f;
	// 	}
	// }

	public void Reset()
	{
		// Charge = 0f;
		mainWeapon.Reset();
		// sideWeapon.Reset();
	}
}