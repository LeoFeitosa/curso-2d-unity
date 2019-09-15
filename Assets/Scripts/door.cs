using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public fade fade;
    public Transform tPlayer; // TRANSFORM DO PLAYER
    public Transform destino;

    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType(typeof(fade)) as fade;
    }

    public void interacao()
    {
        StartCoroutine("acionarPorta");
    }

    IEnumerator acionarPorta()
    {
        fade.fadeIn();
        yield return new WaitWhile(() => fade.fume.color.a < 0.9f);
        tPlayer.gameObject.SetActive(false);
        tPlayer.position = destino.position;
        yield return new WaitForSeconds(0.5f);
        tPlayer.gameObject.SetActive(true);
        fade.fadeOut();
    }
}
