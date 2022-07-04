using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public SteamVR_Controller.Device controller
    {
        get
        {
            if (!System.Object.ReferenceEquals(this.GetComponent<SteamVR_TrackedObject>(), null))
            {
                return SteamVR_Controller.Input((int)this.GetComponent<SteamVR_TrackedObject>().index);
            }
            else
            {
                return null;
            }

        }
    }
}