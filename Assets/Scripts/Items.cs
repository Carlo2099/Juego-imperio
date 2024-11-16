using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{

    public AudioClip sonidoItem;
    public CombateCuerpoACuerpo combateScript;
    public GameObject mensajeOculto;
    private bool mensajeVisible = false;

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Player"))
        {
            combateScript.danoGolpe += 1;
            Destroy(this.gameObject);
            AudioManager.instance.ReproducirSonido(sonidoItem);

            // Mostrar el mensaje oculto.
            mensajeVisible = true;
            mensajeOculto.SetActive(true);


            // Llamar al m�todo para ocultar el mensaje despu�s de 3 segundos.
            StartCoroutine(ocultarMensaje(3.0f));
        }
    }
    private IEnumerator ocultarMensaje(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        mensajeVisible = false;
        mensajeOculto.SetActive(false);
    }
}
