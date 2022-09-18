
using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;


    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void ReceiveDamage(Damage dmg)
    {
        if(!isAlive)
            return;
        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitPointChange();
        
    }
    protected override void Death()
    {
        isAlive = false;
        Time.timeScale = 0f;
        GameManager.instance.deathMenu.SetActive(true);
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        
        if (isAlive)
        {
            UpdateMotor(new Vector3(x, y, 0));
            
        }
            
    }
    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }
    public void OnLevelUp()
    {
        maxHitpoint *= 2;
        hitpoints = maxHitpoint;
        GameManager.instance.ShowText("+" + 5 + " Ruby!", 30, Color.magenta, transform.position, Vector3.up * 50, 1.5f);
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
    public void Respawn()
    {
        Heal(maxHitpoint);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }

}
