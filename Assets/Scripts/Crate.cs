using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Fighter
{
    [SerializeField] AudioSource destroySoundEffect;
    protected override void Death()
    {
        destroySoundEffect.Play();
        Destroy(gameObject);        
    }
}
