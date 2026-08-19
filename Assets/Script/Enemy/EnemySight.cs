using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    private EnemyReferences refs;

    [Header("Settings")]
    [SerializeField] private float detectionRange = 15f;
    [SerializeField] private float viewAngle = 90f;
    [SerializeField] private float losePlayerTimer = 1.2f;
    public Transform playerLastSpotted;
    public float timeSinceLostPlayer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       refs = GetComponent<EnemyReferences>(); 
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


 
 

    internal bool lostPlayer()
    {
        return !canSeePlayer() && timeSinceLostPlayer >= losePlayerTimer;
    } 
    void Update()
    {
        if (!canSeePlayer())
        {
            timeSinceLostPlayer += Time.deltaTime;
        }
        else
            timeSinceLostPlayer = 0f;
    }
}
