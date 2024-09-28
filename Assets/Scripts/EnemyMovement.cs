using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float gamePeriod = 0.5f;
    [SerializeField] ParticleSystem goalParticle;

    void Start()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<WayPoint> path)
    {
        //print("Starting patrol..."); don;t need anymore
        foreach (WayPoint wayPoint in path)
        {
            transform.position = wayPoint.transform.position;

            yield return new WaitForSeconds(gamePeriod);
        }
        SelfDestruct();
        //print("Ending patrol"); don;t need anymore
    }

    private void SelfDestruct()
    {
        //important to instentiate before destroying this object
        var vfx = Instantiate(goalParticle, transform.position, Quaternion.identity);
        vfx.Play();

        //destroy particle after delay
        Destroy(vfx.gameObject, vfx.main.duration);
        //destroy enemy
        Destroy(gameObject);
    }
}
