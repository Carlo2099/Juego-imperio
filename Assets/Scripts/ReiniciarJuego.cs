using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReiniciarJuego : MonoBehaviour
{
    public void reiniciarEscena()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("SampleScene");
    }
}
