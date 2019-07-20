using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleDanoInimigo : MonoBehaviour
{
    private _GameCtrl _GameCtrl;

    public  float[]    ajusteDano;

    // Start is called before the first frame update
    void Start()
    {
        _GameCtrl = FindObjectOfType(typeof(_GameCtrl)) as _GameCtrl;
    }

    // Update is called once per frame
    void Update()
    {
        
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
                break;
        }
    }
}
