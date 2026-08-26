using UnityEngine;

public class Interact : MonoBehaviour
{
    public Transform InteractionSource;
    public float InteractRange;

    public void interact()
    {
        Ray r= new Ray(InteractionSource.position,InteractionSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.OnInteract();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
