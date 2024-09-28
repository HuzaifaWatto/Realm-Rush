using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //all r instance
    //parameters of each tower
    [SerializeField] Transform objectToPan;
    [SerializeField] float attackRange = 30f;
    [SerializeField] ParticleSystem projectileParticle;

    //what the tower is standing on
    public WayPoint baseWayPoint;

    //state of each tower
    Transform targetEnemy;

    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
        }
    }

    private void SetTargetEnemy() 
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0) { return; }
        Transform closestEnemy = sceneEnemies[0].transform;
        // Rick method
        //foreach (EnemyDamage testEnemy in sceneEnemies)
        //{
        //    closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        //}

        // Asad bhi method
        float distToClosest = Vector3.Distance(transform.position, closestEnemy.position);

        for (int i = 1; i < sceneEnemies.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, sceneEnemies[i].transform.position);
            if (dist <= distToClosest)
            {
                closestEnemy = sceneEnemies[i].transform;
                distToClosest = dist;
            }
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(transform.position, transformA.position);
        var distToB = Vector3.Distance(transform.position, transformB.position);
        if (distToA < distToB)
        {
            return transformA;
        }
        return transformB;
    }

    void FireAtEnemy()
    {
        float currentDistance =
            Vector3.Distance(targetEnemy.transform.position,
            gameObject.transform.position);
        if (currentDistance <= attackRange)
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }
}
