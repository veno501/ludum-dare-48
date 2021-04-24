using UnityEngine;
using System.Collections;

namespace Weapons
{
	public class PlayerWeapon : PlayerModule
	{
		[SerializeField]
		float fireRPM;
		public float FireDelay
		{
			get { return 1f / fireRPM; }
			set
			{
				fireRPM = 1f / value;
				fireDelayYield = new WaitForSeconds(1f / fireRPM);
			}
		}

		protected bool delayIsReady;

		protected Coroutine delayAfterFireCoroutine;
		WaitForSeconds fireDelayYield;

		[Space]

		public Damage damage;
		// protected ObjectPool currentObjectPool = null;
		// protected GameObject poolObject = null;
		
		public virtual bool IsReady
		{
			get { return delayIsReady && PlayerWeapons.active; }
		}

		protected virtual void Start()
		{
			fireDelayYield = new WaitForSeconds(FireDelay);
			delayIsReady = true;
		}

		public virtual void Reset()
		{
			// cancel fire delay
			if (delayAfterFireCoroutine != null)
			{
				StopCoroutine(delayAfterFireCoroutine);
			}
			delayIsReady = true;

			// DestroyObjectPool();
			// CreateObjectPool();
		}

		protected virtual IEnumerator DelayAfterFire()
		{
			delayIsReady = false;
			yield return fireDelayYield;
			delayIsReady = true;
		}

		// protected void CreateObjectPool()
		// {
		// 	if (poolObject != null)
		// 	{
		// 		currentObjectPool = PoolManager.CreateObjectPool(poolObject);
		// 	}
		// }

		// protected void DestroyObjectPool()
		// {
		// 	if (poolObject != null && currentObjectPool != null)
		// 	{
		// 		PoolManager.DestroyObjectPool(currentObjectPool);
		// 		currentObjectPool = null;
		// 	}
		// }
	}
}