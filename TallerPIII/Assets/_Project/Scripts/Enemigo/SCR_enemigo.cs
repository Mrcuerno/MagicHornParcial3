﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_enemigo : MonoBehaviour
{
    private int vida = 4;
    private Animator animator;
    public enum Estado {Caminando, Corriendo, Atacando }
    public Estado miEstado;
    private string caminandoText = "Walking", atacandoText = "Attacking", runningText = "Running";
    public int tipo;
    public GameObject piso, ojos, jugador;
    public bool grounded, dirActual;
    LayerMask layerMaskMapa,layerMaskPlayer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        miEstado = Estado.Caminando;
        jugador = GameObject.FindWithTag("Player");
        layerMaskMapa = LayerMask.GetMask("Default");
        layerMaskPlayer = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D hitColliders = Physics2D.OverlapCircle(piso.transform.position, 0.1f);
        if (hitColliders)
        {
            // if(hitColliders[i].GetComponent<SCR_enemigo>())
            //hitColliders[i].transform.root.GetComponent<SCR_enemigo>()?.mover("Sonido", _centro, _prioridad);
            Debug.Log("algo");
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        RaycastHit2D rayo = Physics2D.Raycast(ojos.transform.position, transform.right * -1, 10f);
        if (rayo.collider && rayo.collider.transform.root.tag == "Player")
        {
            float distancia = Mathf.Sqrt((rayo.collider.transform.position.x - transform.position.x) * (rayo.collider.transform.position.x - transform.position.x));
            Debug.Log("Distancia: " + distancia);
            if (distancia <= 10f)
            {
                switch (miEstado) {
                    case Estado.Caminando:
                            Debug.DrawRay(ojos.transform.position, transform.right * -10f, Color.blue);
                            setCorrer();
                            break;
                    case Estado.Atacando:
                        switch (tipo)
                        {
                            case 0:
                                if (distancia > 2f)
                                {
                                    Debug.DrawRay(ojos.transform.position, transform.right * -10f, Color.red);
                                    setCorrer();
                                }
                                break;
                            case 1:
                                if (distancia > 6f)
                                {
                                    Debug.DrawRay(ojos.transform.position, transform.right * -10f, Color.red);
                                    setCorrer();
                                }
                                break;
                        }
                        break;
                    case Estado.Corriendo:
                        switch (tipo)
                        {
                            case 0:
                                if (distancia <= 2f && miEstado != Estado.Atacando)
                                {
                                    Debug.DrawRay(ojos.transform.position, transform.right * -10f, Color.red);
                                    setAtacar();
                                }
                                break;
                            case 1:
                                if (distancia <= 6f && miEstado != Estado.Atacando)
                                {
                                    Debug.DrawRay(ojos.transform.position, transform.right * -10f, Color.red);
                                    setAtacar();
                                }
                                break;
                        }
                        break;
                }
            }
        }
        else if(rayo.collider)
        {
            if (rayo.collider.transform.position.x - transform.position.x < 1.5 && rayo.collider.transform.position.x - transform.position.x > -1.5)
            {
                dirActual = !dirActual;
                transform.Rotate(0, 180, 0, Space.Self);
            }
            Debug.DrawRay(ojos.transform.position, transform.right * -10f, Color.black);
            miEstado = Estado.Caminando;
        }
        switch (miEstado)
        {
            case Estado.Caminando:
                if (grounded)
                {
                    transform.Translate(Vector2.left * Time.deltaTime, Space.Self);
                }
                else
                {
                    dirActual = !dirActual;
                    transform.Rotate(0, 180, 0, Space.Self);
                }
                break;
            case Estado.Corriendo:
                if (grounded)
                {
                    transform.Translate(Vector2.left * 2f * Time.deltaTime, Space.Self);
                }
                else
                {
                    dirActual = !dirActual;
                    transform.Rotate(0, 180, 0, Space.Self);
                    miEstado = Estado.Caminando;
                }
                break;
            case Estado.Atacando:
                
                break;
        }
    }
    private void setCorrer()
    {
        miEstado = Estado.Corriendo;
        animator.SetBool(runningText, true);
        animator.SetBool(caminandoText, false);
        animator.SetBool(atacandoText, false);
    }
    private void setCaminar()
    {
        miEstado = Estado.Caminando;
        animator.SetBool(runningText, false);
        animator.SetBool(caminandoText, true);
        animator.SetBool(atacandoText, false);
    }
    private void setAtacar()
    {
        miEstado = Estado.Atacando;
        animator.SetBool(runningText, false);
        animator.SetBool(caminandoText, false);
        animator.SetBool(atacandoText, true);
    }
    public void HacerDanio()
    {
        Debug.Log("Daño daño!");
        //jugador.GetComponent<PlayerMovement>().recibirDanio(1);
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
