using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinColor : MonoBehaviour
{
    public int size;//비출 큐브 뭉치의 사이즈이다.
    public GameObject cube;
    public float scale;//이 값을 이용해 값을 완만하게 만들어, 결과적으로 더 큰 뭉치의 청크를 만들수 있다.
    public float m;//높이 값을 조정하는 수
    bool move;
    float height;

    void Start()
    {
        move = true;

        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
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
            height = Mathf.PerlinNoise(child.transform.position.x / scale, child.transform.position.z / scale);

            child.GetComponent<MeshRenderer>().material.color = new Color(height, height, height, height);
        }

        if (move)
        {
            foreach (Transform child in transform)
            {
                height = Mathf.PerlinNoise(child.transform.position.x / scale, child.transform.position.z / scale);
                child.transform.position = new Vector3(child.transform.position.x, Mathf.RoundToInt(height * m), child.transform.position.z);

                // Mathf.RoundToInt() : 매개변수로 받은 두 정수중 가까운 정수를 반환합니다.
            }
        }
    }
}
