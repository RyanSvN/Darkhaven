using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Called when something enters the trigger zone
        Debug.Log("Entered trigger zone");
    }

    private void OnTriggerExit(Collider other)
    {
        // Called when something exits the trigger zone
        Debug.Log("Exited trigger zone");
    }
}
