using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_enemigo : MonoBehaviour
{
    private int vida = 4;
    private Animator animator;
    public enum Estado { Idle, Caminando, Corriendo, Atacando }
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
        miEstado = Estado.Idle;
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
        Vector2 direccion = jugador.transform.position - transform.position;

        RaycastHit2D rayo = Physics2D.Raycast(ojos.transform.position, direccion.normalized, Mathf.Infinity, layerMaskPlayer);
        if (rayo.collider && rayo.collider.transform.root.tag == "Player")
        {
            float distancia = Mathf.Sqrt((rayo.collider.transform.position.y - transform.position.y) * (rayo.collider.transform.position.y - transform.position.y) + (rayo.collider.transform.position.x - transform.position.x) * (rayo.collider.transform.position.x - transform.position.x));
            //Debug.Log("Distancia: " + distancia);
            if (distancia <= 10f && (miEstado == Estado.Idle || miEstado == Estado.Caminando))
            {   
                if ((transform.position.x - rayo.collider.transform.position.x) > 0)
                {
                    
                    if (transform.rotation.y == 1)
                    {
                        dirActual = false;
                        Debug.Log("girar a izquierda");
                        transform.Rotate(0, 180, 0, Space.Self);
                    }
                }
                else
                {
                    
                    if (transform.rotation.y == 0)
                    {
                        dirActual = true;
                        Debug.Log("girar a derecha");
                        transform.Rotate(0, 180, 0, Space.Self);
                    }
                }
                miEstado = Estado.Corriendo;
                animator.SetBool(runningText, true);
            }
            else if(miEstado==Estado.Idle)
            {
                miEstado = Estado.Caminando;
            }
        }
        else
        {
            Debug.DrawRay(ojos.transform.position, direccion.normalized, Color.black);
            miEstado = Estado.Caminando;
        }
        switch (tipo)
        {
            case 0:

                break;
            case 1:
                break;
        }
        if (miEstado == Estado.Caminando)
        {
            RaycastHit2D rayo2 = Physics2D.Raycast(ojos.transform.position, Vector2.left, 15f, layerMaskPlayer);
            if (rayo2.collider)
            {
                Debug.DrawRay(ojos.transform.position, rayo2.collider.transform.position - transform.position, Color.yellow);
                //Debug.Log(rayo2.collider.transform.position);
                //Debug.Log(rayo2.collider.transform.position.x - transform.position.x);
                if (rayo2.collider.transform.position.x - transform.position.x < 1.5 && rayo2.collider.transform.position.x - transform.position.x > -1.5)
                {
                    dirActual = !dirActual;
                    transform.Rotate(0, 180, 0, Space.Self);
                }
            }
            else
            {
                Debug.DrawRay(ojos.transform.position, Vector2.left * 15f, Color.red);
            }
            if (grounded)
            {
                transform.Translate(Vector2.left * Time.deltaTime, Space.Self);
            }
            else
            {
                dirActual = !dirActual;
                transform.Rotate(0, 180, 0, Space.Self);
            }
        }
        if (miEstado == Estado.Corriendo)
        {
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
        }
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
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(ojos.transform.position, Vector2.left*15f);
    }
}
