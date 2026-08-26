using UnityEngine;

public class KeyPickUpTracker : MonoBehaviour
{
    public bool hasKey=false;
    public bool hasPowerSwitch=false;
    public void pickUpLightKey()
    {
        hasPowerSwitch = true;
    }
    public void usedLightKey()
    {
        hasPowerSwitch=false;
    }
    public void pickedUpKey()
    {
        hasKey = true;
    }
    public void usedKey()
    {
        hasKey=false;
    }
}
