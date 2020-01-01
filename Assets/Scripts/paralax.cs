using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralax : MonoBehaviour
{
    public Transform background;
    public float paralaxScale;
    public float velocidade;

    public Transform cam;
    public Vector3 previewCamPosition;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        previewCamPosition = cam.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float paralaxX = (previewCamPosition.x - cam.position.x) * paralaxScale;
        float bgTargetX = background.position.x - paralaxX;

        Vector3 bgPos = new Vector3(bgTargetX, background.position.y, background.position.z);
        background.position = Vector3.Lerp(background.position, bgPos, velocidade * Time.deltaTime);

        previewCamPosition = cam.position;
    }
}
