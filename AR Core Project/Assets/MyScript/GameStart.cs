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
    [Tooltip("Ŭ�� �� ��Ÿ���� ������Ʈ ������")]
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

        // ���� ��ġ ��Ʈ�� �����ͼ� �̺�Ʈ ����
        controls = new Touch_control();

        controls.control.touch.performed += _ => {
            var touchPosition = Pointer.current.position.ReadValue();

            Debug.Log(touchPosition);
            // UI�� ��ġ�� ��� OnPress�� �������� ����
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

    // UI �˻� �Լ�
    private bool IsPointerOverUI(Vector2 touchPosition)
    {
        if (isStart == false)
        {
            isStart = true;
            Canvas.SetActive(true);
        }


        // EventSystem�� Raycaster�� ���� UI üũ
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = touchPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // UI�� �����ϴ��� ���� ��ȯ
        return results.Count > 0;
    }

    // ������ �����
    private void OnPress(Vector3 position)
    {

        if (aRRaycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            // ù��°�� ���� raycast hit ������Ʈ
            var hitPose = hits[0].pose;
            GameObject spawnedObject = null;


            // ������ ������Ʈ�� ������ 5���� �����ϴ� �ڵ�
            if (box.Count < 1)
            {
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                box.Add(spawnedObject);
            }
            else
            {
                // 5���� �ʰ��ϴ� ������ ���, 1��° ������Ʈ�� ���� �� ������Ʈ�� Add��
                Destroy(box[0]);
                box.RemoveAt(0);

                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                box.Add(spawnedObject);
            }

            // ������ ������Ʈ ������ �� ��������
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
