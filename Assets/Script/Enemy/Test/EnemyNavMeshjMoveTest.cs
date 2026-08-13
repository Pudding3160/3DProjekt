using UnityEngine;

public class EnemyNavMeshjMoveTest : MonoBehaviour
{

    private EnemyReferences refs;
    public Transform[] patrolPoints;
    private float waitTimer;

    private void Awake()
    {
        
        refs = GetComponent<EnemyReferences>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waitTimer = 0;
        refs.navMeshagent.SetDestination(patrolPoints[Random.Range(0, patrolPoints.Length)].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!refs.navMeshagent.pathPending &&
            refs.navMeshagent.remainingDistance <= refs.navMeshagent.stoppingDistance)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= 0.5f)
            {
                refs.navMeshagent.SetDestination(
                    patrolPoints[Random.Range(0, patrolPoints.Length)].position
                );
                waitTimer = 0f;
            }
        }
    }

}
