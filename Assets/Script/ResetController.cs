using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetController : MonoBehaviour
{

    public PlayerLs referenciaL1;

    int monedas = 0;
    int vida = 3;
    float time = 300.0f;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
        if (Input.GetKeyDown(KeyCode.M)) 
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetToDefaults();
        }

        if (((int)referenciaL1.time) == 0)
        {
            ResetToDefaults();
        }

    }

    public void ResetToDefaults()
    {
        referenciaL1.monedas = monedas;
        referenciaL1.vida = vida;
        referenciaL1.time = time;

        SavePlayerPrefs();
    }
    private void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("monedas", referenciaL1.monedas);
        PlayerPrefs.SetInt("vida", referenciaL1.vida);
        PlayerPrefs.SetFloat("time", referenciaL1.time);
        PlayerPrefs.SetFloat("checkpoint_x", PlayerPrefs.GetFloat("positionO_x"));
        PlayerPrefs.SetFloat("checkpoint_y", PlayerPrefs.GetFloat("positionO_y"));
        PlayerPrefs.SetFloat("checkpoint_z", PlayerPrefs.GetFloat("positionO_z"));
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

        public void TogglePause()
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Time.timeScale = 0; // Detiene el tiempo del juego
                                    // Puedes deshabilitar otros componentes o lógicas de juego aquí
            }
            else
            {
                Time.timeScale = 1; // Reanuda el tiempo del juego
                                    // Puedes habilitar nuevamente los componentes o lógicas de juego aquí
            }
        }

    public void Menu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
