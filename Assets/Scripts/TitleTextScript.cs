using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTextScript : MonoBehaviour

{
    public GameObject uiObject;
    void Start()
    {
        uiObject.SetActive(false);
    }

    
    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "MainPlayer")
        {
            uiObject.SetActive(true);
            StartCoroutine("WaitForSec");
        }
        
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(10);
        Destroy(uiObject);
        Destroy(gameObject);
    }
}
