using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;

    public Rigidbody2D rigi;

    public float runSpeed = 40f;
    public Camara cam;
    float horizontalMove = 0f;
    bool jump = false;
    public int jumpa = 100;
    bool crouch = false;
    bool tocarpared = false;
    bool tocarI = false;
    bool tocarD = false;

    void CaerPared()
    {
        //GetComponent<BoxCollider2D>().sharedMaterial = null;
        //GetComponent<CircleCollider2D>().sharedMaterial = null;
        Debug.Log("Entroxx");
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

        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisionando");


        if (collision.gameObject.CompareTag("Pared") && jump)
        {
            if (collision.gameObject.transform.position.x < this.transform.position.x)
            {


            }

            tocarpared = true;
            jump = true;

            Debug.Log("El brinco" + jump);
            Debug.Log(tocarpared);
            rigi.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            StartCoroutine(Fernando());
            rigi.gravityScale = 0;
            rigi.velocity = Vector2.zero;
            CaerPared();
        }

        jump = false;
    }

    

        void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        
    }

    public IEnumerator Fernando()
    {
        yield return new WaitForSeconds(0.3f);
        rigi.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigi.gravityScale = 3;
    }
}