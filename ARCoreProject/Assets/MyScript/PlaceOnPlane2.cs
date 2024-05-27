using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane2 : PressInputBase
{
    /// <summary>
    /// The prefab that will be instantiated on touch.
    /// </summary>
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject placedPrefab;

    /// <summary>
    /// The instantiated object.
    /// </summary>
    GameObject spawnedObject;

    /// <summary>
    /// If there is any touch input.
    /// </summary>
    bool isPressed;

    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();



    List<GameObject> box = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if there is any pointer device connected to the system.
        // Or if there is existing touch input.
        if (Pointer.current == null || isPressed == false)
            return;
 
        // Store the current touch position.
        var touchPosition = Pointer.current.position.ReadValue();

        
    }

    protected override void OnPress(Vector3 position){

        // Check if the raycast hit any trackables.
        if (aRRaycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first hit means the closest.
            var hitPose = hits[0].pose;


            if (box.Count <= 5)
            {
                // Check if there is already spawned object. If there is none, instantiated the prefab.
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                box.Add(spawnedObject);
            }
            else
            {
                Destroy(box[0]);
                box.RemoveAt(0);

                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                box.Add(spawnedObject);
            }


            // To make the spawned object always look at the camera. Delete if not needed.
            Vector3 lookPos = Camera.main.transform.position - spawnedObject.transform.position;
            lookPos.y = 0;
            spawnedObject.transform.rotation = Quaternion.LookRotation(lookPos);
        }

    }

    protected override void OnPressCancel() => isPressed = false;
}
