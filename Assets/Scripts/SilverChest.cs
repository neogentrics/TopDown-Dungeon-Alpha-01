using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverChest : Collectable
{
    [SerializeField] AudioSource collectSoundEffect;
    public Sprite emptyChest;
    public int goldAmount = 5;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.gold += goldAmount;
            GameManager.instance.ShowText("+" + goldAmount + " Gold!", 30, 
                Color.yellow, transform.position, Vector3.up * 50, 1.5f);
            collectSoundEffect.Play();
        }
    }
}
