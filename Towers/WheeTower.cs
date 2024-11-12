using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheeTower : TowerBase
{
    public Transform target;
    public Transform towerRotation;
    public Transform towerpivot;

    public float rotateSpeed;

    public bool hasAttackedOnce;

    public float waittime;
    void Start()
    {
        range = towerData.range;
        damage = towerData.damage;
        fireRate = towerData.fireSpeed;
        cost = towerData.cost;
        size = towerData.size;

        isntInBed = true;
    }

    void Update()
    {
        if (isntInBed)
        {
            GetEnemies();

            GetTarget();

            Attack();
        }
    }

    public void Attack()
    {
        if (target != null)
        {
            Vector3 dir = target.position - towerpivot.position;
            Quaternion lookrotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(towerRotation.rotation, lookrotation, Time.deltaTime * rotateSpeed).eulerAngles;
            towerRotation.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            hasAttackedOnce = true;

            waittime += Time.deltaTime;

            if (waittime > fireRate)
            {
                target.GetComponent<Enemy>().Damage(damage);

                waittime = 0;

                hasAttackedOnce = false;
                GetTarget();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(towerTransform.position, range);
    }

    public void GetTarget()
    {
        for (int i = 0; i < enemiesInRange.Length; i++)
        {

            if (i == 0)
            {
                target = enemiesInRange[i].GetComponent<Enemy>().enemyTranform;
            }
            else
            {
                if (enemiesInRange[i].GetComponent<Enemy>().enemyHealth < enemiesInRange[i - 1].GetComponent<Enemy>().enemyHealth)
                {
                    target = enemiesInRange[i].GetComponent<Enemy>().enemyTranform;
                }
            }
        }
    }
}