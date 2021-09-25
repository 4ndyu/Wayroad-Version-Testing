using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splashScript : MonoBehaviour
{
    public GameObject menu;

    public void toggleMenu()
    {
        if (menu.activeInHierarchy)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timer(5));
    }

    IEnumerator timer(int seconds)
    {
        toggleMenu();
        var waterMark = GameObject.Find("WaterMarkLogo");
        waterMark.SetActive(false);

        yield return new WaitForSecondsRealtime(seconds);

        GameObject.Find("SplashScreen").SetActive(false);
        toggleMenu();
        waterMark.SetActive(true);
    }
}
