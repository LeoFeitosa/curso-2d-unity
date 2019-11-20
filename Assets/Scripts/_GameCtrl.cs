using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _GameCtrl : MonoBehaviour
{
    private fade fade;

    public string[] tiposDano;
    public GameObject[] fxDano;
    public GameObject fxMorte;
    public int gold; // armezena a quantidad de ouro coletada
    public TextMeshProUGUI boltTxt;

    [Header("Banco de dados armas")]
    public Sprite[] spriteArmas1;
    public Sprite[] spriteArmas2;
    public Sprite[] spriteArmas3;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType(typeof(fade)) as fade;
        fade.fadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        string s = gold.ToString("N0");
        boltTxt.text = s.Replace(",", ".");
    }
}
