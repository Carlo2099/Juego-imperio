using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Movimiento : MonoBehaviour
{
    public AudioClip SoundSalto;
    public AudioClip SoundPuerta;

    private Rigidbody2D rigidbody2;
    private float horizontal;
    private Animator animator;
    private bool enSuelo = true;
    public GameObject textoFlotante;

    public GameManager gameManager;

    //da�o
    public bool sePuedeMover = true;
    [SerializeField] private Vector2 velRebote;

    // Objeto que subir� en Y
    int varAuxPuerta = 0;
    //int varAuxPuerta1 = 0;
    //int varAuxPuerta2 = 0;

    public GameObject objetoParaSubir1;
    public GameObject objetoParaSubir2;
    public GameObject objetoParaSubir3;

    private Vector3 posicionInicialObjeto1;
    private Vector3 posicionInicialObjeto2;
    private Vector3 posicionInicialObjeto3;
    private bool subirObjeto = false;
    private bool subirObjetoAux = true;

    private bool sonidoReproducido = false;

    public GameObject enemigosFoza;

    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        posicionInicialObjeto1 = objetoParaSubir1.transform.position;
        posicionInicialObjeto2 = objetoParaSubir2.transform.position;
        posicionInicialObjeto3 = objetoParaSubir3.transform.position;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (horizontal < 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        animator.SetBool("corriendo", horizontal != 0.0f);
        if (sePuedeMover)
        {
            if (Input.GetKeyDown(KeyCode.W) && enSuelo)
            {
                Jump();
            }
        }

        if (transform.position.x > 25.0f && transform.position.x < 28.0f)
        {

            if (gameManager.primeraPuerta)
            {
                print("Abriendo");
                gameManager.primeraPuerta = false;
                varAuxPuerta = 1;

                subirObjeto = true;
            }
            else
            {
                if (varAuxPuerta != 1)
                {
                    Destroy(GameObject.Find(textoFlotante.name + ("(Clone)")));
                    MostrarTexto();
                }
            }
        }
        if (subirObjeto)
        {
            SubirObjeto(objetoParaSubir1, posicionInicialObjeto1);
        }
        //enemigos grupo 1
        if (transform.position.x >= 90.0f && transform.position.x <= 99.0f &&
        transform.position.y >= -7.0f && transform.position.y <= 1.0f)
        {
            enemigosFoza.SetActive(true);
            subirObjetoAux = true;
}
        //fosa puerta 1
        if (transform.position.x >= 104.0f && transform.position.x <= 107.0f &&
        transform.position.y >= -38.0f && transform.position.y <= -34.0f && subirObjetoAux == true)
        {
            SubirObjeto(objetoParaSubir2, posicionInicialObjeto2);
            subirObjetoAux = true;
        }
        //fosa puerta 2
        if (transform.position.x >= 117.0f && transform.position.x <= 120.0f &&
        transform.position.y >= -38.0f && transform.position.y <= -34.0f && subirObjetoAux == true)
        {
            SubirObjeto(objetoParaSubir3, posicionInicialObjeto3);
        }
    }


    private void Jump()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundSalto);
        rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, 0);
        rigidbody2.AddForce(Vector2.up * 400);
        enSuelo = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suelo") || collision.gameObject.layer == LayerMask.NameToLayer("plataforma"))
        {
            enSuelo = true; 
        }
    }


    private void FixedUpdate()
    {
        if (sePuedeMover)
        {
            moverizqDrc();
        }
        
    }
    public void Rebote(Vector2 puntoGolpe)
    {
        float direccionX = (horizontal > 0.0f) ? 1.0f : -1.0f;
        rigidbody2.velocity = new Vector2(velRebote.x * puntoGolpe.x * direccionX, velRebote.y);
    }

    private void moverizqDrc()
    {
        rigidbody2.velocity = new Vector2(horizontal * 4, rigidbody2.velocity.y);
    }

    public void MostrarTexto()
    {
        GameObject texto = Instantiate(textoFlotante, this.transform);
    }

    private void SubirObjeto(GameObject objetoParaSubir, Vector3 posicionInicialObjeto)
    {
        if (!sonidoReproducido)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundPuerta);
            sonidoReproducido = true;
        }

        if (objetoParaSubir.transform.position.y < posicionInicialObjeto.y + 3.0f)
        {
            objetoParaSubir.transform.Translate(Vector3.up * Time.deltaTime);
        }
        else
        {
            subirObjeto = false;
            subirObjetoAux = false;
        }
    }
}