using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerStats", menuName = "TowerScriptableObjects", order = 1)]
public class TowerSO : ScriptableObject
{
    public float range;
    public int damage;
    public float fireSpeed;
    public float cost;
    public float size;
    public float health;
    public string placeTag;
    public GameObject towerBluePrint;
}