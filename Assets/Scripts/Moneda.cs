using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public int valor = 1;
    public GameManager manager;

    public AudioClip sonidoMoneda;

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Player"))
        {
            manager.SumarPuntos(valor);
            Destroy(this.gameObject);
            AudioManager.instance.ReproducirSonido(sonidoMoneda);

        }
    }
}
