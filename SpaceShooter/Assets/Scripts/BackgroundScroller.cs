using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * scrollSpeed * Time.deltaTime);

        float vertSize = GetComponent<SpriteRenderer>().bounds.size.y;

        if (transform.position.y < -vertSize)
            transform.Translate( Vector2.up * 2 * vertSize);
       

    }
}
