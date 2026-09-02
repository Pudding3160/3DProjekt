using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text ammo;
    public void UpdateAmmo(string ammocount)
    {
        ammo.text = ammocount;
    }
    public void OnMenuPress()
    {

    }

}
