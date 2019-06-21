using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_enemigo : MonoBehaviour
{
    private int vida = 4;
    private Animator animator;
    public enum Estado {Caminando, Corriendo, Atacando }
    public Estado miEstado;
    private string caminandoText = "Walking", atacandoText = "Attacking", runningText = "Running", atacandoRangoText= "AttackingRango";
    public int tipo;
    public GameObject piso, ojos, jugador;
    public PlayerMovement movimientoJugador;
    public bool grounded, dirActual;
    LayerMask layerMaskMapa,layerMaskPlayer;

    
    // Start is called before the first frame update
    void Start()
    {
        
        animator = GetComponent<Animator>();
        miEstado = Estado.Caminando;
        jugador = GameObject.FindWithTag("Player");
        movimientoJugador = jugador.transform.GetChild(0).gameObject.GetComponent<PlayerMovement>();
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
            //Debug.Log("algo");
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
            //Debug.Log("Distancia: " + distancia);
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
                                if (distancia <= 2f)
                                {
                                    Debug.DrawRay(ojos.transform.position, transform.right * -10f, Color.red);
                                    setAtacar();
                                }
                                break;
                            case 1:
                                if (distancia <= 6f)
                                {
                                    Debug.DrawRay(ojos.transform.position, transform.right * -10f, Color.red);
                                    setAtacarRango();
                                }
                                break;
                        }
                        break;
                }
            }
        }
        else if(rayo.collider)
        {
            if(miEstado== Estado.Atacando)
            {
                setCorrer();
            }
            //Debug.Log("Nombre: " + rayo.point.x);
            //Debug.Log("Equis: " + rayo.collider.transform.position.x);
            if (rayo.point.x - transform.position.x < 1.1f && rayo.point.x - transform.position.x > -1.1f)
            {
                
                dirActual = !dirActual;
                transform.Rotate(0, 180, 0, Space.Self);
            }
            Debug.DrawRay(ojos.transform.position, transform.right * -10f, Color.black);
            miEstado = Estado.Caminando;
        }
        else
        {
            setCorrer();
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
        animator.SetBool(atacandoRangoText, false);
    }
    private void setCaminar()
    {
        miEstado = Estado.Caminando;
        animator.SetBool(runningText, false);
        animator.SetBool(caminandoText, true);
        animator.SetBool(atacandoText, false);
        animator.SetBool(atacandoRangoText, false);
    }
    private void setAtacar()
    {
        miEstado = Estado.Atacando;
        animator.SetBool(runningText, false);
        animator.SetBool(caminandoText, false);
        animator.SetBool(atacandoText,true);
        animator.SetBool(atacandoRangoText, false);
    }
    private void setAtacarRango()
    {
        miEstado = Estado.Atacando;
        animator.SetBool(runningText, false);
        animator.SetBool(caminandoText, false);
        animator.SetBool(atacandoText, false);
        animator.SetBool(atacandoRangoText, true);
    }
    public void hacerDanio()
    {
        Debug.Log("Daño daño!");
        movimientoJugador.recibirDanio(1);
    }
    public void recibirDanio(int _danio)
    {
        vida -= _danio;
        if (vida <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
