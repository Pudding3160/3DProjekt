using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private TMP_Text ammo;
    public static UIManager instance;
    public void UpdateAmmo(string ammocount)
    {
        ammo.text = ammocount;
    }
    public void OnMenuPress()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OnNextLevelPress()
    {
        int index = Random.Range(2, 4);
        SceneManager.LoadScene(index);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


}
