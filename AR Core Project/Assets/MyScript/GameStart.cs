using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

[RequireComponent(typeof(ARRaycastManager))]
public class GameStart : MonoBehaviour
{

    // 포탈 프리팹 설정
    [SerializeField]
    [Tooltip("클릭 시 나타나는 오브젝트 프리팹")]
    GameObject placedPrefab;

    // 입력 시스템 가져오기
    Touch_control controls;

    // AR레이 변수 설정
    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // 포탈 개수 설정
    List<GameObject> portal = new List<GameObject>();

    // AR플랜 메쉬 오브젝트
    GameObject Trackables = null;


    // 각 변수 설정
    public GameObject GameMap;
    public GameObject Canvas;
    public GameObject AR_Canvas;
    public GameObject Audio;
    private bool isStart = false;


    // 터치 컨트롤 이벤트 연결
    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();

        // 만든 터치 컨트롤 가져와서 이벤트 연결
        controls = new Touch_control();

        controls.control.touch.performed += _ => {
            var touchPosition = Pointer.current.position.ReadValue();

            // UI가 터치된 경우 OnPress를 실행하지 않음
            if (!IsPointerOverUI(touchPosition))
                OnPress(touchPosition);
        }; 

        controls.control.touch.canceled += _ => { }; 
    }

    // 게임 시작 시 XR Origin의 Trackables자식을 가져옴(AR 플랜인식하면 생성되는 메쉬들 -> 게임 시작 시 안보이게 하기 위함)
    private void Update()
    {
        if (Trackables == null)
            Trackables = gameObject.transform.Find("Trackables").gameObject;
    }

    // 클릭한 곳 UI인지 검사
    private bool IsPointerOverUI(Vector2 touchPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = touchPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }

    // 누르면 실행됨
    private void OnPress(Vector3 position)
    {
        if (aRRaycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            GameObject spawnedObject = null;


            // 처음 게임 시작 시 UI가 생기고, 벽에 플랜 메쉬를 안보이게
            if (isStart == false)
                GameStartInit();

            // 포탈을 새로 생성할 때 마다 맵을 카메라 기준 아래로 이동
            //GameMap.GetComponent<SetMoveMap>().setPositionMap();
 

            // 포탈 개수를 1개로 제한하는 코드
            if (portal.Count < 1)
            {
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                portal.Add(spawnedObject);
            }
            else
            {
                // 1개를 초과하는 생성일 경우, 제거 후 오브젝트를 Add함
                Destroy(portal[0]);
                portal.RemoveAt(0);

                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                portal.Add(spawnedObject);
            }

            // 목숨 UI 위치 조절
            AR_Canvas.transform.position = spawnedObject.transform.position + (Vector3.up*0.2f) + (Vector3.forward*0.6f);


        }
    }

    // 게임 시작 시 실행 함수
    private void GameStartInit()
    {
        SendMessge.Instance.ShowMessage("Game start!!! Find the key and escape!");

        isStart = true;
        GameMap.SetActive(true);
        Audio.SetActive(true);
        Trackables.SetActive(false);
        Canvas.SetActive(true);
        AR_Canvas.SetActive(true);
    }

    private void OnEnable()
    {
        controls.control.Enable();
    }
    private void OnDisable()
    {
        controls.control.Disable();
    }

}
