using UnityEngine;

public class LightKey : MonoBehaviour, IInteractable
{
    public GameObject player;
    public KeyPickUpTracker tracker;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        tracker = player.GetComponent<KeyPickUpTracker>();
    }
    public void OnInteract()
    {
        if (!tracker.hasPowerSwitch)
        {
            tracker.pickUpLightKey();
            GameObject.Destroy(gameObject);
        }

    }
}
