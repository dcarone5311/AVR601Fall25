using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public int totalHealth;
    public int health;
    float initalScale;

    public Transform bar;

    // Start is called before the first frame update
    void Start()
    {
        totalHealth = GetParentHealth();
        health = totalHealth;
        initalScale = bar.localScale.x;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        health = GetParentHealth();
        bar.localScale = new Vector3((float) health/totalHealth * initalScale , bar.localScale.y, bar.localScale.z);
        bar.localPosition = Vector3.left * (0.44f - bar.localScale.x / 2f);
        transform.rotation = Quaternion.identity;
        transform.position = transform.parent.position + Vector3.up * 1f;
    }

    private int GetParentHealth()
    {
        GameObject parent = transform.parent.gameObject; //parent game object
        if (parent.tag == "Enemy") //hit enemy
            return parent.GetComponent<EnemyAI>().health;
        if (parent.tag == "Player") //hit player
            return parent.GetComponent<ShipController>().health;
        else
            return 0;
    }
}
