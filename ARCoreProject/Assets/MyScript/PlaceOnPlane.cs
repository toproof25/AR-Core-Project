using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : PressInputBase
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

        // Check if the raycast hit any trackables.
        if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first hit means the closest.
            var hitPose = hits[0].pose;

            // Check if there is already spawned object. If there is none, instantiated the prefab.
            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                // Change the spawned object position and rotation to the touch position.
                spawnedObject.transform.position = hitPose.position;
                spawnedObject.transform.rotation = hitPose.rotation;
            }

            // To make the spawned object always look at the camera. Delete if not needed.
            Vector3 lookPos = Camera.main.transform.position - spawnedObject.transform.position;
            lookPos.y = 0;
            spawnedObject.transform.rotation = Quaternion.LookRotation(lookPos);
        }
    }

    protected override void OnPress(Vector3 position) => isPressed = true;

    protected override void OnPressCancel() => isPressed = false;
}
