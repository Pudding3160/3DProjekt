using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public GameObject player;
    public KeyPickUpTracker tracker;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        tracker =player.GetComponent<KeyPickUpTracker>();
    }
    public void OnInteract()
    {
        if (!tracker.hasKey) { 
        tracker.pickedUpKey();
        GameObject.Destroy(gameObject);
        }

    }

}
