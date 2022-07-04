using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerScript : MonoBehaviour
{
    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    private Valve.VR.EVRButtonId menuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;

    private Controller controller;

    HashSet<InteractableItem> objectsHoveringOver = new HashSet<InteractableItem>();

    private InteractableItem closestItem;
    private InteractableItem interactingItem;

    private Color originalColor;

    void Start()
    {
        controller = GetComponent<Controller>();
        originalColor = GameObject.Find("Ball").GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller not initialized");

            return;
        }

        if (controller.controller.GetPressDown(menuButton))
        {
            SceneManager.LoadScene("Scene1");
        }

        if (controller.controller.GetPressDown(gripButton))
        {
            float minDistance = float.MaxValue;
            float distance;

            foreach (InteractableItem item in objectsHoveringOver)
            {
                distance = (item.transform.position - transform.position).sqrMagnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestItem = item;
                }
            }

            interactingItem = closestItem;
            closestItem = null;

            if (interactingItem)
            {
                if (interactingItem.IsInteracting())
                {
                    interactingItem.EndInteraction(this);
                }
                interactingItem.BeginInteraction(this);
                interactingItem.GetComponent<Renderer>().material.color = Color.green;
            }
            
        }

        if (controller.controller.GetPress(gripButton) && interactingItem != null)
        {
            interactingItem.GetComponent<Renderer>().material.color = Color.green;
        }

        if (controller.controller.GetPressUp(gripButton) && interactingItem != null)
        {
            interactingItem.GetComponent<Renderer>().material.color = originalColor;
            interactingItem.EndInteraction(this);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
        if (collidedItem)
        {
            objectsHoveringOver.Add(collidedItem);
            collidedItem.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
        if (collidedItem)
        {
            collidedItem.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
        if (collidedItem)
        {
            objectsHoveringOver.Remove(collidedItem);
            collidedItem.GetComponent<Renderer>().material.color = originalColor;
        }
    }
}