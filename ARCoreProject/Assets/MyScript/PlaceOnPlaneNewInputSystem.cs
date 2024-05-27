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


    private bool isSpawn = false;

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();

        // ���� ��ġ ��Ʈ�� �����ͼ� �̺�Ʈ ����
        controls = new Touch_control();

        controls.control.touch.performed += _ => {
            if (!isSpawn)
            {
                isSpawn = true;
                var touchPosition = Pointer.current.position.ReadValue();
                OnPress(touchPosition);
            }
        }; 

        controls.control.touch.canceled += _ => { 
            //
        }; 
    }

    private void OnPress(Vector3 position)
    {

        if (aRRaycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
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
