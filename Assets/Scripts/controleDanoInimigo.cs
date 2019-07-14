using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleDanoInimigo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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

                int danoTomado = collision.gameObject.GetComponent<armaInfo>().dano;

                print("tomei "+ danoTomado + " dano");
                break;
        }
    }
}
