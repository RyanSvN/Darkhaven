using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{

    public Animator Animator;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FadeToLevel(1);
        }
    }

    public void FadeToLevel (int levelIndex)
    {
        Animator.SetTrigger("FadeOut");
    }
}
