using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSystem : MonoBehaviour
{
    public float exposed;
    public float maxexposed;
    public bool candotimer;

    public GameObject bed;

    public Transform player;
    public Transform ouder;

    public RaycastHit hit;

    public int spawnint;

    public Transform[] randomtransforms;

    public Animator kock;

    public float waitforkok;

    public int minimumwaittime;
    public int maximumwaittime;

    public bool walkIn;
    public bool cought;

    public float waitTime;

    public GameObject seenUI;

    private void Start()
    {
        RandomTime();
    }

    private void Update()
    {
        kock.SetBool("walkIn", walkIn);
        kock.SetBool("cought", cought);

        Debug.DrawRay(ouder.position, player.position - ouder.position);

        Walking();

        if (Physics.Raycast(ouder.position, player.position - ouder.position, out hit, Mathf.Infinity) && hit.transform.CompareTag("player"))
        {
            candotimer = true;
            seenUI.SetActive(true);
        }
        else
        {
            candotimer = false;
            seenUI.SetActive(false);
        }

        if (candotimer)
        {
            exposed += Time.deltaTime;
        }

        if (exposed >= maxexposed)
        {
            cought = true;
            bed.GetComponent<Bed>().inBed = true;
        }
    }

    public void Walking()
    {
        waitTime += Time.deltaTime;

        if (waitTime > waitforkok)
        {
            walkIn = true;
            waitTime = 0;

            RandomTime();
        }
    }

    public void RandomTime()
    {
        waitforkok = Random.Range(minimumwaittime, maximumwaittime);
    }
}
