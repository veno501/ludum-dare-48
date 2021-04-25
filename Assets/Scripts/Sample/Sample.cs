using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    public float worth = 1f;
    public float health = 1f;
    public GameObject collectable;

    public void TakeDamage(Damage _damage)
    {
        health -= _damage.amount;
        if (health <= 0)
        {
            DropCollectable();
        }
    }

    void DropCollectable()
    {
        GameObject ob = Instantiate(collectable, transform.position, Quaternion.identity) as GameObject;
        // ob.GetComponent<Rigidbody2D>().velocity = (Player.tr.position - transform.position).normalized * 5f;

        Destroy(this.gameObject);
    }
}
