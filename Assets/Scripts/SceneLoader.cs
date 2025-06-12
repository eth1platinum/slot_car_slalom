using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void loadMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void loadOptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
