using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxMovement : MonoBehaviour
{

    float angle = 0f;
    float radius = 9f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        radius = 9f + Mathf.Cos(angle/10) * 10;
        angle += .03f;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        Vector3 p = transform.position;
        transform.position = new Vector3(x, p.y + .02f, y);



    }
}
