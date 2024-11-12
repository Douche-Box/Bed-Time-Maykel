using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneyDamage : TowerBase
{
    public GameObject phoney;

    public bool canDamage;


    public Transform[] waypoints;
    public int waypointIndex;
    public Quaternion phoneRotationQ;
    public float speed;
    public float turnSpeed;
    private void Start()
    {
        canDamage = true;

        damage = towerData.damage;
        range = towerData.range;
        fireRate = towerData.fireSpeed;

        speed = fireRate;

    }

    private void Update()
    {
        PhoneMovement();
    }

    public void PhoneMovement()
    {
        if (waypointIndex == 0)
        {
            phoneRotationQ = Quaternion.LookRotation(waypoints[waypointIndex].transform.position - transform.position);
        }

        if (waypointIndex == waypoints.Length - 1)
        {
            speed = 0;
            DamageExplosion();
        }

        if (Vector3.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f)
        {
            waypointIndex++;
            phoneRotationQ = Quaternion.LookRotation(waypoints[waypointIndex].transform.position - transform.position);
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, phoneRotationQ, Time.deltaTime * turnSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            GetEnemies();

            DamageExplosion();
        }
    }

    public void DamageExplosion()
    {
        GetEnemies();

        if (canDamage)
        {
            speed = 0;

            for (int i = 0; i < enemiesInRange.Length; i++)
            {
                enemiesInRange[i].GetComponent<Enemy>().Damage(damage);
            }

            Destroy(gameObject);
        }
    }
}
