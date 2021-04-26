using UnityEngine;

namespace Weapons
{
    public class BasicProjectile : Projectile
    {
        Damage damage;
        float drag;
        Rigidbody2D rbody;

        void Awake()
        {
            rbody = GetComponent<Rigidbody2D>();
        }

        public void Spawn (BasicTurret turret, float _lifetime/*, bool _isInPool*/)
        {
            base.Spawn(_lifetime/*, _isInPool*/);

            damage = turret.damage;
            drag = turret.projectileDrag;

            transform.position = Player.instance.weapons.mainMount.position;
            transform.rotation = Player.instance.weapons.mainMount.rotation * 
                Quaternion.Euler(0, 0, Random.Range(-turret.spreadAngle, turret.spreadAngle) * 0.5f);
            rbody.velocity = transform.up * turret.projectileVelocity * Random.Range(0.9f, 1.1f);
        }

        void FixedUpdate()
        {
            float agePercentage = 1f - lifetimeTimer / lifetime;
            rbody.velocity -= rbody.velocity * (agePercentage * drag);
        }

        void OnTriggerEnter2D(Collider2D hit)
        {
            if (hit.transform == Player.tr) return;
            if (hit.GetComponent<Projectile>()) return;
            // if (hit.GetComponent<EnemyController>() != null)
            // {
            //     DealDamage(hit.GetComponent<EnemyController>());
            // }
            if (hit.GetComponentInParent<Creature>()) {
                hit.GetComponentInParent<Creature>().TakeDamage(damage);
            }
            if (hit.GetComponentInParent<Sample>()) {
                hit.GetComponentInParent<Sample>().TakeDamage(damage);
            }
            Destroy(this.gameObject);
        }

        // void DealDamage(EnemyController enemy)
        // {
        //     enemy.health.TakeDamage(damage);
        //     Despawn();
        // }
    }
}