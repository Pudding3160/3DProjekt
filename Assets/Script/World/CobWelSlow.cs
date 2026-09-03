using UnityEngine;

public class CobWelSlow : MonoBehaviour
{
    public GameObject playerObject;
    public PlayerControl playerControl;
    void Start()
    {
        
    }
    private void Awake()
    {
        playerObject = GameObject.FindWithTag("Player");
        playerControl = playerObject.GetComponent<PlayerControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerControl.walkSpeed = 1.8f;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerControl.walkSpeed = 3.0f;
    }
   


}
