using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabController : MonoBehaviour
{
    public static GrabController controller;

    [SerializeField]
    private Transform grabPoint;
    [SerializeField]
    private GameObject grabbedObject;
    [SerializeField]
    private float grabbedObjectYPosition;

    private bool isGrabbing;

    private void Awake()
    {
        controller = this;
    }

    private void Update()
    {
        if (InteractionController.controller.IsDetectingObject)
        {
            Grab();
            Drop();
        }
    }

    private void Grab()
    {
        if (isGrabbing)
        {
            isGrabbing = false;
            grabbedObject.transform.parent = null;
            grabbedObject.transform.position = new Vector2(grabbedObject.transform.position.x,
                grabbedObjectYPosition);
            grabbedObject = null;
        }
    }

    private void Drop()
    {
        if (!isGrabbing)
        {
            isGrabbing = true;
            grabbedObject = InteractionController.controller.DetectedObject;
            grabbedObject.transform.parent = transform;
            grabbedObjectYPosition = grabbedObject.transform.position.y;
            grabbedObject.transform.localPosition = grabPoint.localPosition;
        }
    }

    private void OnDestroy()
    {
        controller = null;
    }
}
