using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    private void Start()
    {
         
    }

    // Text Fields
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI hitpointText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI upgradeCostText;

    // Logic Fields
    private int currentCharacterSelect = 0;
    public Image characterSelectSprite, weaponSprite;
    public RectTransform xpBar;

    // Character Selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelect++;

            // If no more selection
            if (currentCharacterSelect == GameManager.instance.playerSprites.Count)
            {
                currentCharacterSelect = 0;
            }

            OnSelectChange();
        }
        else
        {
            currentCharacterSelect--;

            // If no more selection
            if (currentCharacterSelect < 0)
            {
                currentCharacterSelect = GameManager.instance.playerSprites.Count - 1;
            }

            OnSelectChange();
        }
    }
    private void OnSelectChange()
    {
        characterSelectSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelect];
        GameManager.instance.player.SwapSprite(currentCharacterSelect);
    }

    // Weapon Upgrade
    public void OnUpgradeClick()
    {
        // Buy Weapon Upgrade & Upgrade
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    // Update Charater Information
    public void UpdateMenu()
    {
        //Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];

        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        // Meta
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitpoints.ToString();
        goldText.text = GameManager.instance.gold.ToString();

        // XP Bar
        int currLevel = GameManager.instance.GetCurrentLevel();
        if (currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total experience points"; // Display total XP 
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevel - prevLevelXp;
            int currXPIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXPIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXPIntoLevel.ToString() + " / " + diff;
        }

    }


}
