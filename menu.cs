using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
   
    public void Startgame()
    {
        SceneManager.LoadScene(1);
    }
    public void Exitgame()
    {
        Application.Quit();
    }
    public void Teach()
    {
        SceneManager.LoadScene(2);
    }
}
