using UnityEngine;
using UnityEngine.AI;

public class EnemyReferences : MonoBehaviour
{
    public NavMeshAgent navMeshagent;
    public Animator animator;

    private void Awake()
    {
        navMeshagent=GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();  
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
