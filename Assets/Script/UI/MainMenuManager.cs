
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject progressResetText;
    public GameStateTracker progress;
    private void Awake()
    {
        GameObject ui = GameObject.Find("ui");
        progressResetText = ui.transform.Find("ProgressReset").gameObject;
        progressResetText.SetActive(false);
        progress = FindFirstObjectByType<GameStateTracker>();
    }
    public void OnStartPress()
    {
        int index = Random.Range(3, 4);
        SceneManager.LoadScene(index);
    }
    public void OnTutorialPress()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void OnQuitPress()
    {
        Application.Quit();
    }

    public void OnReseProgressCLick()
    {
        progressResetText.SetActive(true);
        Destroy(progressResetText, 2);
        
        progress.ResetProgress();

    }
}
