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

    [SerializeField]
    [Tooltip("클릭 시 나타나는 오브젝트 프리팹")]
    GameObject placedPrefab;

    Touch_control controls;

    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    List<GameObject> box = new List<GameObject>();

    GameObject Trackables = null;

    public GameObject Canvas;
    private bool isStart = false;


    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();

        // 만든 터치 컨트롤 가져와서 이벤트 연결
        controls = new Touch_control();

        controls.control.touch.performed += _ => {
            var touchPosition = Pointer.current.position.ReadValue();

            Debug.Log(touchPosition);
            // UI가 터치된 경우 OnPress를 실행하지 않음
            if (!IsPointerOverUI(touchPosition))
            {
                OnPress(touchPosition);
            }


            //OnPress(touchPosition);
        }; 

        controls.control.touch.canceled += _ => { 
            //
        }; 
    }

    private void Update()
    {
        if (Trackables = null)
        {
            Trackables = gameObject.transform.Find("Trackables").gameObject;
        }
    }

    // UI 검사 함수
    private bool IsPointerOverUI(Vector2 touchPosition)
    {
        if (isStart == false)
        {
            isStart = true;
            Canvas.SetActive(true);
        }


        // EventSystem과 Raycaster를 통해 UI 체크
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = touchPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // UI가 존재하는지 여부 반환
        return results.Count > 0;
    }

    // 누르면 실행됨
    private void OnPress(Vector3 position)
    {

        if (aRRaycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            // 첫번째로 닿은 raycast hit 오브젝트
            var hitPose = hits[0].pose;
            GameObject spawnedObject = null;


            // 생성된 오브젝트의 개수가 5개로 제한하는 코드
            if (box.Count < 1)
            {
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                box.Add(spawnedObject);
            }
            else
            {
                // 5개를 초과하는 생성일 경우, 1번째 오브젝트를 제거 후 오브젝트를 Add함
                Destroy(box[0]);
                box.RemoveAt(0);

                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                box.Add(spawnedObject);
            }

            // 생성된 오브젝트 방향을 내 방향으로
            //Vector3 lookPos = Camera.main.transform.position - spawnedObject.transform.position;
            //lookPos.y = 0;
            //spawnedObject.transform.rotation = Quaternion.LookRotation(lookPos);
        }
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
