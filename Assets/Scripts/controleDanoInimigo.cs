﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleDanoInimigo : MonoBehaviour
{
    private _GameCtrl _GameCtrl;
    private playerScript playerScript;
    private SpriteRenderer sRender;

    [Header("Configuração de Vida")]
    public int vidaInimigo;
    public int vidaAtual;
    public GameObject barrasVida; // objeto contendo todas as barras
    public Transform hpBar; // objeto indicador da quantidade de vida
    public Color[] characterColor; // controle de cor do personagem
    private float percVida; // controla o percentual de vida
    public GameObject danoTXTPrefab; // objeto que ira exibir o dano tomado

    [Header("Configuração Resistência/Fraqueza")]
    public  float[]    ajusteDano;
    public bool olhandoEsquerda, playerEsquerda;

    [Header("Configuração KnockBack")]
    public GameObject knockforcePrefab; // forca de repulsao
    public Transform knockPosition; // ponto de origem da forca
    public float knockX; // valor padrao do positon X
    private float kx;

    private bool getHit; // indica se tomou um hit


    // Start is called before the first frame update
    void Start()
    {
        _GameCtrl = FindObjectOfType(typeof(_GameCtrl)) as _GameCtrl;
        playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;
        sRender = GetComponent<SpriteRenderer>();

        sRender.color = characterColor[0];
        barrasVida.SetActive(false);
        vidaAtual = vidaInimigo;
        hpBar.localScale = new Vector3(1, 1, 1);

        if (olhandoEsquerda == true)
        {
            olhandoEsquerda = !olhandoEsquerda; // inverte o valor da variavel
            float x = transform.localScale.x;
            x *= -1;
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
            barrasVida.transform.localScale = new Vector3(x, barrasVida.transform.localScale.y, barrasVida.transform.localScale.x);
        }
  }

    // Update is called once per frame
    void Update()
    {
        float xPlayer = playerScript.transform.position.x;

        if(xPlayer < transform.position.x)
        {
            playerEsquerda = true;
        }
        else if(xPlayer > transform.position.x)
        {
            playerEsquerda = false;
        }

        if (olhandoEsquerda == true && playerEsquerda == true)
        {
            kx = knockX;
        }
        else if (olhandoEsquerda == false && playerEsquerda == true)
        {
            kx = knockX * -1;
        }
        else if (olhandoEsquerda == true && playerEsquerda == false)
        {
            kx = knockX * -1;
        }
        else if (olhandoEsquerda == false && playerEsquerda == false)
        {
            kx = knockX;
        }

        knockPosition.localPosition = new Vector3(kx, knockPosition.localPosition.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Arma":
                if (getHit == false)
                {
                    getHit = true;
                    barrasVida.SetActive(true);
                    armaInfo infoArma = collision.gameObject.GetComponent<armaInfo>();

                    float danoArma = infoArma.dano;
                    int tipoDano = infoArma.tipoDano;

                    //dano tomado
                    float danoTomado = danoArma + (danoArma * (ajusteDano[tipoDano] / 100));

                    vidaAtual -= Mathf.RoundToInt(danoTomado); //rezus da vida a quantidade de dano tomado

                    percVida = (float)vidaAtual / (float)vidaInimigo;
                    if (percVida < 0) { percVida = 0; }

                    hpBar.localScale = new Vector3(percVida, 1, 1);

                    if (vidaAtual <= 0)
                    {
                        Destroy(this.gameObject);
                    }

                    print("tomei " + danoTomado + " de dano do tipo " + _GameCtrl.tiposDano[tipoDano]);

                    GameObject danoTemp = Instantiate(danoTXTPrefab, transform.position, transform.localRotation);
                    danoTemp.GetComponent<TextMesh>().text = Mathf.RoundToInt(danoTomado).ToString();
                    danoTemp.GetComponent<MeshRenderer>().sortingLayerName = "HUD";
                    int forcaX = 50;
                    if(playerEsquerda == false) { forcaX *= -1; }
                    danoTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaX, 230));
                    Destroy(danoTemp, 1f);

                    GameObject knockTemp = Instantiate(knockforcePrefab, knockPosition.position, knockPosition.localRotation);
                    Destroy(knockTemp, 0.02f);

                    StartCoroutine("invuneravel");
                }
                break;
        }
    }

    void flip()
    {
        olhandoEsquerda = !olhandoEsquerda; // inverte o valor da variavel
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        barrasVida.transform.localScale = new Vector3(x, barrasVida.transform.localScale.y, barrasVida.transform.localScale.x);
    }

    IEnumerator invuneravel()
    {
        sRender.color = characterColor[1];
        yield return new WaitForSeconds(0.2f);
        sRender.color = characterColor[0];
        yield return new WaitForSeconds(0.2f);
        sRender.color = characterColor[1];
        yield return new WaitForSeconds(0.2f);
        sRender.color = characterColor[0];
        yield return new WaitForSeconds(0.2f);
        sRender.color = characterColor[1];
        yield return new WaitForSeconds(0.2f);
        sRender.color = characterColor[0];
        getHit = false;
        barrasVida.SetActive(false);
    }
}
