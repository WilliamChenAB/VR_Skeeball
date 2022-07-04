using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBall : MonoBehaviour {

    public GameObject resetBallPosition;

    private Vector3 resetBallVelocity = new Vector3(-0.5f, -0.5f, 0f);

    private void OnTriggerEnter(Collider collider)
    {
        InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
        if (collidedItem)
        {
            if (!collidedItem.IsInteracting())
            {
                collidedItem.transform.position = resetBallPosition.transform.position;
                collidedItem.GetComponent<Rigidbody>().velocity = resetBallVelocity;
            }
        }
    }
}
