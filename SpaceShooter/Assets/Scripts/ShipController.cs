using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    public float moveSpeed, turnSpeed;
    public float coolDown;
    public GameObject bulletPrefab;

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

}
