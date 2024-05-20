using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    public GameObject block;
    public int width = 10;
    public int hight = 5;
    public string myName = "wall";

    void Start()
    {
        GameObject wall = new GameObject("wall");
        for (int y=0; y < hight; y++)
        {
            for (int x = 0; x < width; x++)
                Instantiate(block, new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    void Update()
    {
        
    }
}
