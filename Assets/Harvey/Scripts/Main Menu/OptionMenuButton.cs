using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenuButton : MonoBehaviour
{
    [Tooltip("The Option Menu parent game object")]
    [SerializeField] GameObject optionMenu;

    public void MenuToggle()
    {
        // If the option menu is currently active
        if (optionMenu.activeSelf == true)
        {
            // Show the option menu
            optionMenu.SetActive(false);
        }
        // If the option menu is currently inactive
        else
        {
            // Hide the option menu
            optionMenu.SetActive(true);
        }
    }
}
