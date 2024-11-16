using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TiempoTexto : MonoBehaviour
{
    public float tiempoEspera = 3.0f;
    private TextMeshPro texto;

    private void Start()
    {
        texto = GetComponent<TextMeshPro>();
        StartCoroutine(DesactivarTextoDespuesDeEspera());
    }

    private IEnumerator DesactivarTextoDespuesDeEspera()
    {
        yield return new WaitForSeconds(tiempoEspera);
        texto.enabled = false;
    }
}
