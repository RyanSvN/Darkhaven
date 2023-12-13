using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Animator animator;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Fade();
    }

    public void QuitGame()
    {   
        Application.Quit();
    }

    public void Fade()
    {
        animator.SetTrigger("FadeOut");
    }
}
