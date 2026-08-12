using UnityEngine;

public class PatrolArea : MonoBehaviour
{
    private PatrolPoint[] points;

    private void Awake()
    {
        points = GetComponentsInChildren<PatrolPoint>();

    }

    public PatrolPoint GetRandomPoint(Vector3 agentLocation)
    {
        return points[Random.Range(0,points.Length)];
    }
}
