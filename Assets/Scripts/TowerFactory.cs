using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{

    [SerializeField] Tower towerPrefab;
    [SerializeField] int towerLimit = 5;
    [SerializeField] Transform towerParentTransform;
    //crete a queue
    Queue<Tower> towerQueue = new Queue<Tower>();

    public void AddTower(WayPoint baseWayPoint)
    {
        //todo chng to queue size
        //var towers = FindObjectsOfType<Tower>();



        int numTower = towerQueue.Count;
        if (numTower < towerLimit)
        {
           InstentiateNewTowers(baseWayPoint);
        }
        else
        {
            MoveExistingTowers(baseWayPoint);
        }
    }

    private void InstentiateNewTowers(WayPoint baseWayPoint)
    {
        var newTower = Instantiate(towerPrefab, baseWayPoint.transform.position, Quaternion.identity);
        //make towers instentiate in parent tower gameobject
        newTower.transform.parent = towerParentTransform.transform;
        //set the placeable flags
        baseWayPoint.isPlaceable = false;
        //set the baseWaypoints
        newTower.baseWayPoint = baseWayPoint;
        //set the placeable flags
        baseWayPoint.isPlaceable = false;
        //put new tower in queue
        towerQueue.Enqueue(newTower);
    }

    private void MoveExistingTowers(WayPoint newBaseWayPoint)
    {
        //take bottom tower of queue
        var oldTower = towerQueue.Dequeue();
        //set the placeable flags
        oldTower.baseWayPoint.isPlaceable = true; // free up the block
        newBaseWayPoint.isPlaceable = false;
        //set the baseWaypoints
        oldTower.baseWayPoint = newBaseWayPoint;
        oldTower.transform.position = newBaseWayPoint.transform.position;
        //put the old tower top of queue
        towerQueue.Enqueue(oldTower);
        //Debug.Log("Limit reached..");
    }
}
