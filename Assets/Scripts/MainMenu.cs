using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    public void PlayGame() 
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }

    public void Options() 
    {
        SceneManager.LoadSceneAsync("Option Menu");
    }

    public void QuitButton() 
    {
        Application.Quit();
    }

}