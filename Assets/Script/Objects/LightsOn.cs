using UnityEngine;

public class LightsOn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void LightsInteract()
    {
        gameObject.SetActive(true);
    }
}
