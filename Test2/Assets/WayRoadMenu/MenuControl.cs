using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    public GameObject Panel;
    
    public void toggleMenu()
    {
        if(Panel.activeInHierarchy)
        {
            Panel.SetActive(false);
        }
        else
        {
            Panel.SetActive(true);
        }
        
    }
}
