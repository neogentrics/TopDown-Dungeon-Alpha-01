using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldChest : Collectable
{
    [SerializeField] AudioSource collectSoundEffect;
    public Sprite emptyChest;
    public int rubyAmount = 5;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.ruby += rubyAmount;
            GameManager.instance.ShowText("+" + rubyAmount + " Ruby!", 30, Color.magenta, transform.position, Vector3.up * 50, 1.5f);
            collectSoundEffect.Play();
        }
    }
}
