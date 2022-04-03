using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class FoxSpawner : MonoBehaviour
{
    public GameObject prettyBall;
    bool active = false;
    public float spawnTime = 0f;
    public float radius = 3f;
    bool timer = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (active)
        {
            if ((Time.time - spawnTime) > .2f)
            {
                Debug.Log(Time.time);

                //float x = Mathf.Cos(i) * radius;
                //float y = Mathf.Sin(i) * radius;
                Instantiate(prettyBall, new Vector3(0, -2, 0), Quaternion.identity);
                spawnTime = Time.time;
            }

        }

    }

    void OnCollisionEnter(Collision collision)
    {
        active = true;
    }
}
