using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SCR_MenuManager : MonoBehaviour
{
    public GameObject[] Menus=new GameObject[4];// MainMenu, LevelMenu,GameMenu;
    public GameObject[] nivelesBtn= new GameObject[2];
    public GameObject[] Objetos = new GameObject[2];
    private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if(PlayerPrefs.GetInt("NivelMax")==0)
        {
            PlayerPrefs.SetInt("NivelMax", 2);
            PlayerPrefs.Save();
        }
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void btnCambioMenu(int menu)
    {
        Menus[0].SetActive(false);
        Menus[1].SetActive(false);
        Menus[2].SetActive(false);
        Menus[menu].SetActive(true);
        switch (menu)
        {
            case 1:
                for (int i=0;i < PlayerPrefs.GetInt("NivelMax")-2 && i < 5;i++)
                {
                    nivelesBtn[i].SetActive(true);
                }
                break;
        }
    }
    public void victoria()
    {
        int escenaActual=SceneManager.GetActiveScene().buildIndex;
        escenaActual++;
        if (PlayerPrefs.GetInt("NivelMax") < escenaActual)
        {
            PlayerPrefs.SetInt("NivelMax", escenaActual);
        }
        PlayerPrefs.Save();
        CambiarEscena(escenaActual);
    }
    public void CambiarEscena(int nivel)
    {
        Debug.Log("Nivel max: " + PlayerPrefs.GetInt("NivelMax"));
        print("Cambiando a la escena " + nivel);
        Time.timeScale = 1;
        isPaused = false;
        if (PlayerPrefs.GetInt("NivelMax")>= nivel)
        {
            if (nivel == 1)
            {
                Menus[1].SetActive(false);
                Menus[2].SetActive(false);
                Menus[3].SetActive(false);
                Menus[0].SetActive(true);
            }
            else
            {
                Menus[0].SetActive(false);
                Menus[1].SetActive(false);                
                Menus[2].SetActive(true);
                Menus[3].SetActive(false);
                Objetos[0].SetActive(false);
                //Objetos[1].SetActive(false);
            }
            SceneManager.LoadScene(nivel);
        }
        
    }
    public void salir()
    {
        Application.Quit();
    }
    public void btnPause()
    {
        if (isPaused)
        {
            Menus[3].SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            isPaused = true;
            Menus[3].SetActive(true);
            Time.timeScale = 0;
        }
    }
}
