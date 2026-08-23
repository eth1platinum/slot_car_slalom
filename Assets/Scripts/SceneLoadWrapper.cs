using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadWrapper : MonoBehaviour
{
    public void loadCreditsMenu()
    {
        SceneLoader.loadCreditsMenu();
    }
    public void loadMainGame()
    {
        SceneLoader.loadMainGame();
    }

    public void loadMainMenu()
    {
        SceneLoader.loadMainMenu();
    }

    public void loadShopMenu()
    {
        SceneLoader.loadShopMenu();
    }

    public void loadOptionsMenu()
    {
        SceneLoader.loadOptionsMenu();
    }

    public void quitGame()
    {
        SceneLoader.quitGame();
    }
}