using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_enemigo : MonoBehaviour
{
    private int vida = 4;
    private Animator animator;
    public enum Estado { Idle, Caminando, Corriendo, Atacando}
    public Estado miEstado;
    private string caminandoText = "Walking", atacandoText = "Attacking", runningText="Running";
    public int tipo;
    public GameObject piso, ojos,jugador;
    public bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        miEstado = Estado.Idle;
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
        Vector2 direccion = transform.position - jugador.transform.position;
        RaycastHit2D rayo=Physics2D.Raycast(ojos.transform.position,direccion,15f);
        if (rayo.collider && rayo.collider.transform.tag=="Player")
        {
            float distancia = ((rayo.collider.transform.position.y - transform.position.y)*(rayo.collider.transform.position.y - transform.position.y)+ (rayo.collider.transform.position.x - transform.position.x) * (rayo.collider.transform.position.x - transform.position.x));
            if(distancia<=6.2f && (miEstado==Estado.Idle || miEstado == Estado.Caminando))
            {
                miEstado = Estado.Corriendo;
                animator.SetBool(runningText, true);
            }
        }
        switch (tipo)
        {
            case 0:
                break;
            case 1:
                break;
        }
    }
    public void HacerDanio()
    {
        Debug.Log("Daño daño!");
        //GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().recibirDanio(1);
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
