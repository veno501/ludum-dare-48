using UnityEngine;
using System.Collections;

namespace Weapons
{
	public class MainWeapon : PlayerWeapon
	{
		float heatLevel;
		public float HeatLevel
		{
			get { return heatLevel; }
			set { heatLevel = Mathf.Clamp01(value); }
		}
		public Vector2 heatAndCoolSpeed = new Vector2();

		public float coolDelay;
		WaitForSeconds coolDelayYield;
		Coroutine coolCoroutine;

		public override bool IsReady
		{
			get { return (base.IsReady && HeatLevel < 1f); }
		}

		protected override void Start()
		{
			base.Start();
			coolDelayYield = new WaitForSeconds(coolDelay);
			HeatLevel = 0f;
		}

		public override void Reset()
		{
			base.Reset();
			HeatLevel = 0f;
		}

		public void RaiseHeat()
		{
			HeatLevel += 1f * heatAndCoolSpeed.x;

			if (coolCoroutine != null)
			{
				StopCoroutine(coolCoroutine);
			}

			if (HeatLevel == 1f)
			{
				coolCoroutine = StartCoroutine(CoolWithDelay(true));
			}
			else
			{
				coolCoroutine = StartCoroutine(CoolWithDelay(false));
			}
		}

		IEnumerator CoolWithDelay (bool _overheated)
		{
			if (_overheated)
			{
				yield return new WaitForSeconds(coolDelay * 4f);
			}
			yield return coolDelayYield;

			while (HeatLevel > 0)
			{
				yield return null;
				HeatLevel -= (1f * heatAndCoolSpeed.y) * Time.deltaTime;
			}
		}
	}
}