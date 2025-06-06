using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void loadGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
