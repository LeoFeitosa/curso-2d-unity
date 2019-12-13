using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private _GameCtrl _GameCtrl;

    private     Animator    playerAnimator;
    private     Rigidbody2D playerRb;
    private     SpriteRenderer sRender;

    private     float       h, v;

    public      Transform   groundCheck; //objeto responsavel em deectar se o prsonagem esta sobre uma superficie
    public      LayerMask   whatIsGround; //indica o que é superficie para o teste do grounded

    public      int         vidaMaxima;
    public      int         vidaAtual;

    public      float       speed; //velocidade de movimentacao do personagem
    public      float       jumpForce; //forca aplicada para gerar o pulo do personagem

    public      bool        Grounded; //indica se o personagem esta pisando em alguma superficie
    public      bool        attacking; //indica de o personagem esta atacando
    public      bool        loockLeft; //indica se o personagem esta virado para a esquerda
    public      int         idAnimation; //indica o id da animacao
    public      Collider2D  standing, crounching; //colisor em pé, colisor agachado

    //intereção com itens e objetos
    public      Transform   hand;
    private     Vector3     dir = Vector3.right;
    public      LayerMask   interacao;
    public      GameObject  objetoInteracao;
    public      GameObject  alertaBalao;

    //Sistema de armas
    public      int         idArma;
    public      int         idArmaAtual;
    public      GameObject[]  armas, arcos, flechaArco, staffs;
    public      GameObject  flechaPrefab, magiaPrefab;
    public      Transform   spawnFlecha, spawnMagia;
    
    // Start is called before the first frame update
    void Start()
    {
        _GameCtrl = FindObjectOfType(typeof(_GameCtrl)) as _GameCtrl;

        //CARREGA OS DADOS INICIAIS DO PERSONAGEM
        vidaMaxima = _GameCtrl.vidaMaxima;
        idArma = _GameCtrl.idArma;

        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        sRender = GetComponent<SpriteRenderer>();

        vidaAtual = vidaMaxima;

        foreach (GameObject o in armas)
        {
            o.SetActive(false);
        }
        foreach (GameObject o in arcos)
        {
            o.SetActive(false);
        }
        foreach (GameObject o in staffs)
        {
            o.SetActive(false);
        }

        trocarArma(idArma);
    }

    private void FixedUpdate()
    {
        Grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, whatIsGround);

        playerRb.velocity = new Vector2(h * speed, playerRb.velocity.y);

        interagir();
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

        if (Input.GetButtonDown("Fire1") && v >= 0 && attacking == false && objetoInteracao == null)
        {
            playerAnimator.SetTrigger("atack");
        }

        if (Input.GetButtonDown("Fire1") && v >= 0 && attacking == false && objetoInteracao != null)
        {
            objetoInteracao.SendMessage("interacao", SendMessageOptions.DontRequireReceiver);
        }

        if (Input.GetButtonDown("Jump") && Grounded && !attacking)
        {
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
        playerAnimator.SetFloat("idClasseArma", _GameCtrl.idClasseArma[_GameCtrl.idArmaAtual]);
    }

    private void LateUpdate()
    {
        if(_GameCtrl.idArma != _GameCtrl.idArmaAtual)
        {
            trocarArma(_GameCtrl.idArma);            
        }
    }

    private void flip()
    {
        loockLeft = !loockLeft; // inverte o valor da variavel
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

        dir.x = x;
        //dir = dir;
    }

    public void attack(int atk)
    {
        switch (atk)
        {
            case 0:
                attacking = false;
                armas[2].SetActive(false);
                break;

            case 1:
                attacking = true;
                break;
        }
    }

    public void attackFlecha(int atk)
    {
        switch (atk)
        {
            case 0:
                attacking = false;
                arcos[2].SetActive(false);
                break;

            case 1:
                attacking = true;
                break;

            case 2:
                GameObject tempPrefab = Instantiate(flechaPrefab, spawnFlecha.position, spawnFlecha.localRotation);
                tempPrefab.transform.localScale = new Vector3(tempPrefab.transform.localScale.x * dir.x, tempPrefab.transform.localScale.y, tempPrefab.transform.localScale.z);
                tempPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(5 * dir.x, 0);
                Destroy(tempPrefab, 2);
                break;
        }
    }

    public void attackStaff(int atk)
    {
        switch (atk)
        {
            case 0:
                attacking = false;
                staffs[3].SetActive(false);
                break;

            case 1:
                attacking = true;
                break;

            case 2:
                GameObject tempPrefab = Instantiate(magiaPrefab, spawnMagia.position, spawnMagia.localRotation);
                tempPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(5 * dir.x, 0);
                Destroy(tempPrefab, 1);
                break;
        }
    }

    void interagir()
    {
        Debug.DrawRay(hand.position, dir * 0.9f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(hand.position, dir, 0.1f, interacao);

        if (hit == true)
        {
        print("interagiu");
            objetoInteracao = hit.collider.gameObject;
            alertaBalao.SetActive(true);
        }
        else
        {
            objetoInteracao = null;
            alertaBalao.SetActive(false);
        }
    }

    void controleArma(int id)
    {
        foreach (GameObject o in armas)
        {
            o.SetActive(false);
        }

        armas[id].SetActive(true);
    }

    void controleArco(int id)
    {
        foreach (GameObject o in arcos)
        {
            o.SetActive(false);
        }

        arcos[id].SetActive(true);
    }

    void controleStaff(int id)
    {
        foreach (GameObject o in staffs)
        {
            o.SetActive(false);
        }

        staffs[id].SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "coletavel":

                collision.gameObject.SendMessage("coletar", SendMessageOptions.DontRequireReceiver);

                break;
        }
    }

    public void changeMaterial(Material novoMaterial)
    {
        sRender.material = novoMaterial;
        foreach (GameObject o in armas)
        {
            o.GetComponent<SpriteRenderer>().material = novoMaterial;
        }
        foreach (GameObject o in arcos)
        {
            o.GetComponent<SpriteRenderer>().material = novoMaterial;
        }
        foreach (GameObject o in flechaArco)
        {
            o.GetComponent<SpriteRenderer>().material = novoMaterial;
        }
        foreach (GameObject o in staffs)
        {
            o.GetComponent<SpriteRenderer>().material = novoMaterial;
        }
    }

    public void trocarArma(int id)
    {
        _GameCtrl.idArma = id;

        switch (_GameCtrl.idClasseArma[idArma])
        {

            case 0: //espadas machados e martelos

                armas[0].GetComponent<SpriteRenderer>().sprite = _GameCtrl.spriteArmas1[idArma];
                armaInfo tempArmaInfo = armas[0].GetComponent<armaInfo>();
                tempArmaInfo.danoMin = _GameCtrl.danoMinArma[idArma];
                tempArmaInfo.danoMax = _GameCtrl.danoMaxArma[idArma];
                tempArmaInfo.tipoDano = _GameCtrl.tipoDanoArma[idArma];

                armas[1].GetComponent<SpriteRenderer>().sprite = _GameCtrl.spriteArmas2[idArma];
                tempArmaInfo = armas[1].GetComponent<armaInfo>();
                tempArmaInfo.danoMin = _GameCtrl.danoMinArma[idArma];
                tempArmaInfo.danoMax = _GameCtrl.danoMaxArma[idArma];
                tempArmaInfo.tipoDano = _GameCtrl.tipoDanoArma[idArma];

                armas[2].GetComponent<SpriteRenderer>().sprite = _GameCtrl.spriteArmas3[idArma];
                tempArmaInfo = armas[2].GetComponent<armaInfo>();
                tempArmaInfo.danoMin = _GameCtrl.danoMinArma[idArma];
                tempArmaInfo.danoMax = _GameCtrl.danoMaxArma[idArma];
                tempArmaInfo.tipoDano = _GameCtrl.tipoDanoArma[idArma];
                break;

            case 1: //arcos

                arcos[0].GetComponent<SpriteRenderer>().sprite = _GameCtrl.spriteArmas1[idArma];
                arcos[1].GetComponent<SpriteRenderer>().sprite = _GameCtrl.spriteArmas2[idArma];
                arcos[2].GetComponent<SpriteRenderer>().sprite = _GameCtrl.spriteArmas3[idArma];
                break;

            case 2: //staffs

                staffs[0].GetComponent<SpriteRenderer>().sprite = _GameCtrl.spriteArmas1[idArma];
                staffs[1].GetComponent<SpriteRenderer>().sprite = _GameCtrl.spriteArmas2[idArma];
                staffs[2].GetComponent<SpriteRenderer>().sprite = _GameCtrl.spriteArmas3[idArma];
                staffs[3].GetComponent<SpriteRenderer>().sprite = _GameCtrl.spriteArmas4[idArma];
                break;
        }

        _GameCtrl.idArmaAtual = _GameCtrl.idArma;

    }
}
