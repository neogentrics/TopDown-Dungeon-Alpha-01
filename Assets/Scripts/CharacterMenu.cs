using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class CharacterMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    // Text Fields
    public TextMeshProUGUI upgradeCostText;

    // Logic Fields
    private int currentCharacterSelect = 0;
    public Image characterSelectSprite, weaponSprite;
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public GameObject pauseMenuUI, controlMenu, BGSound;

    Resolution[] resolutions;

    // Menu Pause Function
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        controlMenu.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }
    public void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        pauseMenuUI.SetActive(false);
        BGSound.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }


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
                
    }

    // Sound Controls
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetAudio(float volume)
    {
        audioMixer.SetFloat("audio", volume);
    }
    public void SetMusic(float volume)
    {
        audioMixer.SetFloat("music", volume);
    }

    // Resolutions & Screen Display
    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
