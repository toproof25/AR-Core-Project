using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMoveMap : MonoBehaviour
{

    [SerializeField]
    private GameObject player; 
    
    public void setPositionMap()
    {
        // ���� ī�޶� ����
        Camera mainCamera = Camera.main;

        if (mainCamera != null && player != null)
        {
            // �÷��̾��� �۷ι� �������� ����
            Vector3 playerGlobalPosition = player.transform.position;

            // ���� �� ������Ʈ�� �۷ι� �������� ����
            Vector3 mapGlobalPosition = transform.position;

            // ���� ī�޶��� �۷ι� �������� ����
            Vector3 cameraGlobalPosition = mainCamera.transform.position;

            // �÷��̾ ���� ī�޶��� x, z �����ǰ� ���� y�� ī�޶󺸴� 6��ŭ ���� ��ġ�� �����ϱ� ���� ���� �̵��� ���
            float deltaX = cameraGlobalPosition.x - playerGlobalPosition.x;
            float deltaY = (cameraGlobalPosition.y - 10f) - playerGlobalPosition.y;
            float deltaZ = cameraGlobalPosition.z - playerGlobalPosition.z;

            // ���� ���ο� �۷ι� ������ ���
            Vector3 newMapPosition = new Vector3(
                mapGlobalPosition.x + deltaX,
                mapGlobalPosition.y + deltaY,
                mapGlobalPosition.z + deltaZ
            );

            // ���� ��ġ�� ���� ����
            transform.position = newMapPosition;
        }
        else
        {
            Debug.LogError("Main Camera or Player is not assigned.");
        }

    }



    void Start()
    {
       
    }

    
    void Update()
    {
        
    }
}
