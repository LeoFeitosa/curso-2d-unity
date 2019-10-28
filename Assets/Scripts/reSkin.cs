using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class reSkin : MonoBehaviour
{
    private SpriteRenderer sRender;

    public Sprite[] sprites;
    public string spriteSheetName; // nome do spriteSheet que sera utiizado
    public string loadedSpriteSheetName; //nome do spriteSheet em uso

    private Dictionary<string, Sprite> spriteSheet;

    // Start is called before the first frame update
    void Start()
    {
        sRender = GetComponent<SpriteRenderer>();
        loadSpriteSheet();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (loadedSpriteSheetName != spriteSheetName)
        {
            loadSpriteSheet();
        }
        print(sRender.sprite);
        sRender.sprite = spriteSheet[sRender.sprite.name];
    }

    private void loadSpriteSheet()
    {
        sprites = Resources.LoadAll<Sprite>(spriteSheetName);
        spriteSheet = sprites.ToDictionary(x => x.name, x => x);
        loadedSpriteSheetName = spriteSheetName;
    }
}
