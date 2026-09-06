using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{

    public static bool brokePersonalBest;
    public static void loadCreditsMenu()
    {
        SceneManager.LoadScene("CreditsMenu");
    }
    public static void loadMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public static void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void loadShopMenu()
    {
        SceneManager.LoadScene("ShopMenu");
    }

    public static void loadOptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
    }
    public static void loadGameOverScreen()
    {
        SceneManager.LoadScene("GameOverScreen");
    }

    public static void quitGame()
    {
        Application.Quit();
    }
}
