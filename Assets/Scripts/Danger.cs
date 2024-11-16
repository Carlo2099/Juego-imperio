using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    public GameManager manager;
    public AudioClip sonidoPerderVida;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = new Vector3(-9.599994f, -3.695437f, 0f);
            manager.PerderVidaSinAnimacion();
            AudioManager.instance.ReproducirSonido(sonidoPerderVida);
        }
    }
}
