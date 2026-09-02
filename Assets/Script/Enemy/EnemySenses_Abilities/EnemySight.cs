using System;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    private EnemyReferences refs;

    [Header("Settings")]
    [SerializeField] private float detectionRange = 25f;
    [SerializeField] private float hearRange = 20f;
    [SerializeField] private float longHearRange = 35f;
    [SerializeField] private float viewAngle = 90f;
    [SerializeField] private float losePlayerTimer = 1.2f;
    private CharacterController charController; 
    public GameObject player;
    private float overallSpeed;
    public Transform playerLastSpotted;
    public float timeSinceLostPlayer;
    public bool shot = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       refs = GetComponent<EnemyReferences>();
        charController = player.GetComponent<CharacterController>();
    }

    public bool canSeePlayer()
    {
        return IsFacingPlayer() && HasClearPath() && PlayerInRange();
    }

    private bool PlayerInRange()
    {
        float distance = Vector3.Distance(
          transform.position,
          refs.player.transform.position
      );

        return distance<= detectionRange;    
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
        //DEBUG TO CHECK PLAYER SPEED
        //Debug.Log(overallSpeed);
        if(overallSpeed>4.5f&& distance<longHearRange)
            return true;
        else
        return distance <= hearRange && overallSpeed > 3f;
    }
    private bool HasClearPath()
    {
        var dirToPlayer= refs.player.position-transform.position;
        if(Physics.Raycast(transform.position,dirToPlayer.normalized,out RaycastHit hit, dirToPlayer.magnitude))
        {
            return hit.transform == refs.player;
        }

        return true;
    }

    private bool IsFacingPlayer()
    {
        var dirToPlayer=(refs.player.position-transform.position).normalized;
        var angle=Vector3.Angle(transform.forward, dirToPlayer);
        return angle < viewAngle / 2f;
    }

    public void playerhit()
    {
        refs.stunned=true;
    }

    public bool gotShot()
    {
        return refs.stunned;
    }
    
 

    internal bool lostPlayer()
    {
        return !canSeePlayer() && timeSinceLostPlayer >= losePlayerTimer;
    } 
    void Update()
    {
        if (!canSeePlayer() && !canHear())
        {
            timeSinceLostPlayer += Time.deltaTime;
        }
        else
            timeSinceLostPlayer = 0f;
    }
}
