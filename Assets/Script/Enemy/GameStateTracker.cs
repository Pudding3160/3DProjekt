using System;
using System.Collections.Generic;
using UnityEngine;


public class GameStateTracker : MonoBehaviour
{
    public GameObject enemy;
    public EnemyController enemyCtrl;
    public GameObject enemy2;
    public EnemyController enemyCtrl2;
    private int random;
    private List<int> nums = new List<int> { 1, 2, 3};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        enemyCtrl = enemy.GetComponent<EnemyController>();
        enemyCtrl2 = enemy2.GetComponent<EnemyController>();
    }
    private void OnEnable()
    {
        enemy2.gameObject.SetActive(false);
        Debug.Log("It can move");
        AddAbility();
        
    }

    private void AddAbility()
    {
        random = UnityEngine.Random.Range(0, nums.Count);
        switch (nums[random])
        {
            case 1:
                enemyCtrl.canSee = true;
                enemyCtrl2.canSee = true;
                nums.Remove(1);
                Debug.Log("It can see");
                break;
            case 2:
                enemyCtrl.canHear = true;
                enemyCtrl2.canHear = true;
                nums.Remove(2);
                Debug.Log("It can hear");
                break;
            case 3:
                enemy2.gameObject.SetActive(true);
                Debug.Log("It is two");
                nums.Remove(3);
                break;
            default:
                Debug.Log("No abilities left");
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
