using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private StateMachine stateMachine;
    private EnemyReferences enemyRefs;
    public Transform[] patrolPoints;
    public Transform patrolPointContainer;
   

    private void Awake()
    {
        enemyRefs = GetComponent<EnemyReferences>();
        
        patrolPoints = patrolPointContainer.GetComponentsInChildren<Transform>();



        stateMachine = new StateMachine();   // IMPORTANT: StateMachine is NOT a MonoBehaviour
    }

    private void Start()
    {
        // States
        var patrolState = new EnemyState_Patrol(enemyRefs, patrolPoints);
        var idleState = new EnemyState_Idle(enemyRefs);
        

        // Initial State   
        stateMachine.SetState(idleState);
        // Transitions
        At(idleState, patrolState, PlayerClose());
        stateMachine.AddAnyTransition(patrolState,PlayerClose());
       
        void At(IState from, IState to, Func<bool> condition)
    => stateMachine.AddTransition(from, to, condition);

    }

   // private Func<bool> PlayerClose() => () => Vector3.Distance(transform.position, enemyRefs.player.transform.position) > 10f;

    private Func<bool> PlayerClose() => () =>
    {
        float distance = Vector3.Distance(
            transform.position,
            enemyRefs.player.transform.position
        );

        Debug.Log($"Player distance: {distance}");

        return distance >= 10f;
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
