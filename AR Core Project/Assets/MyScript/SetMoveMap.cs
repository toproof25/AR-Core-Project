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

    // 포탈이 플랜면에 생성될 때 마다 맵의 위치를 변경함
    // 맵이 방보다 크기 때문에 맵을 변경하여 플레이어 캐릭터를 계속 볼 수 있게 하기 위함

    // 메인카메라, 플레이어는 이동하지 않으면서 카메라 아래쪽으로 플레이어가 위치하도록 맵의 포지션만 이동하게끔 구현해야함
    public void setPositionMap()
    {
        // 메인 카메라
        Camera mainCamera = Camera.main;


        if (mainCamera != null && player != null)
        {
            // 플레이어 글로벌 포지션
            Vector3 playerPosition = player.transform.position;

            // 현재 맵 오브젝트 글로벌 포지션
            Vector3 mapPosition = transform.position;

            // 메인 카메라글로벌 포지션
            Vector3 cameraPosition = mainCamera.transform.position;

            // 플레이어가 메인 카메라 위치 계산
            float x = cameraPosition.x - playerPosition.x;
            float y = (cameraPosition.y - 10f) - playerPosition.y;
            float z = cameraPosition.z - playerPosition.z;

            // 맵 포지션 계산
            Vector3 newPosition = new Vector3(
                mapPosition.x + x,
                mapPosition.y + y,
                mapPosition.z + z
            );

            // 맵의 위치를 새로 설정
            transform.position = newPosition;


            // 내비메쉬 업데이트 (이동한 위치 기준)
            navMeshSurface.BuildNavMesh();

            // 모든 적의 NavMeshAgent를 다시 설정(안하면 적의 이동이 이전 Nav로 고정되어 이동함)
            foreach (NavMeshAgent agent in agents)
            {
                agent.Warp(agent.transform.position);
            }
        }


    }
}
