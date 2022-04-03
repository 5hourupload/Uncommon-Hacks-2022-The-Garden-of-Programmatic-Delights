using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CubeSpawner : MonoBehaviour
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
        //Debug.Log(spawnTime);

        Debug.Log(spawnTime + " " + Time.time  + " " + ((Time.time - spawnTime)));

        if (active)
        {
            if ((Time.time - spawnTime) > .1f)
            {
                Debug.Log(Time.time);

                    //float x = Mathf.Cos(i) * radius;
                    //float y = Mathf.Sin(i) * radius;
                    Instantiate(prettyBall, new Vector3(0, -2, 0), Quaternion.identity);
                
                spawnTime = Time.time;
            }

        }


        //foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
        //{
        //    if (gameObj.name.Contains("ball"))
        //    {
        //        Vector3 p = gameObj.transform.position;
        //        gameObj.transform.position = new Vector3(p.x, p.y + .01f, p.z);
        //    }
        //}

    }

    void OnCollisionEnter(Collision collision)
    {
        active = true;
    }
}
