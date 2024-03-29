using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Tooltip("The text that shows the player's current score")]
    [SerializeField] TMP_Text currentScoreScoreText;
    [Tooltip("The text that shows the player's high score")]
    [SerializeField] TMP_Text highScoreScoreText;
    [Tooltip("The text that shows the player's ammo")]
    [SerializeField] TMP_Text ammoText;
    [Tooltip("The text that shows the player's ammo")]
    [SerializeField] Image weaponImage;
    [Tooltip("The prefebs for the heart image")]
    [SerializeField] GameObject heartImagePrefeb;
    [Tooltip("The parent for the heart objects that respresents player's life")]
    [SerializeField] Transform healthIconsParent;

    public void UpdateCurrentScore(int score)
    {
        try
        {
            // Update current score score text
            currentScoreScoreText.text = score.ToString();
        }
        catch
        {
            // Throw a debug message
            Debug.Log($"There is no TMP_Text component on {currentScoreScoreText.name}");
        }
    }

    public void UpdateHighScore(int score)
    {
        try
        {
            // Update high score score text
            highScoreScoreText.text = score.ToString();
        }
        catch
        {
            // Throw a debug message
            Debug.Log($"There is no TMP_Text component on {highScoreScoreText.name}");
        }
    }

    public void UpdateHealth(int health)
    {
        Debug.Log("Updating health");
        // If the required health is more than the heart icons on the screen now
        if (healthIconsParent.childCount < health)
        {
            // Get the difference
            int difference = health - healthIconsParent.childCount;

            // Set all heart icons to active
            for (int i = 0; i < healthIconsParent.childCount; i++)
            {
                healthIconsParent.GetChild(i).gameObject.SetActive(true);
            }

            // Instantiate hearts for the icon parent according to the difference
            for (int i = 0; i < difference; i++)
            {
                Instantiate(heartImagePrefeb, healthIconsParent);
            }
        }
        // If the heart icons on the screen now is sufficant to display the health
        else
        {
            try
            {
                // Get the difference
                int difference = healthIconsParent.childCount - health;

                // Set all heart icons to active
                for (int i = 0; i < healthIconsParent.childCount; i++)
                {
                    healthIconsParent.GetChild(i).gameObject.SetActive(true);
                }

                // Hide the unneccessary amount of heart icons
                for (int i = 0; i < difference; i++)
                {
                    healthIconsParent.GetChild(i).gameObject.SetActive(false);
                }
            }
            catch
            {
                // Throw a debug message
                Debug.Log("Health is negative!");
            }
        }
    }

    public void UpdateAmmo(int currentAmmo, int maxAmmo)
    {
        // Update the ammo text
        ammoText.text = $"Ammo: {currentAmmo} / {maxAmmo}";
    }

    public void UpdateWeaponSprite(Sprite sprite)
    {
        // Update the waepon's sprite
        weaponImage.sprite = sprite;
    }
}
