using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    private _GameCtrl _GameCtrl;

    private SpriteRenderer  spriteRenderer;
    public Sprite[]         imagemObjeto;
    public bool             open;
    public GameObject       loots;
    private bool            gerouLoot;

    // Start is called before the first frame update
    void Start()
    {
        loadGameCtrl();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void interacao()
    {
        open = !open;

        switch (open)
        {
            case true:
                spriteRenderer.sprite = imagemObjeto[1];
                
                loadGameCtrl();

                if(gerouLoot == false)
                {
                    StartCoroutine("gerarLoot");
                }

                break;

            case false:
                spriteRenderer.sprite = imagemObjeto[0];
                break;
        }
    }

    private void loadGameCtrl()
    {
        if (_GameCtrl == null)
        {
            _GameCtrl = FindObjectOfType(typeof(_GameCtrl)) as _GameCtrl;
        }
    }

    IEnumerator gerarLoot()
    {
        gerouLoot = true;
        // CONTROLE DE LOOT
        int qtdMoedas = Random.Range(1, 5);
        for (int l = 0; l <= qtdMoedas; l++)
        {
            GameObject lootTemp = Instantiate(loots, transform.position, transform.localRotation);
            lootTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-25, 25), 150));
            yield return new WaitForSeconds(0.01f);
        }
    }
}
