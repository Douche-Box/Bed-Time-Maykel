using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotwheelsTower : TowerBase
{
    public GameObject carOBJ;
    public GameObject carClone;

    public Transform spawnPoint;

    public int spawnamount;

    public bool animationBool;

    public Animator coldwheelstower;

    public float waitforamountofseconds;

    public float timer;
    public bool candoTimer;
    private void Update()
    {
        coldwheelstower.SetBool("Uit", animationBool);

        if (spawnamount > 0)
        {
            animationBool = true;

            if (candoTimer)
            {
                timer += Time.deltaTime;
            }

            if (timer >= waitforamountofseconds)
            {
                candoTimer = false;
                timer = waitforamountofseconds;
                if (carClone == null)
                {
                    carClone = Instantiate(carOBJ, this.transform);

                    spawnamount--;
                }
            }
        }
        if (spawnamount == 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                animationBool = false;
                timer = 0;
            }
        }
    }
}
