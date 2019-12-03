using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class reSkin : MonoBehaviour
{
    private _GameCtrl _GameCtrl;
    public bool isPlayer; //verifica se este script esta associado ao personagem jogavel
    private SpriteRenderer sRender;

    public Sprite[] sprites;
    public string spriteSheetName; // nome do spriteSheet que sera utiizado
    public string loadedSpriteSheetName; //nome do spriteSheet em uso

    private Dictionary<string, Sprite> spriteSheet;

    // Start is called before the first frame update
    void Start()
    {
        _GameCtrl = FindObjectOfType(typeof(_GameCtrl)) as _GameCtrl;
        if (isPlayer) { 
            spriteSheetName = _GameCtrl.spriteSheetName[_GameCtrl.idPersonagem].name;
        }
        sRender = GetComponent<SpriteRenderer>();
        loadSpriteSheet();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isPlayer)
        {
            if (_GameCtrl.idPersonagem != _GameCtrl.idPersonagemAtual)
            {
                spriteSheetName = _GameCtrl.spriteSheetName[_GameCtrl.idPersonagem].name;
                _GameCtrl.idPersonagemAtual = _GameCtrl.idPersonagem;
            }
        }

        if (loadedSpriteSheetName != spriteSheetName)
        {
            loadSpriteSheet();
        }
        // print(sRender.sprite);
        sRender.sprite = spriteSheet[sRender.sprite.name];
    }

    private void loadSpriteSheet()
    {
        sprites = Resources.LoadAll<Sprite>(spriteSheetName);
        spriteSheet = sprites.ToDictionary(x => x.name, x => x);
        loadedSpriteSheetName = spriteSheetName;
    }
}
