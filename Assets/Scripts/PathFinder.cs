using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] WayPoint startWaypoint, endWaypoint;

    Dictionary<Vector2Int, WayPoint> grid = new Dictionary<Vector2Int, WayPoint>();
    Queue<WayPoint> queue = new Queue<WayPoint>();
    List<WayPoint> path = new List<WayPoint>(); 
    bool isRunning = true;
    WayPoint searchCenter; // current searchCenter

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<WayPoint> GetPath()
    {
        if (path.Count == 0)
        {
            CalculatePath();
        }
        return path;
    }

    private void CalculatePath()
    {
        LoadBlocks();
        //ColorStartAndEnd(); //don't need anymore
        BreadthFirstSearch();
        CreatePath();
    }

    private void CreatePath()
    {
        SetAsPath(endWaypoint);
      

        WayPoint previous = endWaypoint.exploredForm;
        while (previous != startWaypoint)
        {
            //add intermedidate waypoints
            SetAsPath(previous);
            //path.Add(previous);
            //previous.isPlaceable = false;
            previous = previous.exploredForm;
        }
        SetAsPath(startWaypoint);
        //path.Add(startWaypoint);
        //startWaypoint.isPlaceable = false;
        path.Reverse();
        //add start waypoints
        //reverse the list
    }

    private void SetAsPath(WayPoint wayPoint)
    {
        path.Add(wayPoint);
        wayPoint.isPlaceable = false;
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);
        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            HaltIfEndPoint();
            ExploreNieghbors();
            searchCenter.isExplored = true;
        }
   
    }

    private void HaltIfEndPoint()
    {
        if(searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNieghbors()
    {
        if (!isRunning) { return; }
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
            if (grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
            //try //don't need anymore
            //{
            //    QueueNewNeighbours(neighbourCoordinates); 
            //catch
            //{
            //    //sitBack nd rest
            //}
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        WayPoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            //sitBack nd rest
        }
        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredForm = searchCenter;
            //print("Exploring " + explorationCoordinates); //don't need anymore
        }
    }

    //private void ColorStartAndEnd() //don't need anymore
    //{
    //    startWaypoint.SetTopColor(Color.green); 
    //    endWaypoint.SetTopColor(Color.red);

    //}

    private void LoadBlocks()
    {
        var wayPoints = FindObjectsOfType<WayPoint>();
        foreach (WayPoint wayPoint in wayPoints)
        {
            var gridPos = wayPoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Overlapping block " + wayPoint);
            }
            else
            {
                 grid.Add(gridPos, wayPoint);
                //wayPoint.SetTopColor(Color.black); //don't need anymore
            }

        }
        //print("Loaded " + grid.Count + " blocks"); //don't need anymore
    }

}
