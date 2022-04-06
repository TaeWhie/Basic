using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    float scale;
    float heightScale;

    int planeSize;
    public GameObject cube;

    void Start()
    {
        scale = 0.2f;
        heightScale = 2f;

        planeSize = 25;

        for (int x = 0; x < planeSize; x++)
        {
            for (int z = 0; z < planeSize; z++)
            {
                var c = Instantiate(cube, new Vector3(x, 0, z), Quaternion.identity);
                c.transform.parent = transform;
            }
        }
    }

    void Update()
    {
        foreach (Transform child in transform)
        {
            child.transform.position = new Vector3(child.transform.position.x,
            heightScale * Mathf.PerlinNoise(Time.time + (child.transform.position.x * scale),
            Time.time + (child.transform.position.z * scale)),
            child.transform.position.z);
        }
    }
}