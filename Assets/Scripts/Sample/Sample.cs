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
        for (int i = 0; i < 5; i++)
        {
            Quaternion rot = Quaternion.Euler(0,0,(int)(Random.Range(0,360)/90)*90);
            GameObject ob = Instantiate(collectable, transform.position, rot) as GameObject;
            // ob.GetComponent<Rigidbody2D>().velocity = (Player.tr.position - transform.position).normalized * 5f;

		    // = transform.TransformDirection(recoil);
            ob.transform.SetParent(Level.instance.currentBlock.root);
        
            ob.GetComponent<Rigidbody2D>().velocity = ((Player.rb.position-(Vector2)transform.position) * 
                (Random.insideUnitCircle*1f)).normalized * 8f;
        }

        Destroy(this.gameObject);
    }
}
