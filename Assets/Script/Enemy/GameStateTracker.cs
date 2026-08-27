using System;
using UnityEngine;


public class GameStateTracker : MonoBehaviour
{
    public GameObject enemy;
    public EnemyController enemyController;
    private float random;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyController=enemy.GetComponent<EnemyController>();
    }
    private void OnEnable()
    {
        random = UnityEngine.Random.Range(1, 3);
        Console.WriteLine(random.ToString());
    }
    // Update is called once per frame
    void Update()
    {
        Console.WriteLine(random.ToString());
    }
}
