using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuttyTrap : TowerBase
{
    public float health;

    public RaycastHit[] enemyDetection;

    public GameObject enemyToAttack;

    public float waittime;

    public Collider[] trapTarget;

    private void Start()
    {
        #region
        range = towerData.range;
        damage = towerData.damage;
        fireRate = towerData.fireSpeed;
        cost = towerData.cost;
        size = towerData.size;
        placeTag = towerData.placeTag;
        health = towerData.health;
        #endregion
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            other.GetComponent<Enemy>().SlowDownEnemy(damage);
            health--;
        }
    }
}