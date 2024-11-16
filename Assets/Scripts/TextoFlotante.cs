using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextoFlotante : MonoBehaviour
{
    public float tiempoDeVida = 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tiempoDeVida-=Time.deltaTime;
        if (tiempoDeVida<=0)
        {
            Destroy(this.gameObject);
        }
        transform.position = new Vector3(transform.parent.position.x,
                                        transform.parent.position.y+3);

        if (transform.parent.localScale.x==-1 && this.transform.localScale.x == 1)
        {
            this.transform.localScale = new Vector3(-1f,1f,1f);
        }
        if (transform.parent.localScale.x == 1 && this.transform.localScale.x == -1)
        {
            this.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
