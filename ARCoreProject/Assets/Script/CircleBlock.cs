using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBlock : MonoBehaviour
{
    public GameObject blockPrefab;
    public int numberOObjects = 20;
    public float radius = 30;

    void Start()
    {
        for(int y = 0; y < numberOObjects; y++)
        {
            for (int i=0; i < numberOObjects; i++)
            {
                float angle = i * Mathf.PI * 2 / numberOObjects;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;

                Vector3 pos = transform.position + new Vector3(x, y, z);
                float angleDegrees = angle * Mathf.Rad2Deg;
                Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
                Instantiate(blockPrefab, pos, rot);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
