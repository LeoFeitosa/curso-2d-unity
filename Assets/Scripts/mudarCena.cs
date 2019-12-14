using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mudarCena : MonoBehaviour
{
    private fade fade;
    public string cenaDestino;
    private _GameCtrl _GameCtrl;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType(typeof(fade)) as fade;
        _GameCtrl = FindObjectOfType(typeof(_GameCtrl)) as _GameCtrl;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void interacao()
    {
        StartCoroutine("mudancaCena");
    }

    IEnumerator mudancaCena()
    {
        fade.fadeIn();
        yield return new WaitWhile(() => fade.fume.color.a < 0.9f);

        if (cenaDestino == "titulo") { Destroy(_GameCtrl.gameObject); }
        SceneManager.LoadScene(cenaDestino);
    }
}
