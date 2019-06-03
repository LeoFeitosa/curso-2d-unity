using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private     Animator    playerAnimator;
    private     Rigidbody2D playerRb;
    private     float       h, v;

    public      Transform   groundCheck; //objeto responsavel em deectar se o prsonagem esta sobre uma superficie
    public      float       speed; //velocidade de movimentacao do personagem
    public      float       jumpForce; //forca aplicada para gerar o pulo do personagem
    public      bool        Grounded; //indica se o personagem esta pisando em alguma superficie
    public      bool        attacking; //indica de o personagem esta atacando
    public      bool        loockLeft; //indica se o personagem esta virado para a esquerda
    public      int         idAnimation; //indica o id da animacao
    public      Collider2D  standing, crounching; //colisor em pé, colisor agachado

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
        playerRb.velocity = new Vector2(h * speed, playerRb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if(h > 0 && loockLeft && !attacking)
        {
            flip();
        }
        else if(h< 0 && !loockLeft && !attacking)
        {
            flip();
        }

        if(v < 0)
        {
            idAnimation = 2;
            if (Grounded)
            {
                h = 0;
            }
        }
        else if (h != 0)
        {
            idAnimation = 1;
        } else
        {
            idAnimation = 0;
        }

        if (Input.GetButtonDown("Fire1") && v >= 0 && !attacking) {
            playerAnimator.SetTrigger("atack");
        }

        if (Input.GetButtonDown("Jump") && Grounded && !attacking) {
            playerRb.AddForce(new Vector2(0, jumpForce));
        }

        if (attacking && Grounded)
        {
            h = 0;
        }

        if (v < 0 && Grounded)
        {
            crounching.enabled = true;
            standing.enabled = false;
        } 
        else if (Grounded)
        {
            crounching.enabled = false;
            standing.enabled = true;            
        }
        else if (v != 0 && !Grounded)
        {
            crounching.enabled = false;
            standing.enabled = true;
        }

        playerAnimator.SetBool("grounded", Grounded);
        playerAnimator.SetInteger("idAnimation", idAnimation);
        playerAnimator.SetFloat("speedY", playerRb.velocity.y);
    }

    private void flip()
    {
        loockLeft = !loockLeft; // inverte o valor da variavel
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    public void attack(int atk)
    {
        switch (atk)
        {
            case 0:
                attacking = false;
                break;

            case 1:
                attacking = true;
                break;
        }
    }
}
