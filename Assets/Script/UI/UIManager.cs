using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text ammo;
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
        int index = 2;//Random.Range(1, 4);
        SceneManager.LoadScene(index);
    }

    

}
