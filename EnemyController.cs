using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public EnemyData enemyData; //received from generator
    public EnemyListSO enemyListSO;
    GameObject ScoreManager, PlayerObject;
    ScoreManager ScoreManagerScript;
    public TextMeshProUGUI hintText;

    public static event Action<int> OnPlayerDamaged;

    public bool isKilledbyPlayer = false;
    bool isMove = true;
    int scoreAmount = 1;
    float moveSpeed = 0.02f;
    float damageToPlayer = 20f;

    private void OnEnable()
    {
        HintManager.OnHintStatusCheck += ApplyHint;
    }

    private void OnDisable()
    {
        HintManager.OnHintStatusCheck -= ApplyHint;
    }

    public void Die()
    {
        //case of enemy killed by player attack
        //isKilledbyPlayer becomes true by EnemyListManager
        if (isKilledbyPlayer)
        {
            Destroy(gameObject);
            ScoreManagerScript.increaseScore(scoreAmount);
        }

        //case of enemy collider with player
        else
        {
            Destroy(gameObject);
            PlayerObject.GetComponent<PlayerHpManager>().DecreaseHp(damageToPlayer);
        }
    }

    

    //to collide with player
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isKilledbyPlayer = false;
            //Debug.Log("플레이어 충돌!");
            int enemyId = this.enemyData.GetId();
            OnPlayerDamaged?.Invoke(enemyId); //HintManager, etc...
            enemyListSO.RemoveEnemyData(enemyData);
            Die();
        }
    }

    public void SetMeaning(string meaning)
    {
        //Hint text initialization
        hintText.text = meaning;

        //Check hint status
        ApplyHint(enemyData.GetId());
    }

    private void ApplyHint(int targetId)
    {
        if (this.enemyData.GetId() == targetId)
        {
            if (HintStatus.isHintActive(targetId)) //bool type
            {
                hintText.gameObject.SetActive(true);
            }

            else
            {
                hintText.gameObject.SetActive(false);
            }
        }
    }

    

    void Start()
    {
        ScoreManager = GameObject.Find("ScoreManager");
        ScoreManagerScript = ScoreManager.GetComponent<ScoreManager>();
        PlayerObject = GameObject.Find("Player");


    }

    void Update()
    {
        if (isMove)
        {
            transform.Translate(new Vector3(-moveSpeed, 0f, 0f));
        }
    }

    
}