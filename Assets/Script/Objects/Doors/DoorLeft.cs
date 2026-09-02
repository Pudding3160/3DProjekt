using UnityEngine;

public class DoorLeft : MonoBehaviour, IInteractable
{

    private bool isMoving = false;
    private float moveSpeed = 1.2f;
    private float movedDistance = 0f;
    public void OnInteract()
    {
        Debug.Log("works");
        isMoving = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
            return;

        float movement = moveSpeed * Time.deltaTime;

        transform.position += Vector3.back * movement;
        movedDistance += movement;

        if (movedDistance >= 3f)
        {
            isMoving = false;
        }

    }
}
