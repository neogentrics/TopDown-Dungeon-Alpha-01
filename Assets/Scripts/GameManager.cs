using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(FloatingTextmanager.gameObject);     // Need to fix this
            Destroy(hud);
            Destroy(menu);
            return;
        }

        // PlayerPrefs.DeleteAll();

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
        
         
    }

    private void Update()
    {
        goldHUDText.text = (instance.gold.ToString() + " :Gold Coins");
        rubyHUDText.text = (instance.ruby.ToString() + " :Ruby Coins");
        levelText.text = ("LEVEL " + instance.GetCurrentLevel().ToString());
        hitpointText.text = "Health " + instance.player.hitpoints.ToString() + " / " + player.maxHitpoint.ToString();        
        int weaponLevel = instance.weapon.weaponLevel;
        weaponDamageHUDText.text = ("DAMAGE " + instance.weapon.damagePoint[weaponLevel].ToString());
        XPSystem();

        if (Input.GetKeyUp(KeyCode.P))
        {
            if (CharacterMenu.GameIsPaused)
            {
                CharacterMenu.Resume();
            }
            else
            {
                CharacterMenu.Paused();
            }
        }

    }

    // Resources

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;
    public TextMeshProUGUI hitpointText, levelText, goldHUDText, rubyHUDText, xpHUDText, weaponDamageHUDText;

    [SerializeField] AudioSource getHitSoundEffect;
    [SerializeField] AudioSource levelUpSoundEffect;

    // References
    public Player player;
    public Weapon weapon;
    public floatingTextManager FloatingTextmanager;
    public CharacterMenu CharacterMenu;
    public RectTransform hitpointBar, xpHUDBar;
    public GameObject hud, menu, deathMenu, deathMusic;

    // Logic
    public int gold;
    public int ruby;
    public int experience;


    // Floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingTextmanager.Show(msg, fontSize, color, position, motion, duration);
    }

    // Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // Is the Weapon max level?
        if(weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }

        if (gold >= weaponPrices[weapon.weaponLevel])
        {
            gold -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }

    // Hitpoint Bar
    public void OnHitPointChange()
    {
        float ratio = (float)player.hitpoints / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(ratio, 1, 1);
        getHitSoundEffect.Play();
    }

    // Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) // Max Level
            {
                return r;
            }
        }
        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }
    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;

        if (currLevel < GetCurrentLevel())
            OnLevelUp();


    }
    public void OnLevelUp()
    {
        player.OnLevelUp();
        OnHitPointChange();
        levelUpSoundEffect.Play();
    }

    private void XPSystem()
    {
        int currLevel = GetCurrentLevel();
        if (currLevel == xpTable.Count)
        {
            xpHUDText.text = experience.ToString() + " Max XP Level"; // Display total XP 
            xpHUDBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXPIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXPIntoLevel / (float)diff;            
            xpHUDBar.localScale = new Vector3(completionRatio, 1, 1);
            xpHUDText.text = (currXPIntoLevel.ToString() + " / " + diff + " xp");
        }
    }

// On Scene Loaded
public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    // Death Menu & Respawn
    public void Respawn()
    {
        deathMenu.SetActive(false);
        Time.timeScale = 1.0f;
        deathMusic.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn();
    }

    // Save State "Save Game Data"
    /*
     * INT preferredSkin
     * INT gold
     * INT experience
     * INT weaponLevel
     */

    public void SaveState()
    {
        
        string s = "";

        s += "0" + "|";
        s += gold.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // Change Player Skin
        gold = int.Parse(data[1]);

        // Experience
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

        // Change weapon level;
        weapon.SetWeaponLevel(int.Parse(data[3]));

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
}
