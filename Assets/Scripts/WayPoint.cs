using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    Vector2Int gridPos;
    public bool isExplored = false;
    public WayPoint exploredForm;
    public bool isPlaceable = true;
    const int gridSize = 10;

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
             Mathf.RoundToInt(transform.position.x / gridSize),
             Mathf.RoundToInt(transform.position.z / gridSize));


    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) //left click
        {
            if (isPlaceable)
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
            }
            else
            {
                print("can't place tower");
            }

        }

    }
    //public void SetTopColor(Color color) //don't need anymore
    //{
    //    MeshRenderer topMesh = transform.Find("Top").GetComponent<MeshRenderer>();
    //    topMesh.material.color = color;

    //}

}
