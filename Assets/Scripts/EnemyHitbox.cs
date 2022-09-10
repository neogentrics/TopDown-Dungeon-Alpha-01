using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    // Damage
    public int damage = 1;
    public float pushForce = 4.2f;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            // Create new Damage Object, before sending to Player
            Damage dmg = new Damage()
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}
