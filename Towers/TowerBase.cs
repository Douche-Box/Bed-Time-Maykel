using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public bool isntInBed;
    public TowerSO towerData;

    public float range;
    public int damage;
    public float fireRate;
    public float cost;
    public float size;
    public string placeTag;

    public LayerMask enemiesLayer;

    public Transform towerTransform;

    public Collider[] enemiesInRange;

    public void GetEnemies()
    {
        enemiesInRange = Physics.OverlapSphere(towerTransform.position, range, enemiesLayer);
    }

    public void isInBed(bool YesOrNo)
    {
        isntInBed = YesOrNo;
    }
}