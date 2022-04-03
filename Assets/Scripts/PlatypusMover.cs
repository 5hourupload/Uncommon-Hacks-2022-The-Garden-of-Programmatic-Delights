using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatypusMover : MonoBehaviour
{
    Vector3 rotation;
    float move = .001f;
    float angle = 0f;
    float radius = 7f;
    float increment;
    // Start is called before the first frame update
    void Start()
    {
        angle = Random.Range(0f, 2f * Mathf.PI);
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        transform.rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        increment = Random.Range(0f, .25f);
        Vector3 p = transform.position;
        transform.position = new Vector3(Mathf.Cos(angle) * radius, p.y, Mathf.Sin(angle) * radius); ;


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = transform.position;
        radius += .1f;
        transform.position = new Vector3(Mathf.Cos(angle) * radius, p.y + increment, Mathf.Sin(angle) * radius); ;

    }
}
