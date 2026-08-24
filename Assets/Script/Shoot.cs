using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform firePoint;


    // Update is called once per frame
    void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        RaycastHit hit;

        if(Physics.Raycast(firePoint.position,transform.TransformDirection(Vector3.forward),out hit, 100)){
            Debug.DrawRay(firePoint.position,transform.TransformDirection(Vector3.forward)*hit.distance,Color.yellow);

            EnemySight enemy= hit.transform.GetComponent<EnemySight>();
            if (enemy != null) {
                enemy.playerhit();
            }
        }
        
    }
}
