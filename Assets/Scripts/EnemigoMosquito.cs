using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemigoMosquito : MonoBehaviour
{
    [SerializeField] private float vida;

    [SerializeField] float velocidad;
    [SerializeField] Vector2 moveDirection = new Vector2(1f, 0.25f);
    [SerializeField] GameObject derechaCheck, techoCheck, sueloCheck;
    [SerializeField] Vector2 derechaCheckSize, techoCheckSize, sueloCheckSize;
    [SerializeField] LayerMask capaSuelo, platform;
    [SerializeField] bool subiendo = true;

    public GameManager manager;
    public AudioClip sonidoPerderVida;
    public AudioClip sonidoPerderVidaMosquito;

    private bool tocadoSuelo, tocadoArriba, tocadoDerecha;
    private Rigidbody2D Enemigo;

    private Animator animator;

    void Start()
    {
        animator=GetComponent<Animator>();
        Enemigo = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direcionamiento();
    }

    void FixedUpdate()
    {
        Enemigo.velocity = moveDirection * velocidad;
    }

    void direcionamiento()
    {
        tocadoDerecha = HitDetector(derechaCheck, derechaCheckSize, (capaSuelo | platform));
        tocadoArriba = HitDetector(techoCheck, techoCheckSize, (capaSuelo | platform));
        tocadoSuelo = HitDetector(sueloCheck, sueloCheckSize, (capaSuelo | platform));

        if (tocadoDerecha)
        {
            Voltear();
        }
        if (tocadoArriba && subiendo)
        {
            CambiarDireccionEnY();
        }
        if (tocadoSuelo && !subiendo)
        {
            CambiarDireccionEnY();
        }
    }

    bool HitDetector(GameObject gameObject, Vector2 size, LayerMask layer)
    {
        return Physics2D.OverlapBox(gameObject.transform.position, size, 0f, layer);
    }

    void CambiarDireccionEnY()
    {
        moveDirection.y = -moveDirection.y;
        subiendo = !subiendo;
    }

    void Voltear()
    {
        transform.Rotate(new Vector2(0, 180));
        moveDirection.x = -moveDirection.x;
    }
    public void TomaDano(float dano)
    {
        vida -= dano;
        velocidad -= 2;
        AudioManager.instance.ReproducirSonido(sonidoPerderVidaMosquito); 
        if (vida <= 0)
        {
            Muerte();
            manager.GanarVida();
        }

    }

    private void Muerte()
    {
        animator.SetTrigger("Muerte");
        StartCoroutine(DesactivarDespuesDeAnimacion());
    }

    private IEnumerator DesactivarDespuesDeAnimacion()
    {
        // Espera a que la animaci�n de muerte termine
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Muerte"))
        {
            yield return null;
        }

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        // Desactiva el renderizador y los colisionadores
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
