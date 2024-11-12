using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainWaypoints : TowerBase
{
    public GameObject trainTower;

    public Transform[] wpoints;

    public Transform spawnpoint;

    public GameObject trainOBJ;

    void Start()
    {
        trainTower.GetComponent<TrainTower>().waypoints = wpoints;
        trainOBJ = Instantiate(trainTower, spawnpoint);

        isntInBed = true;
    }

    private void Update()
    {
        if (isntInBed == false)
        {
            trainOBJ.GetComponent<TrainTower>().isntInBed = false;
        }
        else
        {
            trainOBJ.GetComponent<TrainTower>().isntInBed = true;
        }
    }

}
