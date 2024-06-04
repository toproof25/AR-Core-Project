using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SetMoveMap : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private NavMeshSurface navMeshSurface;

    [SerializeField]
    List<NavMeshAgent> agents = new List<NavMeshAgent>();

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


            // ����޽� ������ ������Ʈ
            navMeshSurface.BuildNavMesh();

            // ��� ���� NavMeshAgent�� ���ġ
            foreach (NavMeshAgent agent in agents)
            {
                agent.Warp(agent.transform.position); // ���� ��ġ���� ����޽� ��θ� �缳��
            }


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
