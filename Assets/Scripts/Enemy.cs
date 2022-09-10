using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Enemy : Mover
{
    // Experience
    public int xpValue = 3;

    // Logic
    public float triggerLength = 0.3f;
    public float chaseLength = 1.5f;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    // Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];


    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild (0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // Is player in Range?
        if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
            {
                chasing = true;
            }

            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        //Checks for overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            //Clean array

            hits[i] = null;
        }

        UpdateMotor(Vector3.zero);

    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " XP!", 25, Color.green, transform.position, Vector3.up * 40, 1.0f);
    }
}
