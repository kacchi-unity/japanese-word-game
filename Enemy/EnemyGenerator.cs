using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    GameObject enemyListManager, enemy;

    float spawn = 1.5f;
    float delta = 0;
    public bool isSpawn = true;
    
    public void CreateEnemyData()
    {
        //new enemy create with kanji, meaning
        enemy = Instantiate(enemyPrefab);
        int randomNum = Random.Range(0, 3);
        float spawnY;
        if (randomNum == 0) { spawnY = -0.52f; }
        else if (randomNum == 1) { spawnY = - 1.65f; }
        else { spawnY = -2.71f; }
        enemy.transform.position = new Vector3(9.7f, spawnY, 0f);

        //send data to another list script
        int randomNum2 = Random.Range(0,3);
        EnemyData enemyData;
        if (randomNum2 == 0) { enemyData = new EnemyData(enemy, "食べる", "먹다"); }
        else if (randomNum2 == 1) { enemyData = new EnemyData(enemy, "寝る", "자다"); }
        else { enemyData = new EnemyData(enemy, "見る", "보다"); }

        enemyListManager.GetComponent<EnemyListManager>().AddEnemyData(enemyData);

        //give enemyData to EnemyController //prefab's component
        EnemyController enemyControlloerScript = enemy.GetComponent<EnemyController>();
        enemyControlloerScript.enemyData = enemyData;
        
    }

    void Start()
    {
        enemyListManager = GameObject.Find("EnemyListManager");
        CreateEnemyData();
    }

    void Update()
    {
        //create enemy with spawn time setted if isSpawn is true
        if (isSpawn)
        {
            delta += Time.deltaTime;
            if (delta > spawn)
            {
                delta = 0;
                CreateEnemyData();
            }
        }
    }
}
