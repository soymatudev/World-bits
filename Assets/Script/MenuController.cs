using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    
    public void Scena1() 
    {
        SceneManager.LoadScene("Levels",LoadSceneMode.Single);
    }

    public void Controles()
    {
        SceneManager.LoadScene("Controles", LoadSceneMode.Single);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

}
