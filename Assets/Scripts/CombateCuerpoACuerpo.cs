using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCuerpoACuerpo : MonoBehaviour
{
    [SerializeField] private Transform controlGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] public float danoGolpe;
    public AudioClip sonidoArma;

    private Animator animator;
    private void Start()
    {
        animator= GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AudioManager.instance.ReproducirSonido(sonidoArma);
            Golpe();
        }
    }
    public void Golpe()
    {
        animator.SetTrigger("Ataque"); 
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controlGolpe.position, radioGolpe);
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                colisionador.transform.GetComponent<EnemigoMosquito>().TomaDano(danoGolpe);
            }
            if (colisionador.CompareTag("Golem"))
            {
                colisionador.transform.GetComponent<GolemScript>().TomaDano(danoGolpe);
            }
            if (colisionador.CompareTag("Jefe"))
            {
                colisionador.transform.GetComponent<SeguirJugadorJefe>().TomaDano(danoGolpe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawWireSphere(controlGolpe.position,radioGolpe);
    }
}
