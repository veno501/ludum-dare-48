using UnityEngine;
using Weapons;

public class Collectable : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D hit)
	{
		if (hit.GetComponent<Player>())
		{
			Collected();
		}
	}

	void Collected ()
	{
		// add mineral
		Destroy(gameObject);
	}
}