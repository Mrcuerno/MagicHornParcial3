using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_enemigo : MonoBehaviour
{
    private int vida = 4;
    private Animator animator;
    public enum Estado { Idle, Moviendose, Atacando , Saltando}
    public Estado miEstado;
    private string moviendoText = "Moving", atacandoText = "Attack", jumpText="Jump";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        miEstado = Estado.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RecibirDanio(int _danio)
    {
        vida -= _danio;
        if (vida <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
