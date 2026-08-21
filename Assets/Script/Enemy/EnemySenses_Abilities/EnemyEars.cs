using UnityEngine;

public class EnemyEars : MonoBehaviour
{
    private EnemyReferences refs;
    public GameObject player;
    [SerializeField] private float HearingRange = 15f;
    private CharacterController charController;
    private float overallSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        refs=GetComponent<EnemyReferences>();   
        charController=player.GetComponent<CharacterController>();
    }
    public bool canHear()
    {
        float distance = Vector3.Distance(
         transform.position,
         refs.player.transform.position
     );
        Vector3 horizontalVelocity = charController.velocity;
        horizontalVelocity = new Vector3(charController.velocity.x, 0, charController.velocity.z);

        // The speed on the x-z plane ignoring any speed
        float horizontalSpeed = horizontalVelocity.magnitude;
        // The speed from gravity or jumping
        float verticalSpeed = charController.velocity.y;
        // The overall speed
        overallSpeed = charController.velocity.magnitude;
        Debug.Log(overallSpeed);
        return distance <= HearingRange && overallSpeed>3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
