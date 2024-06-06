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

    // ��Ż�� �÷��鿡 ������ �� ���� ���� ��ġ�� ������
    // ���� �溸�� ũ�� ������ ���� �����Ͽ� �÷��̾� ĳ���͸� ��� �� �� �ְ� �ϱ� ����

    // ����ī�޶�, �÷��̾�� �̵����� �����鼭 ī�޶� �Ʒ������� �÷��̾ ��ġ�ϵ��� ���� �����Ǹ� �̵��ϰԲ� �����ؾ���
    public void setPositionMap()
    {
        // ���� ī�޶�
        Camera mainCamera = Camera.main;


        if (mainCamera != null && player != null)
        {
            // �÷��̾� �۷ι� ������
            Vector3 playerPosition = player.transform.position;

            // ���� �� ������Ʈ �۷ι� ������
            Vector3 mapPosition = transform.position;

            // ���� ī�޶�۷ι� ������
            Vector3 cameraPosition = mainCamera.transform.position;

            // �÷��̾ ���� ī�޶� ��ġ ���
            float x = cameraPosition.x - playerPosition.x;
            float y = (cameraPosition.y - 10f) - playerPosition.y;
            float z = cameraPosition.z - playerPosition.z;

            // �� ������ ���
            Vector3 newPosition = new Vector3(
                mapPosition.x + x,
                mapPosition.y + y,
                mapPosition.z + z
            );

            // ���� ��ġ�� ���� ����
            transform.position = newPosition;


            // ����޽� ������Ʈ (�̵��� ��ġ ����)
            navMeshSurface.BuildNavMesh();

            // ��� ���� NavMeshAgent�� �ٽ� ����(���ϸ� ���� �̵��� ���� Nav�� �����Ǿ� �̵���)
            foreach (NavMeshAgent agent in agents)
            {
                agent.Warp(agent.transform.position);
            }
        }


    }
}
