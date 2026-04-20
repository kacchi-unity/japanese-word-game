using UnityEngine;
using TMPro;
using System;

public class EnemyController : MonoBehaviour
{
    public EnemyData enemyData; //received from generator
    public EnemyListSO enemyListSO;
    GameObject ScoreManager, PlayerObject;
    ScoreManager ScoreManagerScript;
    TextMeshProUGUI hintText;
    string meaning;

    public static event Action<string> OnPlayerDamaged;

    public bool isKilledbyPlayer = false;
    bool isMove = true;
    int scoreAmount = 1;
    float moveSpeed = 0.02f;
    float damageToPlayer = 20f;
    

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
            OnPlayerDamaged?.Invoke(this.enemyData.GetKanji());
            enemyListSO.RemoveEnemyData(enemyData);
            this.Die();
        }
    }

    public void ShowHint()
    {
        hintText.gameObject.SetActive(true);
        
    }

    public void HideHint()
    {
        hintText.gameObject.SetActive(false);

    }

    void Start()
    {
        ScoreManager = GameObject.Find("ScoreManager");
        ScoreManagerScript = ScoreManager.GetComponent<ScoreManager>();
        PlayerObject = GameObject.Find("Player");

        //Hint text initialization
        meaning = enemyData.GetMeaning();
        TextMeshProUGUI[] listUI = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI TMProUI in listUI)
        {
            if (TMProUI.name.Equals("Hint"))
            {
                hintText = TMProUI;
                hintText.text = meaning;
                HideHint();
                break;
            }
        }
        

    }

    void Update()
    {
        if (isMove)
        {
            transform.Translate(new Vector3(-moveSpeed, 0f, 0f));
        }
    }

    
}