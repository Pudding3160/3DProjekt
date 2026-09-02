using UnityEngine;

public class DoorOpenKey : MonoBehaviour, IInteractable
{
    public GameObject player;
    public KeyPickUpTracker keyPickUpTracker;
    public GameObject door;
    public DoorUpKey doorOpen;
    public GameObject key;
    public KeyPlace keyPlaced;
    private bool isMoving=false;
    private float moveSpeed=2.5f;
    private float movedDistance=0f;

    public void OnInteract()
    {
        if (keyPickUpTracker.hasKey) { 
        doorOpen.OnOpen();
        keyPlaced.keyPlaced();}
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        doorOpen = door.GetComponent<DoorUpKey>();
        keyPlaced=key.GetComponent<KeyPlace>();
        keyPickUpTracker=player.GetComponent<KeyPickUpTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
            return;

        float movement = moveSpeed * Time.deltaTime;

        transform.position += Vector3.up * movement;
        movedDistance += movement;

        if (movedDistance >= 4f)
        {
            isMoving = false;
        }
    }
}
