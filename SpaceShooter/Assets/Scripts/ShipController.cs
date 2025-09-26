using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    public float moveSpeed, turnSpeed;
    public float coolDown;
    public GameObject bulletPrefab;

    public int health = 100;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = coolDown;
    }

    // Update is called once per frame
    void Update()
    {

        //Obtain input from user
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Apply rotation and translation
        transform.Translate(Vector2.up * input.y * moveSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0f, 0f, -input.x * turnSpeed * Time.deltaTime));


        //restrict player's movement to boundary
        //get numbers from static boundary instance
        float boundaryLength = Boundary.instance.boundaryLength;
        float boundaryWidth = Boundary.instance.boundaryWidth;

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -boundaryLength / 2f, boundaryLength / 2f),
                                         Mathf.Clamp(transform.position.y, -boundaryWidth / 2f, boundaryWidth / 2f) );


        //shooting
        if (Input.GetKeyDown(KeyCode.Space) && timer >= coolDown) //press the space button and cooldown wore off
            Shoot();



        timer += Time.deltaTime;//count time
    }


    void Shoot()
    {
        GetComponent<AudioSource>().Play();
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        Destroy(bullet, 2f); //destroy bullets to declutter scene
        timer = 0f; //reset timer
    }



    public void TakeDamage(int damage)
    {
        health -= damage; //subtract from health
        if (health <= 0)
            Destroy(gameObject);
        StartCoroutine(FlashRed());

    }


    IEnumerator FlashRed()
    {
        SpriteRenderer renderer =GetComponent<SpriteRenderer>();

        for(float val = 1f; val >= 0; val-=0.1f) //fade from white to red
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
