using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlatypusSpawner : MonoBehaviour
{
    public GameObject platypus;
    bool active = false;
    public float spawnTime = 0f;

    bool timer = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(spawnTime);

        Debug.Log(spawnTime + " " + Time.time + " " + ((Time.time - spawnTime)));

        if (active)
        {
            if ((Time.time - spawnTime) > .05f)
            {
                Debug.Log(Time.time);

                Instantiate(platypus, new Vector3(0, -2, 0), Quaternion.identity);

                spawnTime = Time.time;
            }

        }

    }

    void OnCollisionEnter(Collision collision)
    {
        active = true;
    }
}
