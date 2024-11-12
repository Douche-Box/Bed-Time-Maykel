using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    public GameObject player;
    public GameObject tower;
    public TowerSO towerData;
    public GameObject arrowPlacer;
    public Renderer arrowRenderer;
    public GameObject cam;

    public GameObject placeBluePrint;
    public Renderer bluePrintRenderer;

    public GameObject moneyToGiveTo;

    public RaycastHit playerHit;
    public RaycastHit arrowHit;
    public RaycastHit[] sphereHit;
    public RaycastHit[] pathdetecthit;
    public float placeRange;

    public float posX;
    public float posY;
    public float posZ;

    public float distanceToArrow;

    public bool canPlace;
    public bool canDestroy;
    public bool hasTower;
    public bool inWall;
    public bool inTower;

    public Vector3 raycastOrigin;

    public GameObject objectToDestroy;

    public string placeTag;

    public float towerSize;

    public float waitTimeForDelete;
    public float timeToDestroy;

    public float towerReGetTime;
    public float towerregret = 1f;

    public bool arrow;
    public LayerMask towerMask;

    public bool nonplace;

    public bool towerAbleToPlace;

    public GameObject bed;

    public int totalTowerIndex;

    void Update()
    {
        if (tower != null)
        {
            towerAbleToPlace = true;

            hasTower = true;

            towerData = tower.GetComponent<TowerBase>().towerData;

            if (placeBluePrint == null)
            {
                placeBluePrint = tower.GetComponent<TowerBase>().towerData.towerBluePrint;
                bluePrintRenderer = placeBluePrint.GetComponent<MeshRenderer>();

                placeBluePrint = Instantiate(towerData.towerBluePrint, arrowHit.point, arrowHit.transform.rotation);
            }
            else
            {
                arrowRenderer.enabled = true;
            }
        }

        if (hasTower && towerAbleToPlace)
        {
            GetTowerInfo();
        }

        arrowPlacer.transform.position = new Vector3(posX, posY, posZ);
        if (placeBluePrint != null)
        {
            placeBluePrint.transform.position = arrowPlacer.transform.position;
            placeBluePrint.transform.rotation = arrowPlacer.transform.rotation;

            arrowRenderer.enabled = false;
        }
        else
        {
            arrowRenderer.enabled = true;
        }

        distanceToArrow = Vector3.Distance(player.transform.position, arrowPlacer.transform.position);

        if (distanceToArrow < placeRange)
        {
            arrow = true;
            arrowPlacer.SetActive(true);
        }
        else
        {
            arrow = false;
            arrowPlacer.SetActive(false);
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out playerHit, placeRange, towerMask))
        {
            posX = playerHit.point.x;
            posZ = playerHit.point.z;

            if (playerHit.transform.CompareTag("tower"))
            {
                posX = playerHit.transform.position.x;
                posZ = playerHit.transform.position.z;
            }
        }

        if (arrow)
        {
            if (Physics.Raycast(raycastOrigin, -arrowPlacer.transform.up, out arrowHit, 3f, towerMask))
            {
                if (arrowHit.transform.CompareTag("tower"))
                {
                    inTower = true;
                    canDestroy = true;
                }
                else
                {
                    inTower = false;
                    canDestroy = false;
                }
            }

            Debug.DrawRay(arrowPlacer.transform.position, -arrowPlacer.transform.up, Color.red);

            raycastOrigin.x = arrowPlacer.transform.position.x;
            raycastOrigin.y = arrowPlacer.transform.position.y;
            raycastOrigin.z = arrowPlacer.transform.position.z;

            raycastOrigin.y += 1f;

            sphereHit = Physics.SphereCastAll(arrowHit.point, towerSize, -arrowPlacer.transform.up, towerMask);

            for (int i = 0; i < sphereHit.Length; i++)
            {
                if (sphereHit[i].collider.CompareTag(placeTag))
                {
                    canPlace = true;
                }
                if (sphereHit[i].collider.CompareTag("wall"))
                {
                    inWall = true;
                }
                if (sphereHit[i].collider.CompareTag("player"))
                {
                    inWall = true;
                }
                if (sphereHit[i].collider.CompareTag("tower"))
                {
                    inTower = true;
                    if (placeBluePrint != null)
                    {
                        bluePrintRenderer.sharedMaterial.SetFloat("_Placefloat", 0);
                    }

                }
                if (!sphereHit[i].collider.CompareTag("wall"))
                {
                    inWall = false;
                }

                if (inWall == false && inTower == false)
                {
                    if (arrowHit.transform != null)
                    {
                        if (arrowHit.transform.CompareTag(placeTag))
                        {
                            if (inWall == false)
                            {
                                canPlace = true;
                                if (placeBluePrint != null)
                                {
                                    bluePrintRenderer.sharedMaterial.SetFloat("_Placefloat", 1);
                                }
                            }
                        }
                        else
                        {
                            canPlace = false;
                            if (placeBluePrint != null)
                            {
                                bluePrintRenderer.sharedMaterial.SetFloat("_Placefloat", 0);
                            }
                        }
                    }

                }
            }

            if (inWall)
            {
                arrowRenderer.material.color = Color.black;
                if (placeBluePrint != null)
                {
                    bluePrintRenderer.sharedMaterial.SetFloat("_Placefloat", 0);
                }
            }

            if (canPlace && hasTower && inTower == false && inWall == false && nonplace == false && towerAbleToPlace)
            {
                if (player.GetComponent<PlayerInputs>().placeInput)
                {
                    bed.GetComponent<Bed>().towers.Add(Instantiate<GameObject>(tower, arrowHit.point, arrowPlacer.transform.rotation));
                    totalTowerIndex++;

                    Destroy(placeBluePrint);
                    tower = null;
                    hasTower = false;
                    towerAbleToPlace = false;
                }
            }

            if (canDestroy && inTower)
            {
                if (player.GetComponent<PlayerInputs>().interactInput)
                {
                    waitTimeForDelete += Time.deltaTime;

                    arrowRenderer.sharedMaterial.SetFloat("_Placefloat", 0);

                    if (waitTimeForDelete > timeToDestroy)
                    {
                        waitTimeForDelete = 0;

                        bed.GetComponent<Bed>().towers.Remove(arrowHit.transform.gameObject);
                        totalTowerIndex--;

                        float moneytogiveback = arrowHit.transform.GetComponent<TowerBase>().towerData.cost / 2;

                        moneyToGiveTo.GetComponent<Shop>().currency += moneytogiveback;

                        objectToDestroy = arrowHit.transform.gameObject;
                        Destroy(objectToDestroy);

                        hasTower = true;
                    }
                }
                else
                {
                    arrowRenderer.sharedMaterial.SetFloat("_Placefloat", 1);
                    waitTimeForDelete = 0;
                }
            }

            if (player.GetComponent<PlayerInputs>().rotateInputR)
            {
                transform.Rotate(0f, arrowPlacer.transform.position.y + 50 * Time.deltaTime, 0f, Space.Self);
            }

            if (player.GetComponent<PlayerInputs>().rotateInputL)
            {
                transform.Rotate(0f, arrowPlacer.transform.position.y - 100 * Time.deltaTime, 0f, Space.Self);
            }
        }
    }

    /// <summary>
    /// Get tower info size and tag
    /// </summary>
    public void GetTowerInfo()
    {
        if (tower != null)
        {
            towerSize = tower.GetComponent<TowerBase>().towerData.size;
            placeTag = tower.GetComponent<TowerBase>().towerData.placeTag;
        }
    }
}