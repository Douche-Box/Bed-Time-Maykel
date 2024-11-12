using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PherbyTower : TowerBase
{
    public float waitTime;
    public bool canDamage;

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

            waitTime += Time.deltaTime;

            if (waitTime < fireRate)
            {
                canDamage = false;
            }
            if (waitTime > fireRate)
            {
                print("hai");
                canDamage = true;
                waitTime = 0;
            }
            if (canDamage)
            {
                DoDamageOverTime();
            }
        }
    }

    public void DoDamageOverTime()
    {
        for (int i = 0; i < enemiesInRange.Length; i++)
        {
            enemiesInRange[i].GetComponent<Enemy>().Damage(damage);
        }
        return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isntInBed)
        {
            if (other.CompareTag("enemy"))
            {
                other.GetComponent<Enemy>().Stun();
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(towerTransform.position, range);
    }
}
