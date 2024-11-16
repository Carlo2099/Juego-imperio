using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueJefePro : MonoBehaviour
{
    [SerializeField] private Transform controlGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] public float danoGolpe;
    public AudioClip sonidoArma;

    private Animator animator;
    private GameManager gameManager;  

    private float tiempoEntreAtaques = 0.1f; 
    private float siguienteAtaque = 0f; 

    private void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>(); 
    }

    private void Update()
    {
        if (Time.time >= siguienteAtaque) 
        {
            Golpe(); 
            siguienteAtaque = Time.time + tiempoEntreAtaques; 
        }
    }

    public void Golpe()
    {

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controlGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Player"))
            {
                gameManager.PerderVida(colisionador.transform.position); 
                AudioManager.instance.ReproducirSonido(sonidoArma);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controlGolpe.position, radioGolpe);
    }
}
