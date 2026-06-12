using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public EnemyListSO enemyListSO;
    public GameObject enemyPrefab;
    GameObject enemy;
    List<Word> selectedWordList;

    private bool isSpawn = false;
    public bool IsSpawn => isSpawn;

    //Set IsSpawn (Encapsulation)
    public void SetSpawnState(bool state)
    {
        if (this.isSpawn == state)
        {
            return;
        }

        this.isSpawn = state;
        Debug.Log($"Enemy 스폰 상태가 {state}로 변경되었습니다.");
    }

    float spawnDelay = 0f;
    float delta = 0;
    
    float enemyMaxSpeed = 0.06f;
    float enemyMinSpeed = 0.01f;
    float moveSpeed = 0f;

    private void OnEnable()
    {
        WordBoardButtonManager.OnBattleStartButtonClick += BattleStartEventHandling;
    }

    private void OnDisable()
    {
        WordBoardButtonManager.OnBattleStartButtonClick -= BattleStartEventHandling;
    }

    void BattleStartEventHandling()
    {
        this.SetSpawnState(true);
    }

    public void SetSelectedWordList(List<Word> selectedWordList)
    {
        this.selectedWordList = selectedWordList;
    }

    public void CreateEnemyData()
    {
        //create using selected word list & send data to enemy list manager script
        int randomIndex = Random.Range(0, selectedWordList.Count);
        EnemyData enemyData;
        string kanji = selectedWordList[randomIndex].kanji;
        string meaning = selectedWordList[randomIndex].meaning;
        int id = selectedWordList[randomIndex].id;

        //new enemy create with kanji, meaning
        enemy = Instantiate(enemyPrefab);
        enemyData = new EnemyData(enemy, kanji, meaning, id);
        
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.enemyData = enemyData;

        enemyListSO.AddEnemyData(enemyData);

        //Hint text initialization
        enemyController.SetMeaning(meaning);

        //enemy speed setting with lobby setting SO
        enemyController.SetMoveSpeed(this.moveSpeed);
        

        //Select spawn position
        int randomNum = Random.Range(0, 3);
        float spawnY;
        if (randomNum == 0) { spawnY = -0.52f; }
        else if (randomNum == 1) { spawnY = - 1.65f; }
        else { spawnY = -2.71f; }
        enemy.transform.position = new Vector3(9.7f, spawnY, 0f);
        
    }
    void Awake()
    {
        //Set enemy move speed
        float settingSO_SpeedRate = GameDataManager.Instance.RuntimeLobbySetting.GetValue(SettingList.EnemySpeedRate);
        this.moveSpeed = settingSO_SpeedRate * (enemyMaxSpeed - enemyMinSpeed) + enemyMinSpeed;

        //setting spawn delay with lobby setting SO
        this.spawnDelay = GameDataManager.Instance.RuntimeLobbySetting.GetValue(SettingList.EnemySpawnDelay);

        this.SetSpawnState(false);
    }

    void Update()
    {
        //create enemy with spawn time setted if isSpawn is true
        if (isSpawn && selectedWordList != null && selectedWordList.Count > 0)
        {
            delta += Time.deltaTime;
            if (delta > spawnDelay)
            {
                delta = 0;
                CreateEnemyData();
            }
        }
    }
}
