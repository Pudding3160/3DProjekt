using UnityEngine;

public class KeyPickUpTracker : MonoBehaviour
{
    public bool hasKey=false;
    public bool hasPowerSwitch=false;
    public GameObject keyIcon;
    public GameObject powerSwitchIcon;
    
    public void pickUpLightKey()
    {
        powerSwitchIcon.SetActive(true);
        hasPowerSwitch = true;
    }
    public void usedLightKey()
    {
        powerSwitchIcon.SetActive(false);
        hasPowerSwitch =false;
        
    }
    public void pickedUpKey()
    {
        keyIcon.SetActive(true);
        
        hasKey = true;
    }
    public void usedKey()
    {
        hasKey=false;
        keyIcon.SetActive(false);
        
    }
}
