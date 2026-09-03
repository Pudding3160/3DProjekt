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
    public GameObject entity;
    public AudioSource walk;

    private void Awake()
    {
        playerObject = GameObject.FindWithTag("Player");
        player = playerObject.transform;
        playerControl = playerObject.GetComponent<PlayerControl>();

        navMeshagent = GetComponent<NavMeshAgent>();
        animator = entity.GetComponent<Animator>();
        playerControl = playerObject.GetComponent<PlayerControl>();
        walk = GetComponent<AudioSource>();
    }
}