using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Quitgame()
    {
        Application.Quit();
    }


    
}
