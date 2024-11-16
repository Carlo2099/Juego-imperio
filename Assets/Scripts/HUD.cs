using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI puntos;
    public GameObject[] vidas;

    private Movimiento movimientoJugador;
    [SerializeField] private float tiempoPerdidaControl;
    private Animator animator;

    private void Start()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        movimientoJugador = jugador.GetComponent<Movimiento>();

        Animator animatorDelPersonaje = jugador.GetComponent<Animator>();

        if (animatorDelPersonaje != null)
        {
            animator = animatorDelPersonaje;
        }
        else
        {
            Debug.LogWarning("Animator del personaje no encontrado.");
        }
    }


    public void actualizarPuntos(int puntosTotales)
    {
        puntos.text= puntosTotales.ToString();
    }
    public void desactivarvida(int indice)
    {
        vidas[indice].SetActive(false);
    }
    public void desactivarvida(int indice, Vector2 posicion)
    {
        vidas[indice].SetActive(false);
        animator.SetTrigger("Golpe");
        StartCoroutine(PerderControl());

        movimientoJugador.Rebote(posicion);
    }
    public void activarvida(int indice)
    {
        vidas[indice].SetActive(true);
    }
    private IEnumerator PerderControl()
    {
        movimientoJugador.sePuedeMover = false;
        yield return new WaitForSeconds(tiempoPerdidaControl);
        movimientoJugador.sePuedeMover = true;
    }


}
