using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class startToLeave : MonoBehaviour
{

    private bool sfx;
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
}
