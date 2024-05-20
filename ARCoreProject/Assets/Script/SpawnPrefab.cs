using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public Transform objectToPosition;
    public float followSpeed = 1f;

    void Start()
    {
        
    }

    
    void Update()
    {
        var delta = objectToPosition.position - transform.position;
        transform.position += delta * Time.deltaTime * followSpeed;
        
    }
}
