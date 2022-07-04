using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleInteraction : MonoBehaviour
{
    public int pointGain;

    private void OnTriggerEnter(Collider collider)
    {
        InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
        if (collidedItem)
        {
            if (!collidedItem.IsInteracting())
            {
                BallsManager.ballsLeft -= 1;
                ScoreManager.score += pointGain;
                Destroy(collidedItem.gameObject);
            }
        }
    }
}