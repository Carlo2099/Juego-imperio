using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GolemScript : MonoBehaviour
{
    [SerializeField] private float vida;

    [SerializeField] private float velocidad;
    [SerializeField] Transform controladorSuelo;
    [SerializeField] private float distancia;
    [SerializeField] private bool movimientoDerecho;
    private Rigidbody2D rigidbody2;

    public GameManager manager;
    public AudioClip sonidoPerderVida;
    public AudioClip sonidoMuerte;

    public void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
    }
    public void FixedUpdate()
    {
        RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position,Vector2.down,distancia);
        rigidbody2.velocity = new Vector2(velocidad,rigidbody2.velocity.y);

        if (informacionSuelo==false)
        {
            Girar();
        }

    }
    public void Girar()
    {
        movimientoDerecho = !movimientoDerecho;
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y+180,0);
        velocidad *= -1;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distancia);
    }
    public void TomaDano(float dano)
    {
        vida -= dano;
        velocidad -= 2;
        AudioManager.instance.ReproducirSonido(sonidoMuerte);
        if (vida <= 0)
        {
            Muerte();
            manager.GanarVida();
        }

    }

    private void Muerte()
    {
        Renderer renderer = GetComponent<Renderer>();
        Collider2D[] colliders = GetComponents<Collider2D>();

        if (renderer != null)
        {
            renderer.enabled = false;
        }

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            manager.PerderVida(collision.transform.position);

            AudioManager.instance.ReproducirSonido(sonidoPerderVida);
        }
    }
}

