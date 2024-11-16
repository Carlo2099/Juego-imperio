using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamaraScript : MonoBehaviour
{
    public GameObject personaje;
    public GameObject gameOver;
    private AudioSource audioSource;
    void Update()
    {
        Vector3 position = transform.position;
        position.x = personaje.transform.position.x;

        Vector4 position4 = transform.position;
        position.y = personaje.transform.position.y;

        transform.position = position;
    }
}
