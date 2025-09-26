using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public int health = 100;
    public float speed;
    public GameObject bulletPrefab;
    public Transform player;
    float coolDown = 5f;



    Vector3 targetPoint; //where enemy is heading

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<ShipController>().transform;
        SetCoolDown();
        targetPoint = Vector3.zero;
        SetNewTarget();
        timer = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = targetPoint - transform.position;
        transform.Translate(moveDirection.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(targetPoint, transform.position) < 0.4f)
            SetNewTarget();

        if(player != null) //if player exists
        {

            Vector3 playerDirection = player.position - transform.position;
            float angle = Mathf.Rad2Deg * Mathf.Atan2(playerDirection.y, playerDirection.x);
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        }


        if (timer >= coolDown)
            Shoot();


        timer += Time.deltaTime; //keep track of time


    }

    void SetNewTarget()
    {
        float boundaryLength = Boundary.instance.boundaryLength;
        float boundaryWidth = Boundary.instance.boundaryWidth;

        while(Vector3.Distance(transform.position, targetPoint) < 2f)
        {

            //chooses a random location within the boundary
            targetPoint = new Vector3(Random.Range(-boundaryLength / 2f, boundaryLength / 2f),
                                        Random.Range(-boundaryWidth / 2f, boundaryWidth / 2f),
                                        0f);
        }
    }


    void SetCoolDown()
    {
        coolDown = Random.Range(0.5f, 2f);
    }

    void Shoot()
    {
        GetComponent<AudioSource>().Play(); //play shooting sound
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        Destroy(bullet, 2f); //destroy bullets to declutter scene
        timer = 0f;
        SetCoolDown();
    }


    private void OnDrawGizmos()
    {
        //draws target location
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(targetPoint, 0.05f);
    }



    public void TakeDamage(int damage)
    {
        health -= damage; //subtract from health
        if (health <= 0)
        {
            EnemySpawner.instance.enemyCount--; //subtract 1 from enemy count
            Destroy(gameObject);
        }
        StartCoroutine(FlashRed());

    }


    IEnumerator FlashRed()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();


        for (float val = 1f; val >= 0; val -= 0.1f) //fade from white to red
        {
            renderer.color = new Color(1f, val, val);

            yield return new WaitForSeconds(0.01f);

        }

        for (float val = 0f; val <= 1; val += 0.1f) //fade from red to white
        {
            renderer.color = new Color(1f, val, val);
            yield return new WaitForSeconds(0.01f);
        }


        yield return null;
    }
}
