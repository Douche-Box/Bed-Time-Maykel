using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotwheelsDamage : TowerBase
{
    public GameObject car;

    private void Start()
    {
        damage = towerData.damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            print(other.name);
            other.GetComponent<Enemy>().Damage(damage);
        }
        if (other.CompareTag("placeableGround"))
        {
            Destroy(car);
        }
    }
}
