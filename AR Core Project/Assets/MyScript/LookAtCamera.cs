using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    // UI가 계속 카메라를 쳐다보게 하는 스크립트
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.forward = Camera.main.transform.forward;
    }
}
