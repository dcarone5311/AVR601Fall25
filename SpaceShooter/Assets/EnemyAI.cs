using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int health;
    public float speed;
    public GameObject bulletPrefab;
    public Transform player;
    float coolDown = 5f;

    Vector3 targetPoint; //where enemy is heading

    float timer;

    // Start is called before the first frame update
    void Start()
    {
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

        Vector3 playerDirection = player.position - transform.position;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(playerDirection.y, playerDirection.x);
        transform.rotation = Quaternion.Euler(0f, 0f, angle -90f);


        if (timer >= coolDown)
            Shoot();


        timer += Time.deltaTime;
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
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(targetPoint, 0.05f);
    }
}
