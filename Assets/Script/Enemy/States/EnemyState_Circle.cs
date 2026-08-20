using UnityEngine;
using UnityEngine.AI;

public class EnemyState_Circle : IState
{
    private EnemyReferences refs;
    public Transform[] circlePoints;

    private float waitTimer;
    private int currentPointIndex = -1;

    private const float waitTime = 0.1f;

    public EnemyState_Circle(EnemyReferences refs, Transform[] circlePoints)
    {
        this.refs = refs;
        this.circlePoints = circlePoints;
    }

    public void Tick()
    {
        NavMeshAgent agent = refs.navMeshagent;

        // Still travelling
        if (agent.pathPending)
            return;

        // Hasn't reached the current point yet
        if (agent.remainingDistance > agent.stoppingDistance)
            return;

        // Wait before selecting the next point
        waitTimer += Time.deltaTime;

        if (waitTimer >= waitTime)
        {
            PickNewPoint();
        }
    }

    public void OnEnter()
    {
        Debug.Log("Entered Circle");

        waitTimer = 0f;
        PickFirstPoint();
    }

    private void PickFirstPoint()
    {
        if (circlePoints == null || circlePoints.Length == 0)
            return;

        currentPointIndex = Random.Range(0, circlePoints.Length);

        refs.navMeshagent.SetDestination(
            circlePoints[currentPointIndex].position
        );
    }

    private void PickNewPoint()
    {
        if (circlePoints == null || circlePoints.Length == 0)
            return;

        int closestIndex = -1;
        float closestDistance = Mathf.Infinity;

        Vector3 enemyPosition = refs.navMeshagent.transform.position;

        for (int i = 0; i < circlePoints.Length; i++)
        {
            
            if (i == currentPointIndex)
                continue;

            float distance = Vector3.Distance(
                enemyPosition,
                circlePoints[i].position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        if (closestIndex == -1)
            return;

        currentPointIndex = closestIndex;

        refs.navMeshagent.SetDestination(
            circlePoints[currentPointIndex].position
        );

        waitTimer = 0f;
    }

    public void OnExit()
    {
        Debug.Log("Exited Circle");
    }

    public Color GizmoColor()
    {
        return Color.cyan;
    }
}