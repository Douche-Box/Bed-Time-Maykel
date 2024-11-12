using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phoney : MonoBehaviour
{
    public GameObject inputs;

    public GameObject phoneyOBJ;
    public GameObject phoneyClone;

    public Transform spawnPoint;

    public Transform[] wPoints;

    public void Spawner(bool canSpawn)
    {
        if (canSpawn)
        {
            if (phoneyClone == null)
            {
                phoneyClone = Instantiate(phoneyOBJ, this.transform);
                phoneyClone.GetComponent<PhoneyDamage>().waypoints = wPoints;
            }
            canSpawn = false;
        }
    }
}
