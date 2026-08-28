using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    private StateMachine stateMachine;
    private EnemyReferences enemyRefs;
    public Transform[] patrolPoints;
    public Transform patrolPointContainer;
    public Transform[] playerCirclePoints;
    public Transform playerCircleContainer;
    [Header("Attachments")]
    private EnemySight sight;
    private EnemyBite bite;

    public bool canSee = false;
    public bool canHear = false;
    public bool aggresive = false;

   
   

    private void Awake()
    {
        enemyRefs = GetComponent<EnemyReferences>();
        
        patrolPoints = patrolPointContainer.GetComponentsInChildren<Transform>();

        playerCirclePoints = playerCircleContainer.GetComponentsInChildren<Transform>();

        sight = GetComponent<EnemySight>();

        bite= GetComponent<EnemyBite>();    

   

        stateMachine = new StateMachine();  
    }

    private void Start()
    {
        //states
        var patrolState = new EnemyState_Patrol(enemyRefs, patrolPoints);
        var idleState = new EnemyState_Idle(enemyRefs);
        var chaseState= new EnemyState_Chase(enemyRefs);
        var circleState= new EnemyState_Circle(enemyRefs,playerCirclePoints);
        var attackState = new EnemyState_Bite(enemyRefs);
        var attackStunnedState= new EnemyState_AttackStunned(enemyRefs);

        //transitions

        
        stateMachine.AddTransition(patrolState, chaseState, PlayerInSightChase());
        stateMachine.AddTransition(patrolState, circleState, PlayerIsHeard());
        stateMachine.AddTransition(patrolState,chaseState, PlayerIsHeardChase());
        stateMachine.AddTransition(attackStunnedState, patrolState, StunOver());

        stateMachine.AddTransition(patrolState, attackState, PlayerInAttackRange());
        stateMachine.AddTransition(chaseState, attackState, PlayerInAttackRange());
    
        stateMachine.AddAnyTransition(attackStunnedState, Stun());
       
        stateMachine.AddTransition(patrolState, circleState, PlayerInSight());
        stateMachine.AddTransition(chaseState, patrolState, LostPlayer());
        stateMachine.AddTransition(circleState, patrolState, LostPlayer());

        //starting state
        stateMachine.SetState(patrolState);
    }

    private Func<bool> StunOver() => () =>
    {
        return !sight.gotShot();
    };


    private Func<bool> Stun() => () =>
    {
        return sight.gotShot();
    };

    private Func<bool> PlayerIsHeardChase()=>()=>
    {
        return sight.canHear() && aggresive && canHear;
    };
    private Func<bool> PlayerIsHeard()=>()=>
    {
        return sight.canHear() && !aggresive && canHear;
    };


    private Func<bool> PlayerInAttackRange() => () =>
    {

        return bite.canBite();
    };

   

    private Func<bool> LostPlayer() => ()=>{
       // Debug.Log($"Time since spotted: {sight.timeSinceLostPlayer}");
        return sight.lostPlayer();
        };

    private Func<bool> PlayerInSight() => () =>
    {
        return sight.canSeePlayer() && !aggresive && canSee;

    };
    private Func<bool> PlayerInSightChase() => () =>
    {
        return sight.canSeePlayer() && aggresive && canSee;


    };

    private Func<bool> PlayerClose() => () =>
    {
        float distance = Vector3.Distance(
            transform.position,
            enemyRefs.player.transform.position
        );


        return distance <= 50f;
    };

    private void Update()=>stateMachine.Tick();
    

    private void OnDrawGizmos()
    {
        if (stateMachine != null)
        {
            Gizmos.color = stateMachine.GetGizmoColor();
            Gizmos.DrawSphere(transform.position + Vector3.up * 2f, 0.3f);
        }
    }
}
