using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DoorUpKey : MonoBehaviour
{
    private bool isMoving = false;
    private float moveSpeed = 1f;
    private float movedDistance = 0f;
    public GameObject player;
    public KeyPickUpTracker tracker;

    private void Start()
    {
        tracker=player.GetComponent<KeyPickUpTracker>();
    }

    public void OnOpen()
    {
        

            isMoving = true;

        
    }

    private void Update()
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
