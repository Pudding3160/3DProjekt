using UnityEngine;

public class Health : MonoBehaviour
{

    public float health;
    public float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
    }
}
