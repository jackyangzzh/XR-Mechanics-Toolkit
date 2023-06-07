using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BowString : MonoBehaviour
{
    public GameObject bowStringStart;
    public GameObject bowStringEnd;
    public GameObject arrowNotch; // The point where the arrow meets the bowstring
    public XRSocketInteractor socketInteractor;
    public float maxPullDistance = 1.0f; // Maximum distance the bowstring can be pulled back
    private LineRenderer lineRenderer;
    private GameObject arrow; // The arrow currently in the bow
    private Vector3 originalNotchPosition; // The original position of the arrow notch

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 3; // The number of points to connect.

        socketInteractor.selectEntered.AddListener(HandleInsert);
        socketInteractor.selectExited.AddListener(HandleRelease);

        originalNotchPosition = arrowNotch.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Set the position of the start and end of the line to match the bowstring's ends.
        lineRenderer.SetPosition(0, bowStringStart.transform.position);
        lineRenderer.SetPosition(1, arrowNotch.transform.position);
        lineRenderer.SetPosition(2, bowStringEnd.transform.position);
    }

    void HandleInsert(SelectEnterEventArgs arg0)
    {
        // When an arrow is inserted into the socket, store it
        //arrow = interactor.gameObject;
    }

    void HandleRelease(SelectExitEventArgs arg0)
    {
        // Calculate the distance from the starting position to the desired position
        float distance = Vector3.Distance(bowStringStart.transform.position, arrowNotch.transform.position);

        // If the distance is within the allowed distance, fire the arrow
        if (distance <= maxPullDistance)
        {
            FireArrow();
        }

        // Reset the position of the arrow notch
        arrowNotch.transform.position = originalNotchPosition;
    }

    void FireArrow()
    {
        // Fire the arrow here.
        // This will involve applying a force to the arrow in the direction the bow is facing,
        // and detaching the arrow from the socket interactor.
    }
}
