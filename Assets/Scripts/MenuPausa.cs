using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausar;
    [SerializeField] private GameObject menuPausa;

    private bool juegoPausado = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if (juegoPausado)
            {
                Reanudar();
                juegoPausado = false;
            }
            else
            {
                Pausa();
                juegoPausado=true; 
            }
            
        }
    }

    public void Pausa()
    {
        Time.timeScale = 0.0f;
        botonPausar.SetActive(false);
        menuPausa.SetActive(true);
    }
    public void Reanudar()
    {
        Time.timeScale = 1.0f;
        botonPausar.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("SampleScene");
    }

    public void Salir()
    {
        SceneManager.LoadScene("MenuInicial");
    }
}
