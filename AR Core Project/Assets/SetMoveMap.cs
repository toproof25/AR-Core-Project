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
        // 메인 카메라 참조
        Camera mainCamera = Camera.main;

        if (mainCamera != null && player != null)
        {
            // 플레이어의 글로벌 포지션을 구함
            Vector3 playerGlobalPosition = player.transform.position;

            // 현재 맵 오브젝트의 글로벌 포지션을 구함
            Vector3 mapGlobalPosition = transform.position;

            // 메인 카메라의 글로벌 포지션을 구함
            Vector3 cameraGlobalPosition = mainCamera.transform.position;

            // 플레이어가 메인 카메라의 x, z 포지션과 같고 y는 카메라보다 6만큼 낮은 위치로 설정하기 위한 맵의 이동량 계산
            float deltaX = cameraGlobalPosition.x - playerGlobalPosition.x;
            float deltaY = (cameraGlobalPosition.y - 10f) - playerGlobalPosition.y;
            float deltaZ = cameraGlobalPosition.z - playerGlobalPosition.z;

            // 맵의 새로운 글로벌 포지션 계산
            Vector3 newMapPosition = new Vector3(
                mapGlobalPosition.x + deltaX,
                mapGlobalPosition.y + deltaY,
                mapGlobalPosition.z + deltaZ
            );

            // 맵의 위치를 새로 설정
            transform.position = newMapPosition;


            // 내비메쉬 데이터 업데이트
            navMeshSurface.BuildNavMesh();

            // 모든 적의 NavMeshAgent를 재배치
            foreach (NavMeshAgent agent in agents)
            {
                agent.Warp(agent.transform.position); // 현재 위치에서 내비메쉬 경로를 재설정
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
