using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ControladorMenu : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    public void CambiarEsecena(string nombre)
    {
        print("Cambiando a la escena " + nombre);
        SceneManager.LoadScene(nombre);
    }
    private void Start()
    {
        if (!GameObject.Find("Canvas"))
        {
            GameObject tempo = Instantiate(menu);
            tempo.name = "Canvas";
        }
    }

}
