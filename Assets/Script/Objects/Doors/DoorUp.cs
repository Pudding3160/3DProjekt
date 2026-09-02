using UnityEngine;

public class DoorUp : MonoBehaviour, IInteractable
{
    private bool isMoving = false;
    private float moveSpeed = 1f;
    private float movedDistance = 0f;

    public void OnInteract()
    {
        Debug.Log("works");
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