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
        // Create the patrol state
        var patrolState = new EnemyState_Patrol(enemyRefs, patrolPoints);

        // Set initial state
        stateMachine.SetState(patrolState);

        // If you want transitions later:
        // stateMachine.AddTransition(patrolState, chaseState, () => enemyRefs.CanSeePlayer);
    }

    private void Update()
    {
        stateMachine.Tick();
    }

    private void OnDrawGizmos()
    {
        if (stateMachine != null)
        {
            Gizmos.color = stateMachine.GetGizmoColor();
            Gizmos.DrawSphere(transform.position + Vector3.up * 2f, 0.3f);
        }
    }
}
