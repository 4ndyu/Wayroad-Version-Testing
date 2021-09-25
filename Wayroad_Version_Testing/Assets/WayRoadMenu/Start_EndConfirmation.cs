using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_EndConfirmation : MonoBehaviour
{
    public GameObject confirmationScreen;
    // The location where the items are going be placed
    //public GameObject content;

    public void toggleMenu()
    {
        var menu = GameObject.Find("MenuPanel");

        if (menu.activeInHierarchy)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }
    }

    public void toggleConfirmationScreen()
    {
        if (confirmationScreen.activeInHierarchy)
        {
            confirmationScreen.SetActive(false);
        }
        else
        {
            confirmationScreen.SetActive(true);
        }
    }

    void Start()
    {
        confirmationScreen.SetActive(false);
    }

    public void goBack()
    {
        GameObject.Find("MenuButton").GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
    }

    /**
     *  Purpose: Displays the navigation confirmation
     *  
     *  Author: Andy Lam Yu
     */
    public void Go_Confirmation()
    {
        toggleMenu();
        toggleConfirmationScreen();

        GameObject.Find("ConfirmationScreen").GetComponentInChildren<UnityEngine.UI.Text>().text = "GO!";


        // Puts the name of the destination into the destination bar
        // Gets the name of the destination from the button text
        var destBar = GameObject.Find("DestinationBar").GetComponentInChildren<UnityEngine.UI.Text>().text = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponentInChildren<UnityEngine.UI.Text>().text;

        // Setting the text for a button if the user does not want the current location at their destination
        var changeLocation = GameObject.Find("ChangeLocationButton").GetComponentInChildren<UnityEngine.UI.Text>().text = "Different Location?";

        // Updates the changes of the text
        //temp.transform.parent = content.transform;
    }
}
