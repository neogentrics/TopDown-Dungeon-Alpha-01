using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage Struct
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7};
    public float[] pushForce = { 2.0f, 2.3f, 2.5f, 3.2f, 3.4f, 3.8f, 4.2f};

    // Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    // Swing
    private Animator anim;
    private float coolDown = 0.5f;
    private float lastSwing;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > pushForce[weaponLevel])
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")
                return;

            // Create new Damage object, then send to Player
            Damage dmg = new Damage()
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            coll.SendMessage("ReceiveDamage", dmg);
            
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }
    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

    }
    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
