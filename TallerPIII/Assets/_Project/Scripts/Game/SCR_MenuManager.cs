using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SCR_MenuManager : MonoBehaviour
{
    public GameObject[] Menus=new GameObject[3];// MainMenu, LevelMenu,GameMenu;
    public GameObject[] nivelesBtn= new GameObject[2];
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
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
            case 0:
                for (int i=2;i < PlayerPrefs.GetInt("NivelMax");i++)
                {
                    nivelesBtn[i].SetActive(true);
                }
                break;
        }
    }
    public void CambiarEsecena(int nivel)
    {
        print("Cambiando a la escena " + nivel);
        if(PlayerPrefs.GetInt("NivelMax")<= nivel)
        {
            if (nivel == 1)
            {
                Menus[1].SetActive(false);
                Menus[2].SetActive(false);
                Menus[0].SetActive(true);
            }
            else
            {
                Menus[0].SetActive(false);
                Menus[1].SetActive(false);
                Menus[2].SetActive(true);
            }
            SceneManager.LoadScene(nivel);
        }
        
    }
}
