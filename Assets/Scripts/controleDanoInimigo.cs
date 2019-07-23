using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleDanoInimigo : MonoBehaviour
{
    private _GameCtrl _GameCtrl;
    private playerScript playerScript;
    private SpriteRenderer sRender;

    public int vidaInimigo;

    public  float[]    ajusteDano;
    public bool olhandoEsquerda, playerEsquerda;

    //KnockBack
    public GameObject knockforcePrefab; // forca de repulsao
    public Transform knockPosition; // ponto de origem da forca
    public float knockX; // valor padrao do positon X
    private float kx;

    private bool getHit; // indica se tomou um hit

    public Color[] characterColor; // controle de cor do personagem

    // Start is called before the first frame update
    void Start()
    {
        _GameCtrl = FindObjectOfType(typeof(_GameCtrl)) as _GameCtrl;
        playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;
        sRender = GetComponent<SpriteRenderer>();

        sRender.color = characterColor[0];

        if (olhandoEsquerda == true)
        {
            olhandoEsquerda = !olhandoEsquerda; // inverte o valor da variavel
            float x = transform.localScale.x;
            x *= -1;
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);  
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
                    armaInfo infoArma = collision.gameObject.GetComponent<armaInfo>();

                    float danoArma = infoArma.dano;
                    int tipoDano = infoArma.tipoDano;

                    //dano tomado
                    float danoTomado = danoArma + (danoArma * (ajusteDano[tipoDano] / 100));

                    vidaInimigo -= Mathf.RoundToInt(danoTomado); //rezus da vida a quantidade de dano tomado

                    if (vidaInimigo <= 0)
                    {
                        Destroy(this.gameObject);
                    }

                    print("tomei " + danoTomado + " de dano do tipo " + _GameCtrl.tiposDano[tipoDano]);

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
    }
}
