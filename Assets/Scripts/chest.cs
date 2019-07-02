using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    private _GameCtrl _GameCtrl;

    private SpriteRenderer  spriteRenderer;
    public Sprite[]         imagemObjeto;
    public bool             open;

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
                
                _GameCtrl.test += 1;
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
}
