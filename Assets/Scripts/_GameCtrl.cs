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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        boltTxt.text = gold.ToString("N0");
    }
}
