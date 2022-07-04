using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public Rigidbody rigidbody;

    private bool currentlyInteracting;

    private float velocityFactor = 10000f;
    private Vector3 posDelta;

    private float rotationFactor = 100f;
    private Quaternion rotationDelta;
    private float angle;
    private Vector3 axis;

    private ControllerScript attachedController;

    private Transform interactionPoint;

    private Vector3 velocity;
    private Vector3 lastPos;
    private bool thrown;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        interactionPoint = new GameObject().transform;
    }

    void FixedUpdate()
    {
        if (attachedController && currentlyInteracting)
        {
            posDelta = attachedController.transform.position - interactionPoint.position;
            velocity = posDelta * velocityFactor * Time.fixedDeltaTime;
            this.rigidbody.velocity = velocity;

            rotationDelta = attachedController.transform.rotation * Quaternion.Inverse(interactionPoint.rotation);
            rotationDelta.ToAngleAxis(out angle, out axis);

            if (angle > 180)
            {
                angle -= 360;
            }

            this.rigidbody.angularVelocity = (Time.fixedDeltaTime * angle * axis) * rotationFactor;
        }
        else if (!currentlyInteracting && thrown)
        {
            posDelta = this.transform.position - lastPos;
            velocity = posDelta * velocityFactor * Time.fixedDeltaTime;
            this.rigidbody.velocity = velocity;
            thrown = false;
        }
        lastPos = this.transform.position;
    }

    public void BeginInteraction(ControllerScript controller)
    {
        attachedController = controller;
        interactionPoint.position = controller.transform.position;
        interactionPoint.rotation = controller.transform.rotation;
        interactionPoint.SetParent(transform, true);

        currentlyInteracting = true;
    }

    public void EndInteraction(ControllerScript controller)
    {
        if (controller == attachedController)
        {
            attachedController = null;
            currentlyInteracting = false;
            thrown = true;
        }
    }

    public bool IsInteracting()
    {
        return currentlyInteracting;
    }
}
