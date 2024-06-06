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

    // ��Ż ������ ����
    [SerializeField]
    [Tooltip("Ŭ�� �� ��Ÿ���� ������Ʈ ������")]
    GameObject placedPrefab;

    // �Է� �ý��� ��������
    Touch_control controls;

    // AR���� ���� ����
    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // ��Ż ���� ����
    List<GameObject> portal = new List<GameObject>();

    // AR�÷� �޽� ������Ʈ
    GameObject Trackables = null;


    // �� ���� ����
    public GameObject GameMap;
    public GameObject Canvas;
    public GameObject AR_Canvas;
    public GameObject Audio;
    private bool isStart = false;


    // ��ġ ��Ʈ�� �̺�Ʈ ����
    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();

        // ���� ��ġ ��Ʈ�� �����ͼ� �̺�Ʈ ����
        controls = new Touch_control();

        controls.control.touch.performed += _ => {
            var touchPosition = Pointer.current.position.ReadValue();

            // UI�� ��ġ�� ��� OnPress�� �������� ����
            if (!IsPointerOverUI(touchPosition))
                OnPress(touchPosition);
        }; 

        controls.control.touch.canceled += _ => { }; 
    }

    // ���� ���� �� XR Origin�� Trackables�ڽ��� ������(AR �÷��ν��ϸ� �����Ǵ� �޽��� -> ���� ���� �� �Ⱥ��̰� �ϱ� ����)
    private void Update()
    {
        if (Trackables == null)
            Trackables = gameObject.transform.Find("Trackables").gameObject;
    }

    // Ŭ���� �� UI���� �˻�
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

    // ������ �����
    private void OnPress(Vector3 position)
    {
        if (aRRaycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            GameObject spawnedObject = null;


            // ó�� ���� ���� �� UI�� �����, ���� �÷� �޽��� �Ⱥ��̰�
            if (isStart == false)
                GameStartInit();

            // ��Ż�� ���� ������ �� ���� ���� ī�޶� ���� �Ʒ��� �̵�
            //GameMap.GetComponent<SetMoveMap>().setPositionMap();
 

            // ��Ż ������ 1���� �����ϴ� �ڵ�
            if (portal.Count < 1)
            {
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                portal.Add(spawnedObject);
            }
            else
            {
                // 1���� �ʰ��ϴ� ������ ���, ���� �� ������Ʈ�� Add��
                Destroy(portal[0]);
                portal.RemoveAt(0);

                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                portal.Add(spawnedObject);
            }

            // ��� UI ��ġ ����
            AR_Canvas.transform.position = spawnedObject.transform.position + (Vector3.up*0.2f) + (Vector3.forward*0.6f);


        }
    }

    // ���� ���� �� ���� �Լ�
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
