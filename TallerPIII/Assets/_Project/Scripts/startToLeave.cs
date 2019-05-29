using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class startToLeave : MonoBehaviour
{
    private IEnumerator coroutine;
    private bool sfx;
    public GameObject Cajita;


   
    private void Start()
    {
        Cajita.SetActive(false);

   
        StartCoroutine(Hablar(3.0f));
        

    }


    void Update()
    {

        

        if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetButton("Start"))
        {
            if (!sfx)
            {
                //AudioSource.PlayClipAtPoint(s, Vector3.zero);
                Invoke("LoadScene", 0.5f);
                sfx = true;
            }
        }

    }

    void LoadScene()
    {
        Application.LoadLevel("Nivel1");
    }

    private IEnumerator Hablar(float tiempo)
    {
        
        yield return new WaitForSeconds(tiempo);
        Cajita.SetActive(true);

    }

}
