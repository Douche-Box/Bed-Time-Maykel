using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTrap : TowerBase
{
    public Transform trainTransform;
    public Transform[] waypoints;
    public int waypointIndex;
    public Transform trainRotation;
    public float turnSpeed;

    private void Start()
    {
        damage = towerData.damage;
        fireRate = towerData.fireSpeed;
    }

    private void Update()
    {
        if (isntInBed)
        {
            trainMovement();
        }
    }

    public void trainMovement()
    {
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }

        if (Vector3.Distance(trainTransform.position, waypoints[waypointIndex].position) < 0.1f)
        {
            waypointIndex++;
            if (waypointIndex == 15)
            {
                waypointIndex = 0;
            }
            trainRotation.rotation = Quaternion.LookRotation(waypoints[waypointIndex].transform.position - trainTransform.position);
        }

        trainTransform.position = Vector3.MoveTowards(trainTransform.position, waypoints[waypointIndex].position, fireRate * Time.deltaTime);
        trainTransform.rotation = Quaternion.Lerp(transform.rotation, trainRotation.rotation, Time.deltaTime * turnSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isntInBed)
        {
            if (other.CompareTag("enemy"))
            {
                other.GetComponent<Enemy>().Damage(damage);
            }
        }
    }
}
