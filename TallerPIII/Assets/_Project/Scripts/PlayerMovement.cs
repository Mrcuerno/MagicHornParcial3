using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator anim;
    public Rigidbody2D rigi;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    public int jumpa = 100;
    bool crouch = false;
    bool tocarpared = false;
    public Image panel;
    public Image centarse;
    public float timebtwattack;
    public float startimebtwattack;
    public int vida;

    private void Start()
    {
        vida = 4;
    }

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump") && !jump)
        {
            jump = true;
            rigi.constraints = RigidbodyConstraints2D.FreezeRotation;
            rigi.gravityScale = 3;
            StopAllCoroutines();
            controller.Jumpu(jumpa);
            
        }
        if (Input.GetButtonDown("Crouch")   /*||Input.GetAxis("Vertical") < 0*/)
        {
            crouch = true;
            centarse.gameObject.SetActive(true);

        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            centarse.gameObject.SetActive(false);
        }


        

            
      

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Colisionando");
        if (collision.gameObject.CompareTag("Pared") && jump)
        {
           if (collision.gameObject.transform.position.x < this.transform.position.x)
            {
                Debug.Log("Pared Izq");

                //this.transform.localScale = new Vector3(1.0f, 0, 0);

            }

            if (collision.gameObject.transform.position.x > this.transform.position.x)
            {
                Debug.Log("Pared Derecha");
                //this.transform.localScale = new Vector3(1.0f, 0, 0);

            }
            tocarpared = true;
            jump = true;
            //Debug.Log("El brinco" + jump);
            //Debug.Log(tocarpared);
            rigi.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            StartCoroutine(Fernando());
            rigi.gravityScale = 0;
            rigi.velocity = Vector2.zero;
        }
        jump = false;
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
    }

    public IEnumerator Fernando()
    {
        yield return new WaitForSeconds(0.5f);
        rigi.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigi.gravityScale = 3;
        panel.gameObject.SetActive(true);
        anim.Play("Timer2");
        StartCoroutine(timer());
        //panel.enabled = false;


    }
    public IEnumerator timer()
    {
        yield return new WaitForSeconds(0.45f);

        panel.gameObject.SetActive(false);


    }
    public void hacerDanio()
    {
        Debug.Log("Daño daño!");
        //jugador.GetComponent<PlayerMovement>().recibirDanio(1);
    }
    public void recibirDanio(int _danio)
    {
        vida -= _danio;
        if (vida <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Victoria")
        {
            GameObject.Find("Canvas").GetComponent<SCR_MenuManager>().victoria();
        }
    }
}