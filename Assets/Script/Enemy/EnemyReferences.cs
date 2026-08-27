using UnityEngine;
using UnityEngine.AI;

public class EnemyReferences : MonoBehaviour
{
    public NavMeshAgent navMeshagent;
    public Animator animator;
    public Transform player;
    public Transform playerLastPos;
    public bool stunned = false;
    public GameObject playerObject;
    public Transform enemyTransform;
    public PlayerControl playerControl;
    private void Awake()
    {
        navMeshagent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerControl=playerObject.GetComponent<PlayerControl>();
    }
}