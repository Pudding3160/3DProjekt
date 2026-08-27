using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform firePoint;
    public ParticleSystem particle;



    public void Shooting()
    {
        RaycastHit hit;

        Debug.Log("pew");

        if (Physics.Raycast(
            firePoint.position,
            firePoint.forward,
            out hit,
            100f))
        {
            Debug.DrawRay(
                firePoint.position,
                firePoint.forward * hit.distance,
                Color.yellow
            );

            EnemySight enemy = hit.transform.GetComponent<EnemySight>();

            if (enemy != null)
            {
                enemy.playerhit();
            }

            ParticleSystem firingParticle = Instantiate(
                particle,
                firePoint.position,
                firePoint.rotation
            );

            Destroy(firingParticle, 1f);
        }
    }
}
