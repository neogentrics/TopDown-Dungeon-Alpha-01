using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

         
    }
    protected override void ReceiveDamage(Damage dmg)
    {
        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitPointChange();
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }
    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }
    public void OnLevelUp()
    {
        maxHitpoint *= 2;
        hitpoints = maxHitpoint;
    }
    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
            OnLevelUp();
    }
    public void Heal(int healingAmount)
    {
        if (hitpoints == maxHitpoint)
            return;

        hitpoints += healingAmount;
        if (hitpoints > maxHitpoint)
            hitpoints = maxHitpoint;

        GameManager.instance.ShowText("+" + healingAmount.ToString() + "HP", 25, Color.cyan, transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.OnHitPointChange();
    }

}
