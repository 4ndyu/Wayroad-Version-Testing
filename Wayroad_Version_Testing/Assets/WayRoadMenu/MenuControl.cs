using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    public GameObject Panel;
    public GameObject confirmationScreen;

    public void toggleMenu()
    {
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
