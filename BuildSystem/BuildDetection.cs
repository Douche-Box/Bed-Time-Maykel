using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildDetection : MonoBehaviour
{
    public GameObject buildingSystem;
    public GameObject destroyableObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("tower"))
        {
            buildingSystem.GetComponent<BuildingSystem>().canDestroy = true;
            buildingSystem.GetComponent<BuildingSystem>().canPlace = false;

            destroyableObject = other.gameObject;
        }
        if (other.CompareTag("player"))
        {
            buildingSystem.GetComponent<BuildingSystem>().canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("tower"))
        {
            buildingSystem.GetComponent<BuildingSystem>().canDestroy = false;
            buildingSystem.GetComponent<BuildingSystem>().canPlace = true;
        }
        if (other.CompareTag("player"))
        {
            buildingSystem.GetComponent<BuildingSystem>().canPlace = true;
        }
    }
}
