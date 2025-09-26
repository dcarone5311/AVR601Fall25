using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float bulletSpeed = 20f;
    public int damage = 10;

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
        GameObject other = collision.gameObject;//object hit by bullet

        Debug.Log("Object Hit");
        if(gameObject.tag != other.tag) //tags must be different enemy bullet hits player or player bullet hits enemy
        {
            if (other.tag == "Enemy") //hit enemy
                other.GetComponent<EnemyAI>().TakeDamage(damage);
            if (other.tag == "Player") //hit player
                other.GetComponent<ShipController>().TakeDamage(damage);

            Destroy(gameObject);//destroy bullet
        }

    }
}
