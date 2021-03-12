using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Referencia https://www.youtube.com/watch?v=zit45k6CUMk

public class Parallax : MonoBehaviour
{
    // Start is called before the first frame update

    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1-parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        if (temp > startpos + length/2) startpos += length;
        else if (temp < startpos - length/2) startpos -= length;
    }
}
