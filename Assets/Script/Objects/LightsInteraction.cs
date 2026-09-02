using UnityEngine;

public class LightsInteraction : MonoBehaviour, IInteractable
{
    public GameObject player;
    public KeyPickUpTracker keyPickUpTracker;
    public GameObject lights;
    public LightsOn lighson;
    public GameObject key;
    public KeyPlace keyPlaced;
    public void OnInteract()
    {
        if (keyPickUpTracker.hasPowerSwitch) { 
        lighson.LightsInteract();
        keyPlaced.keyPlaced();
            keyPickUpTracker.usedLightKey();
        }
       
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        lighson=lights.GetComponent<LightsOn>();
        keyPlaced=key.GetComponent<KeyPlace>();
        keyPickUpTracker = player.GetComponent<KeyPickUpTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
