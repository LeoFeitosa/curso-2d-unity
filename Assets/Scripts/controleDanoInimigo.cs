using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleDanoInimigo : MonoBehaviour
{
    private _GameCtrl _GameCtrl;
    private playerScript playerScript;

    public  float[]    ajusteDano;
    public bool olhandoEsquerda, playerEsquerda;

    //KnockBack
    public GameObject knockforcePrefab; // forca de repulsao
    public Transform knockPosition; // ponto de origem da forca
    public float knockX; // valor padrao do positon X
    private float kx;

    // Start is called before the first frame update
    void Start()
    {
        _GameCtrl = FindObjectOfType(typeof(_GameCtrl)) as _GameCtrl;
        playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;

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
                armaInfo infoArma = collision.gameObject.GetComponent<armaInfo>();

                float danoArma = infoArma.dano;
                int tipoDano = infoArma.tipoDano;

                //dano tomado
                float danoTomano = danoArma + (danoArma * (ajusteDano[tipoDano] / 100));

                print("tomei "+ danoTomano + " de dano do tipo " + _GameCtrl.tiposDano[tipoDano]);

                GameObject knockTemp = Instantiate(knockforcePrefab, knockPosition.position, knockPosition.localRotation);
                Destroy(knockTemp, 0.02f);

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
}
