using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Boss : Enemy
{
    public float[] fireballSpeed = { 2.5f, 2.8f };
    public float distance = 0.25f;
    public Transform[] fireball;

    private void Update()
    {
        for (int i = 0; i < fireball.Length; i++)
        {
            fireball[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireballSpeed[i]) * distance, Mathf.Sin(Time.time * fireballSpeed[i]) * distance, 0);
        }
    }

}
