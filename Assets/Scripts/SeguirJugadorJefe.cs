using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirJugadorJefe : MonoBehaviour
{

    [SerializeField] private float vidaMaxima = 50;
    private float vidaActual;

    public float radioBusqueda;
    public LayerMask capaJugador;
    public Transform transformJugador;

    public float velocidadMovimiento;
    public float distanciaMaxima;
    public Vector3 puntoInicial;

    public bool mirandoDerecha;

    public Rigidbody2D rigidbody2;
    public Animator animator;

    public EstadosMovimiento estadoActual;
    public enum EstadosMovimiento
    {
        Esperando,
        siguiendo,
        volviendo,
    }

    private void Start()
    {
        vidaActual = vidaMaxima;  
        puntoInicial = transform.position;
    }

    private void Update()
    {
        switch (estadoActual)
        {
            case EstadosMovimiento.Esperando:
                EstadoEsperando();
                break;
            case EstadosMovimiento.siguiendo:
                EstadoSiguiendo();
                break;
            case EstadosMovimiento.volviendo:
                EstadoVolviendo();
                break;
        }
    }
    public void GirarAObjitvo(Vector3 objeto)
    {
        float margen = 0.1f;

        if (objeto.x > transform.position.x + margen && !mirandoDerecha)
        {
            Rotar();
        }
        else if (objeto.x < transform.position.x - margen && mirandoDerecha)
        {
            Rotar();
        }
    }

    public void Rotar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    public void EstadoEsperando()
    {
        Collider2D jugadorColider = Physics2D.OverlapCircle(transform.position, radioBusqueda, capaJugador);
        if (jugadorColider)
        {
            transformJugador = jugadorColider.transform;
            estadoActual = EstadosMovimiento.siguiendo;
        }
    }

    public void EstadoSiguiendo()
    {
        animator.SetBool("EnemigoCerca", true);

        if (transformJugador == null)
        {
            estadoActual = EstadosMovimiento.volviendo;
            return;
        }

        float direccion = transformJugador.position.x - transform.position.x;

        rigidbody2.velocity = new Vector2(Mathf.Sign(direccion) * velocidadMovimiento, rigidbody2.velocity.y);

        if ((direccion > 0 && !mirandoDerecha) || (direccion < 0 && mirandoDerecha))
        {
            GirarAObjitvo(transformJugador.position);
        }

        if (Vector2.Distance(transform.position, puntoInicial) > distanciaMaxima || Vector2.Distance(transform.position, transformJugador.position) > distanciaMaxima)
        {
            estadoActual = EstadosMovimiento.volviendo;
            transformJugador = null;
        }
    }

    public void EstadoVolviendo()
    {
        if (Vector2.Distance(transform.position, puntoInicial) > 0.1f)
        {
            float direccion = (puntoInicial.x - transform.position.x) > 0 ? 1 : -1;
            rigidbody2.velocity = new Vector2(direccion * velocidadMovimiento, rigidbody2.velocity.y);

            if ((direccion > 0 && !mirandoDerecha) || (direccion < 0 && mirandoDerecha))
            {
                GirarAObjitvo(puntoInicial);
            }
        }
        else
        {
            rigidbody2.velocity = Vector2.zero;
            transform.position = puntoInicial;

            animator.SetBool("EnemigoCerca", false);
            estadoActual = EstadosMovimiento.Esperando;

            transformJugador = null;
        }
    }

    public void TomaDano(float dano)
    {
        vidaActual -= dano;
        if (vidaActual <= 0)
        {
            Muerte();
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

        animator.SetTrigger("Muerte");
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
        Gizmos.DrawWireSphere(puntoInicial, distanciaMaxima);
    }
}
