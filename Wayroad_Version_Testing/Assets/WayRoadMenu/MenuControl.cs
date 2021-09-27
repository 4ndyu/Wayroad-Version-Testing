using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    public GameObject Panel;
    public GameObject confirmationScreen;
    public GameObject navigationScreen;

    public void toggleMenu()
    {
        if (navigationScreen.activeInHierarchy)
        {
            navigationScreen.SetActive(false);
        }

        if (confirmationScreen.activeInHierarchy)
        {
            confirmationScreen.SetActive(false);
        }

        if (Panel.activeInHierarchy)
        {
            Panel.SetActive(false);
        }
        else
        {
            Panel.SetActive(true);
        }
        
    }
}
