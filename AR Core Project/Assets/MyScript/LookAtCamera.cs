using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    // UI�� ��� ī�޶� �Ĵٺ��� �ϴ� ��ũ��Ʈ
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.forward = Camera.main.transform.forward;
    }
}
