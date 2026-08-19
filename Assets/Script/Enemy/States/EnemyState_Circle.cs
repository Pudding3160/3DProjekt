using UnityEngine;

public class EnemyState_Circle :IState
{
    private EnemyReferences refs;
    public Transform[] circlePoints;
    public Transform circlePointContainer;
    private float waitTimer;

    public EnemyState_Circle(EnemyReferences refs, Transform[] circlePoints)
    {
        this.refs = refs;
        this.circlePoints = circlePoints;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tick()
    {
        waitTimer += Time.deltaTime;
        if (waitTimer >= 1.5f)
        {
            PickNewPoint();
        }
    }

    public void OnEnter()
    {
        Debug.Log("Entered Circle");
        PickNewPoint();
    }

    private void PickNewPoint()
    {
        refs.navMeshagent.SetDestination(
                   circlePoints[Random.Range(0, circlePoints.Length)].position
               );
        waitTimer = 0;
    }

    public void OnExit()
    {
        Debug.Log("Exited Circle");
    }

    public Color GizmoColor()
    {
        return Color.azure;
    }
}
