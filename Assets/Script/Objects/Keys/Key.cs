using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public GameObject player;
    public KeyPickUpTracker tracker;
    public Transform[] spawns;
    private void Start()
    {
        int random = UnityEngine.Random.Range(0, spawns.Length);
        this.transform.position = spawns[random].position;
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
