using UnityEngine;

public class Health : MonoBehaviour
{

    public float health;
    public float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 3f;
    }
    void takeDamage()
    {
        health -= health;
    }
    void die()
    {
        GameObject.Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(health);
        timer += Time.deltaTime;
        if (timer >= 30f && health < 3f)
        {
            health += 1;
            timer = 0f;
        }
        if (health <= 0)
            die();
    }
}
