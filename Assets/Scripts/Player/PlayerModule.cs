using UnityEngine;

public class PlayerModule : MonoBehaviour
{
	protected Player controller;

	protected virtual void Awake()
	{
		controller = GetComponentInParent<Player>();
	}
}