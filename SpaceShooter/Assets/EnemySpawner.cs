using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public static EnemySpawner instance;
    public int maxEnemies;
    public int enemyCount;
    public GameObject enemyPrefab;

    public float coolDown;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        maxEnemies = 3;

        if(instance == null)
            instance = this;

        enemyCount = 0;
        for (int i = 0; i < 3; i++)
            SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {

        if (timer >= coolDown && enemyCount < maxEnemies)
            SpawnEnemy();

        timer += Time.deltaTime;
    }

    public void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.transform.position = Random.insideUnitCircle.normalized * 10f; //spawn 10 units away in random direction
        enemyCount++;
        timer = 0;
    }

}
