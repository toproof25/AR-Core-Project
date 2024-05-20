using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARPlaneManager))]
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnIn : MonoBehaviour
{
    [SerializeField]
    GameObject PlaceIndicator;

    [SerializeField]
    GameObject PlacePrefab;

    [SerializeField]
    GameObject SpawnedObject;

    [SerializeField]
    InputAction touchInput;

    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();


    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        touchInput.performed += _ => {
            Debug.Log("touch");
            //PlaceObject();
        };
        touchInput.canceled += _ => { };

        PlaceIndicator.SetActive(false);
    }

    void Update()
    {
        if (aRRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            Debug.Log("raycast");

            var hitpose = hits[0].pose;
            PlaceIndicator.transform.SetPositionAndRotation(hitpose.position, hitpose.rotation);

            if (!PlaceIndicator.activeInHierarchy)
                PlaceIndicator.SetActive(true);
            //else
                //PlaceIndicator.SetActive(false);
        }
    }

    public void PlaceObject()
    {
        if (!PlaceIndicator.activeInHierarchy)
            return;

        if (SpawnedObject == null)
            SpawnedObject = Instantiate(PlacePrefab, PlaceIndicator.transform.position, PlaceIndicator.transform.rotation);
        else
            SpawnedObject.transform.SetPositionAndRotation(PlaceIndicator.transform.position, PlaceIndicator.transform.rotation);
    }

    private void OnEnable() { touchInput.Enable(); }
    private void OnDisable() { touchInput.Disable(); }

}
