using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _GameCtrl : MonoBehaviour
{
    public string[] tiposDano;
    public GameObject[] fxDano;
    public GameObject fxMorte;
    public int gold; // armezena a quantidad de ouro coletada
    public TextMeshProUGUI boltTxt;

    [Header("Informações Player")]
    public int idPersonagem;
    public int idPersonagemAtual;
    public int vidaMaxima;
    public int idArma, idArmaAtual;
    
    [Header("Banco de Personagens")]
    public string[] nomePersonagem;
    public Texture[] spriteSheetName;
    public int[] idClasse;
    public int[] idArmaInicial;

    [Header("Banco de dados armas")]
    public string[] nomeArma;
    public int[] custoArma;
    public int[] idClasseArma; // 0=machado, martelo espadas - 1=arcos, 2=staffs

    public Sprite[] spriteArmas1;
    public Sprite[] spriteArmas2;
    public Sprite[] spriteArmas3;
    public Sprite[] spriteArmas4;
    public int[] danoMinArma;
    public int[] danoMaxArma;
    public int[] tipoDanoArma;

    [Header("Paineis")]
    public GameObject painelPause;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        idPersonagem = PlayerPrefs.GetInt("idPersonagem");
    }

    // Update is called once per frame
    void Update()
    {
        string s = gold.ToString("N0");
        boltTxt.text = s.Replace(",", ".");

        if (Input.GetButtonDown("Cancel"))
        {
            pauseGame();
        }
    }

    public void validarArma()
    {
        if(idClasseArma[idArma] != idClasse[idPersonagem])
        {
            idArma = idArmaInicial[idPersonagem];
        }
    }

    void pauseGame()
    {
        bool pauseState = painelPause.activeSelf;
        pauseState = !pauseState;
        painelPause.SetActive(pauseState);

        switch(pauseState)
        {
            case true:
                Time.timeScale = 0;
                break;

            case false:
                Time.timeScale = 1;
                break;
        }

    }
}
