using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlaneNewInputSystem : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Ŭ�� �� ��Ÿ���� ������Ʈ ������")]
    GameObject placedPrefab;

    Touch_control controls;

    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    List<GameObject> box = new List<GameObject>();

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();

        // ���� ��ġ ��Ʈ�� �����ͼ� �̺�Ʈ ����
        controls = new Touch_control();

        controls.control.touch.performed += _ => {
            var touchPosition = Pointer.current.position.ReadValue();
            OnPress(touchPosition);
        }; 

        controls.control.touch.canceled += _ => { 
            //
        }; 
    }

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
            Vector3 lookPos = Camera.main.transform.position - spawnedObject.transform.position;
            lookPos.y = 0;
            spawnedObject.transform.rotation = Quaternion.LookRotation(lookPos);
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
