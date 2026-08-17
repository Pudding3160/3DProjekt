using UnityEngine;

public class EnemyState_Patrol : IState
{
    private EnemyReferences refs;
    public Transform[] patrolPoints;
    public Transform patrolPointContainer;
    private float waitTimer;

    public EnemyState_Patrol(EnemyReferences refs, Transform[] patrolPoints)
    {
        this.refs = refs;
        this.patrolPoints = patrolPoints;
    }

    public void OnEnter()
    {
        PickNewPoint();
        waitTimer = 0f;
        Debug.Log("entered patrol");
    }

    public void Tick()
    {
        if (refs.navMeshagent.remainingDistance <= 0.2f)
        {
            waitTimer += Time.deltaTime;
            
            if (waitTimer >= 2f)
            {
              PickNewPoint();
                waitTimer = 0f;
            }
        }
    }


    public void OnExit()
    {
        // Optional cleanup
    }

    public Color GizmoColor()
    {
        return Color.blue;
    }

   private void PickNewPoint()
    {
        refs.navMeshagent.SetDestination(
                    patrolPoints[Random.Range(0, patrolPoints.Length)].position
                );
        waitTimer = 0f;
    }

}
