using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
	public class BasicTurret : MainWeapon
	{
		public GameObject projectile;
		public float projectileVelocity;
		public float projectileDrag;
		public float spreadAngle;
		public float kickbackForce;
		public Slider cooldownTimer;

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
			cooldownTimer.value = HeatLevel;
		}

		void FireWeapon ()
		{
			// var ob = currentObjectPool.GetObject();
			var ob = Instantiate(projectile) as GameObject;
			ob.GetComponent<BasicProjectile>().Spawn(this, 0.3f/*FireDelay * 3f*/);

			controller.movement.AddRecoil(kickbackForce);

			RaiseHeat();
			delayAfterFireCoroutine = StartCoroutine(DelayAfterFire());
		}
	}
}