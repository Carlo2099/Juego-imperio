using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public HUD hud;
    private int vidas = 3;
    public GameObject personaje;
    private int puntoTotales;
    public GameObject canva;
    public bool primeraPuerta=false;
    private void Start()
    {
        canva.gameObject.SetActive(false);
    }
    public int PuntosTotales
    {
        
        get { 
            return puntoTotales;
        }
    }


    public void SumarPuntos(int puntosASumar)
    {
        puntoTotales = puntosASumar + puntoTotales;
        Debug.Log(puntoTotales);
        hud.actualizarPuntos(puntoTotales);

        if (puntoTotales == 13)
        {
            primeraPuerta = true;
        }
    }

    public void PerderVida(Vector2 posicion)
    {
        if (vidas==1)
        {
            canva.gameObject.SetActive(true);
        }
        else
        {
            vidas -= 1;
            hud.desactivarvida(vidas, posicion);
        }

    }
    public void PerderVidaSinAnimacion()
    {
        if (vidas == 1)
        {
            canva.gameObject.SetActive(true);
        }
        else
        {
            vidas -= 1;
            hud.desactivarvida(vidas);
        }

    }
    public void GanarVida()
    {
        if (vidas==3)
        {
            return;
        }
        hud.activarvida(vidas);
        vidas += 1;
    }

}
