using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnStartPress()
    {
        SceneManager.LoadScene("MapTest1");
    }
    public void OnTutorialPress()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void OnQuitPress()
    {
        Application.Quit();
    }
}
