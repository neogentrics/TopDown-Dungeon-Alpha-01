using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    // Public Fields
    public int hitpoints = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.3f;

    // Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    // Push 
    protected Vector3 pushDirection;

    // All Fighters can RecieveDamage / Die
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoints -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.up * 50, 0.5f);

            if (hitpoints <= 0)
            {
                hitpoints = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {

    }
}
