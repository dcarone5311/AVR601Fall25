using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float bulletSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * bulletSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Object Hit");
        if(gameObject.tag != collision.gameObject.tag)
        {
            Destroy(collision.gameObject); //destroy opposing ship
            Destroy(gameObject);//destroy bullet
        }

    }
}
