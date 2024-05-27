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
    [Tooltip("클릭 시 나타나는 오브젝트 프리팹")]
    GameObject placedPrefab;

    Touch_control controls;

    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    List<GameObject> box = new List<GameObject>();


    private bool isSpawn = false;

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();

        // 만든 터치 컨트롤 가져와서 이벤트 연결
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
