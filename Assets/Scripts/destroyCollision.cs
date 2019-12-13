using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyCollision : MonoBehaviour
{
    public LayerMask destruirLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.1f, destruirLayer);

        if (hit == true)
        {
            Destroy(this.gameObject);
        }
    }
}
