using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private     Animator    playerAnimator;
    private     Rigidbody2D playerRb;

    public      Transform   groundCheck; //objeto responsavel em deectar se o prsonagem esta sobre uma superficie
    public      float       speed; //velocidade de movimentacao do personagem
    public      float       jumpForce; //forca aplicada para gerar o pulo do personagem
    public      bool        Grounded; //indica se o personagem esta pisando em alguma superficie
    public      bool        loockLeft; //indica se o personagem esta virado para a esquerda
    public      int         idAnimation; //indica o id da animacao

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if(h > 0 && loockLeft)
        {
            flip();
        }
        else if(h< 0 && !loockLeft)
        {
            flip();
        }

        if(v != 0)
        {
            idAnimation = 2;
        }
        else if (h != 0)
        {
            idAnimation = 1;
        } else
        {
            idAnimation = 0;
        }

        if (Input.GetButtonDown("Fire1") && v >= 0) {
            playerAnimator.SetTrigger("atack");
        }

        if (Input.GetButtonDown("Jump") && Grounded) {
            playerRb.AddForce(new Vector2(0, jumpForce));
        }

        playerRb.velocity = new Vector2(h * speed, playerRb.velocity.y);

        playerAnimator.SetBool("grounded", Grounded);
        playerAnimator.SetInteger("idAnimation", idAnimation);
    }

    private void flip()
    {
        loockLeft = !loockLeft; // inverte o valor da variavel
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
}
