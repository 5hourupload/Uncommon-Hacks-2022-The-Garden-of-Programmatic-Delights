using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpinner : MonoBehaviour
{

    float angle = 0f;
    float radius = 7f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle -= .01f;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        Vector3 p = transform.position;
        transform.position = new Vector3(x, p.y + .02f, y);



    }
}
