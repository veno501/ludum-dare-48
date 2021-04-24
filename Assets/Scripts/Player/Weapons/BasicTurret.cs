using UnityEngine;

namespace Weapons
{
	public class BasicTurret : MainWeapon
	{
		public GameObject projectile;
		public float projectileVelocity;
		public float projectileDrag;
		public float spreadAngle;
		public float kickbackForce;

		// protected override void Start ()
		// {
		// 	base.Start();
		// 	poolObject = projectile;
		// 	CreateObjectPool();
		// }

		void Update ()
		{
			if (IsReady && controller.input.PrimaryFire)
			{
				FireWeapon();
			}
		}

		void FireWeapon ()
		{
			// var ob = currentObjectPool.GetObject();
			var ob = Instantiate(projectile) as GameObject;
			ob.GetComponent<BasicProjectile>().Spawn(this, FireDelay * 3f);

			controller.movement.AddRecoil(kickbackForce);

			RaiseHeat();
			delayAfterFireCoroutine = StartCoroutine(DelayAfterFire());
		}
	}
}