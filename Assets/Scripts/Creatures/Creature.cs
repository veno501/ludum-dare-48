using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public float health = 1f;
    public LayerMask canSeePlayerLayerMask;
    protected bool canSeePlayer;
    protected Rigidbody2D rbody;

    // void Awake()
    // {
    //     rbody = GetComponent<Rigidbody2D>();
    // }

    // void Update()
    // {
    //     rbody.velocity = new Vector2(Mathf.Sin(Time.time), Mathf.Cos(Time.time)) * 2f;
    // }

    protected void CheckCanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Linecast(rbody.position, Player.rb.position, canSeePlayerLayerMask);
        canSeePlayer = (hit != null);
    }

    public void TakeDamage(Damage _damage)
    {
        health -= _damage.amount;
        if (health <= 0f)
        {
            health = 0f;
            OnKilled();
        }
    }

    void OnKilled()
    {
        // spawn meat collectible?
        // call on creature killed for managers
        Destroy(this.gameObject);
    }
}
