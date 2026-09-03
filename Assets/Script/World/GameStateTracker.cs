using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class GameStateTracker : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject enemy;
    public EnemyController enemyCtrl;
    public GameObject enemy2;
    public EnemyController enemyCtrl2;
    public GameObject moveText;
    public GameObject seeText;
    public GameObject hearText;
    public GameObject twoText;
    public GameObject webText;
    public GameObject LevelTrigger;
    public EndLevelTrigger trigger;
    public GameObject cobwebs;

    

    public bool hasSight = false;
    public bool hasEars = false;
    public bool isTwo = false;
    public bool isWeb=false;    


    public bool isProgressing=false;
    

    public static GameStateTracker instance;


    private int random;
    private List<int> nums = new List<int> { 1, 2, 3,4};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void ResetProgress()
    {
        Destroy(gameObject);
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
       
        
    }
    private void OnEnable()
    {
        
        Debug.Log("It can move");
       // moveText.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        if (scene.name.Contains("Level")) { 
        GameObject ui = GameObject.Find("ui");
        /*RULES TEXT */
        seeText = ui.transform.Find("Pause/Rules/See").gameObject;
        hearText = ui.transform.Find("Pause/Rules/Hear").gameObject;
        twoText = ui.transform.Find("Pause/Rules/Two").gameObject;
        moveText = ui.transform.Find("Pause/Rules/Move").gameObject;
        webText = ui.transform.Find("Pause/Rules/Web").gameObject;
        

        /*RULES TEXT */
        cobwebs = GameObject.Find("CobWebs");
        cobwebs.SetActive(false);
        LevelTrigger = GameObject.Find("LevelEndTrigger");
            trigger = LevelTrigger.GetComponent<EndLevelTrigger>();
            trigger.LoadTrackerAfterDontDestroyOnLoad();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemy = enemies[0];
        enemy2 = enemies[1];

        enemyCtrl = enemy.GetComponentInChildren<EnemyController>();
        enemyCtrl2 = enemy2.GetComponentInChildren<EnemyController>();

        enemy2.SetActive(false);
        Debug.Log("Map loaded");
        LoadAbilities();
            
            isProgressing = true;
        }
    }

    private void LoadAbilities()
    {
        if (isTwo)
        {
            enemy2.gameObject.SetActive(true);
            twoText.SetActive(true);
            if(hasSight)
                enemyCtrl2.canSee=true;
            if(hasEars)
                enemyCtrl2.canHear=true;

        }
        if (hasSight) { 
            enemyCtrl.canSee = true;
            seeText.SetActive(true);
            
            
        }
        if (hasEars) { 
            enemyCtrl.canHear = true;
            hearText.SetActive(true);

            Debug.Log("It can hear");
        }
        if (isWeb) {

            cobwebs.SetActive(true);
            webText.SetActive(true);
            Debug.Log("It can web");
        }

        
    }

    public void AddAbility()
    {
        random = UnityEngine.Random.Range(0, nums.Count);
        if (nums.Count > 0) { 
        switch (nums[random])
        {
            case 1:
                enemyCtrl.canSee = true;
                if(enemy2.gameObject.activeInHierarchy)
                    enemyCtrl2.canSee = true;
                nums.Remove(1);
                Debug.Log("It can see");
               seeText.SetActive(true);
               hasSight=true;
                break;
            case 2:
                enemyCtrl.canHear = true;
                if (enemy2.gameObject.activeInHierarchy)
                    enemyCtrl2.canHear = true;

                nums.Remove(2);
                Debug.Log("It can hear");
               hearText.SetActive(true);
               hasEars=true;
                break;
            case 3:
                enemy2.gameObject.SetActive(true);
                Debug.Log("It is two");
                nums.Remove(3);
                twoText.SetActive(true);
               isTwo=true;
                break;

            case 4:
                webText.SetActive(true);
                Debug.Log("It can web");
                cobwebs.SetActive(true);
                isWeb=true;
                break;
            default:
                Debug.Log("No abilities left");
                break;
        }
        }
        else 
        Debug.Log("No abilities left");
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
   
}
