using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandCollision : MonoBehaviour
{
    [SerializeField] private SteamVR_Input_Sources handSelection;

    private GameObject temp;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other)
    {
        if (handSelection == SteamVR_Input_Sources.LeftHand)
        {
            InteractionWithCube.leftCollision = true;
            InteractionWithCube.collidingLeft = other.gameObject;
        }
        else
        {
            InteractionWithCube.rightCollision = true;
            InteractionWithCube.collidingRight = other.gameObject;
        }

        
    }

    private void OnCollisionExit(Collision other)
    {
        if (handSelection == SteamVR_Input_Sources.LeftHand)
        {
            InteractionWithCube.leftCollision = false;
            InteractionWithCube.collidingLeft = null;
        }
        else
        {
            InteractionWithCube.rightCollision = false;
            InteractionWithCube.collidingRight = null;
        }
    }
}
