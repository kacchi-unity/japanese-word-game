using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    GameObject enemyListManager, enemy;
    List<Word> selectedWordList;

    float spawn = 1.5f;
    float delta = 0;
    public bool isSpawn = true;
    
    public void SetSelectedWordList(List<Word> selectedWordList)
    {
        this.selectedWordList = selectedWordList;
    }

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

        //create using selected word list & send data to enemy list manager script
        int randomIndex = Random.Range(0 , selectedWordList.Count);
        EnemyData enemyData;
        string kanji = selectedWordList[randomIndex].kanji;
        string meaning = selectedWordList[randomIndex].meaning;
        enemyData = new EnemyData(enemy, kanji, meaning);

        enemyListManager.GetComponent<EnemyListManager>().AddEnemyData(enemyData);

        //give enemyData to EnemyController //prefab's component
        EnemyController enemyControlloerScript = enemy.GetComponent<EnemyController>();
        enemyControlloerScript.enemyData = enemyData;
        
    }

    void Start()
    {
        enemyListManager = GameObject.Find("EnemyListManager");
    }

    void Update()
    {
        //create enemy with spawn time setted if isSpawn is true
        if (isSpawn && selectedWordList != null && selectedWordList.Count > 0)
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
