using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class startToPlay : MonoBehaviour
{
    public AudioClip s;
    private bool sfx;

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKeyDown)
        {
            if (!sfx)
            {
                AudioSource.PlayClipAtPoint(s, Vector3.zero);
                Invoke("LoadScene", 0.5f);
                sfx = true;
            }
        }

    }

    void LoadScene()
    {
        Application.LoadLevel("Menu");
    }
}
