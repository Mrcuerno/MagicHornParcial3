using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ControladorMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void CambiarEsecena(string nombre)
    {
        print("Cambiando a la escena " + nombre);
        SceneManager.LoadScene(nombre);
    }

    
}
